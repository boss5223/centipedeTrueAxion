using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridManager : MonoBehaviour
{
    [Header("Grid Design")]
    public Sprite sprite;
    public TextMeshPro text;

    void StartGrid()
    {

        Grid.Instance().SetSprite(sprite);
        Grid.Instance().CreateGrid();
        
    }

 
}
