using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private static Grid instance;
    public float[,] Grids;
    private bool gridCreated;
    Sprite sprite;

    private float Vertical, Horizontal, Columns, Rows;
    private float cellsize = 1;
    private Grid()
    {
        gridCreated = false;
    }
    public static Grid Instance()
    {
        if (instance == null)
        {
            Debug.Log("Instance Grid");
            instance = new Grid();
        }
        return instance;
    }

    public void CreateGrid()
    {
        Vertical = (int)Camera.main.orthographicSize;
        Debug.Log((int)Camera.main.orthographicSize);
        Horizontal = Mathf.RoundToInt(Vertical * ((float)Screen.width / (float)Screen.height));
        Debug.Log("size : "+ Screen.width +" "+ Screen.height);
        Debug.Log(Horizontal);

        Columns = Horizontal * 2f;
        Rows = Vertical * 2f;
        Grids = new float[(int)Columns, (int)Rows];
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                SpawnTile(i, j);
            }
        }
        gridCreated = true;
    }
    private void SpawnTile(int x, int y)
    {
        GameObject tile = new GameObject("x:" + x + "y:" + y);
        tile.transform.position = new Vector3(x - (Horizontal - 0.5f), y - (Vertical - 0.5f));
        //Instantiate(text, tile.transform.position, Quaternion.identity);
        var spriterender = tile.AddComponent<SpriteRenderer>();
        Color color = new Color();
        color.r = 0;
        color.g = 0;
        color.b = 0;
        color.a = 1;
        spriterender.sprite = sprite;
        spriterender.color = color;
    }

    public bool GetGridCreatedIsDone()
    {
        return gridCreated;
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public void GetPositionGrid(int x,int y,out Vector3 position)
    {
        position = new Vector3(x - (Horizontal - 0.5f), y - (Vertical - 0.5f));
    }

    public void GetGridXY(Vector3 position,out int x,out int y)
    {
        x = Mathf.RoundToInt(position.x + (Horizontal - 0.5f));
        y = Mathf.RoundToInt(position.y + (Vertical - 0.5f));
    }

    public int GetCenterX()
    {
        return (int)Columns / 2;
    }

    public int GetCanWalkVertical()
    {
        return Mathf.RoundToInt((Vertical / 100) * 15);
    }

    public int GetCanWalkHorizontal()
    {
        return Mathf.RoundToInt(Horizontal*2-1);
    }
}
