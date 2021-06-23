using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Player
{
    private bool isMoving;
    private Vector3 originPos, targetPos;
    private float timeToMove = 0.1f;
    private Vector3 directions;
    private bool mushroomFounded;
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
        if (mushroomFounded) yield return null;
        isMoving = true;
        float elapsedtime = 0;
        originPos = transform.position;
        targetPos = originPos + direction;
        //Debug.Log("Target Pos: " + targetPos);
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
        RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector3.zero);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Mushroom"))
            {
                targetPos = originPos;
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision != null)
    //    {
    //        if (collision.CompareTag("Mushroom"))
    //        {
    //            mushroomFounded = true;
    //            Debug.Log("found!");
    //        }
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision != null)
    //    {
    //        if (collision.CompareTag("Mushroom"))
    //        {
    //            mushroomFounded = false;
    //        }
    //    }
    //}

}
