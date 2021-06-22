using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Player
{
    private bool isMoving;
    private Vector3 originPos, targetPos;
    private float timeToMove = 0.2f;
    private Vector3 directions;
    // Update is called once per frame
    void Update()
    {
        directions = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow) && !isMoving)
        {
            directions += Vector3.up;
            directions += Vector3.right;
            //Debug.Log(directions);
            StartCoroutine(Move(directions));
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && !isMoving)
        {
            directions += Vector3.up;
            directions += Vector3.left;
            //Debug.Log(directions);
            StartCoroutine(Move(directions));
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) && !isMoving)
        {
            directions += Vector3.down;
            directions += Vector3.left;
            //Debug.Log(directions);
            StartCoroutine(Move(directions));
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow) && !isMoving)
        {
            directions += Vector3.down;
            directions += Vector3.right;
            //Debug.Log(directions);
            StartCoroutine(Move(directions));
        }
        if (Input.GetKey(KeyCode.UpArrow) && !isMoving)
        {
            //StartCoroutine(Move(Vector3.up));
            directions += Vector3.up;
            StartCoroutine(Move(directions));
        }
        if (Input.GetKey(KeyCode.DownArrow) && !isMoving)
        {
            //StartCoroutine(Move(Vector3.down));
            directions += Vector3.down;
            StartCoroutine(Move(directions));
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !isMoving)
        {
            //StartCoroutine(Move(Vector3.left));
            directions += Vector3.left;
            StartCoroutine(Move(directions));
        }
        if (Input.GetKey(KeyCode.RightArrow) && !isMoving)
        {
            //StartCoroutine(Move(Vector3.right));
            directions += Vector3.right;
            StartCoroutine(Move(directions));
        }

    }

    public IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        float elapsedtime = 0;
        originPos = transform.position;
        targetPos = originPos + direction;
        Debug.Log("Target Pos: " + targetPos);
        //if(targetPos.y > Grid.Instance().GetPlayerCanWalkVertical())
        //{
        //    targetPos = new Vector3(targetPos.x, Grid.Instance().GetPlayerCanWalkVertical());
        //}
        int x;
        int y;
        Grid.Instance().GetGridXY(targetPos, out x, out y);
        if (y > Grid.Instance().GetCanWalkVertical() || y < 0)
        {
            targetPos = new Vector3(targetPos.x, originPos.y);
        }
        if(x > Grid.Instance().GetCanWalkHorizontal() || x<0)
        {
            targetPos = new Vector3(originPos.x, targetPos.y);
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
