using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : MonoBehaviour,Centipede
{
    public int indexCentipede { get; set; }
    private bool isMoving;
    private Vector3 originPos, targetPos;
    [Range(0.1f, 9.9f)] public float timeToMove = 0.1f;
    private int directionLookAt = 1;
    private int directionUpDown = 1;

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveDirection(Vector3.right * directionLookAt));
        }
    }
    public IEnumerator MoveDirection(Vector3 direction)
    {
        isMoving = true;
        float elapsedtime = 0;
        originPos = transform.position;
        targetPos = originPos + direction;
        //Debug.Log("Target Pos: " + targetPos);
        #region max-min grid control
        int x;
        int y;
        Grid.Instance().GetGridXY(targetPos, out x, out y);
        if (y < 0)
        {
            targetPos = new Vector3(targetPos.x, originPos.y);
        }

        RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector3.zero);
        if (hit.collider != null) {
            if (x > Grid.Instance().GetCanWalkHorizontal() || x < 0 || hit.collider.CompareTag("Mushroom"))
            {
                if (y <= 0)
                {
                    directionUpDown = -1;
                }
                else if (y >= Grid.Instance().GetMaxY())
                {
                    directionUpDown = 1;
                }
                targetPos += Vector3.down * directionUpDown;
                targetPos = new Vector3(originPos.x, targetPos.y);
                if (directionLookAt == 1)
                {
                    directionLookAt = -1;
                }
                else
                {
                    directionLookAt = 1;
                }
            }
        }
        else
        {
            if (x > Grid.Instance().GetCanWalkHorizontal() || x < 0)
            {
                if (y <= 0)
                {
                    directionUpDown = -1;
                }
                else if (y >= Grid.Instance().GetMaxY())
                {
                    directionUpDown = 1;
                }
                targetPos += Vector3.down * directionUpDown;
                targetPos = new Vector3(originPos.x, targetPos.y);
                if (directionLookAt == 1)
                {
                    directionLookAt = -1;
                }
                else
                {
                    directionLookAt = 1;
                }
            }
        }
       
        #endregion
        Vector3 lookAtTarget = targetPos - transform.position;
        float angle = Mathf.Atan2(lookAtTarget.y, lookAtTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q,1f);
        while (elapsedtime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, elapsedtime / timeToMove);
            elapsedtime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        // Attack Player
        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, Vector3.zero);
        if(playerHit.collider != null)
        {
            if (playerHit.collider.CompareTag("Player"))
            {
                Debug.Log("Attake Player!!!!");
            }
        }
        isMoving = false;
    }

    public void SetDirectionCentipede(int direction)
    {
        directionLookAt = direction;
    }

    public void SetOtherCentipedeDirection()
    {
        int[] dir =  new int[2]{1,-1};
        bool centipedeNull = false;
        while (!centipedeNull) // Right Centipede
        {
            var direction = Vector3.right * dir[0];
            var targetPos = transform.position + direction;
            RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector3.zero);
            if(hit.collider != null)
            {
                if (hit.collider.CompareTag("Centipede"))
                {
                    hit.collider.GetComponent<CentipedeController>().SetDirectionCentipede(1);
                }
                else
                {
                    centipedeNull = true;
                }
            }
            else
            {
                centipedeNull = true;
            }

            dir[0] += 1;
        }

        centipedeNull = false;
        while (!centipedeNull) // Left Centipede
        {
            var direction = Vector3.right * dir[1];
            var targetPos = transform.position + direction;
            RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector3.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Centipede"))
                {
                    hit.collider.GetComponent<CentipedeController>().SetDirectionCentipede(-1);
                }
                else
                {
                    centipedeNull = true;
                }
            }
            else
            {
                centipedeNull = true;
            }

            dir[1] -= 1;
        }
        Debug.Log("Split Centipede Success!!");
    }

}
