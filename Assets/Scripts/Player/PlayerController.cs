using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Player
{
    private bool isMoving;
    private Vector3 originPos, targetPos;
    [Range(0.1f,10f)]public float timeToMove = 0.1f;
    private Vector3 directions;
    private bool mushroomFounded;
    private GameObject bullet;
    private bool isFired;

    void Start()
    {
        bullet = Resources.Load<GameObject>("Prefab/Bullet");
    }
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

        if (Input.GetKey(KeyCode.Space) && !isFired)
        {
            StartCoroutine(Fire());
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

    public IEnumerator Fire()
    {
        if (bullet == null) yield return null;
        isFired = true;
        var bullets = Instantiate(bullet, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        isFired = false;
    }

    public IEnumerator Dead()
    {
        //Animation กระพริบๆ
        PlayerAttribute.Instance.DecreaseHealth();
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.RespawnManager();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Centipede"))
            {
                if (!GameManager.Instance._deadState)
                {
                    Debug.Log("Attake Player!!!!");
                    GameManager.Instance._deadState = true;
                    StartCoroutine(Dead());
                }
            }
        }
    }



}
