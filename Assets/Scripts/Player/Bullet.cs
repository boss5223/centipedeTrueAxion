using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool isFired;
    private Vector3 originPos, targetPos;
    [Range(0.01f, 9.9f)] public float timeToMove = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (!isFired)
        {
            StartCoroutine(Acceleration(Vector3.up));
        }
    }

    IEnumerator Acceleration(Vector3 direction)
    {
        isFired = true;
        float elapsedtime = 0;
        originPos = transform.position;
        targetPos = originPos + direction;
        //Debug.Log("Target Pos: " + targetPos);
        #region max-min grid control
        int x;
        int y;
        Grid.Instance().GetGridXY(targetPos, out x, out y);

        //RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector3.zero);
        //if (hit.collider != null)
        //{
        //    if (hit.collider.CompareTag("Centipede"))
        //    {
        //        Debug.Log("Fire Centipede!!");
        //        var centipede = hit.collider.GetComponent<CentipedeController>();
        //        if (centipede != null) 
        //        {
        //            centipede.SetOtherCentipedeDirection();
        //            Destroy(hit.collider.gameObject);
        //            Destroy(gameObject);
        //            Debug.Log("Centipede Remaining: " + GameManager.Instance.CheckCentipedeRemain());
        //            if(GameManager.Instance.CheckCentipedeRemain() <= 0)
        //            {
        //                GameManager.Instance.GameOverState();
        //            }
        //        }
              
        //    }
        //    else if (hit.collider.CompareTag("Mushroom"))
        //    {
        //        Debug.Log("Fire Mushroom!");
        //        var mushroom = hit.collider.GetComponent<Mushroom>();
        //        if(mushroom != null)
        //        {
        //            mushroom.DecreaseHealthMushroom();
        //            Destroy(gameObject);
        //        }
        //    }
        //}
        if(y > Grid.Instance().GetMaxY())
        {
            Destroy(this.gameObject);
        }
        while (elapsedtime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, elapsedtime / timeToMove);
            elapsedtime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos; 
        #endregion
        isFired = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Centipede"))
            {
                Debug.Log("Fire Centipede!!");
                var centipede = collision.GetComponent<CentipedeController>();
                if (centipede != null)
                {
                    centipede.SetOtherCentipedeDirection();
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                    Debug.Log("Centipede Remaining: " + GameManager.Instance.CheckCentipedeRemain());
                    if (GameManager.Instance.CheckCentipedeRemain() <= 0)
                    {
                        GameManager.Instance.GameOverState();
                    }
                }

            }
            else if (collision.CompareTag("Mushroom"))
            {
                Debug.Log("Fire Mushroom!");
                var mushroom = collision.GetComponent<Mushroom>();
                if (mushroom != null)
                {
                    mushroom.DecreaseHealthMushroom();
                    Destroy(gameObject);
                }
            }
        }
    }
}
