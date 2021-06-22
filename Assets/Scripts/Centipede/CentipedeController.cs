using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : MonoBehaviour,Centipede
{

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
        Debug.Log("Target Pos: " + targetPos);
        int x;
        int y;
        Grid.Instance().GetGridXY(targetPos, out x, out y);
        if (y < 0)
        {
            targetPos = new Vector3(targetPos.x, originPos.y);
        }
        if (x > Grid.Instance().GetCanWalkHorizontal() || x < 0)
        {
            if (y <= 0)
            {
                directionUpDown = -1;
            }
            else if(y >= Grid.Instance().GetMaxY())
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
        while (elapsedtime < timeToMove)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, elapsedtime / timeToMove);
            elapsedtime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

}
