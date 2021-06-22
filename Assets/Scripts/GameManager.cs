using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Grid Design")]
    public Sprite sprite;
    public TextMeshPro text;

    [Header("Player")]
    public GameObject _player;

    private IEnumerator Start()
    {
        Grid.Instance().SetSprite(sprite);
        Grid.Instance().CreateGrid();

        while (!Grid.Instance().GetGridCreatedIsDone())
        {
            yield return null;
        }
        //Create Player;
        Vector3 position = Vector3.zero;
        Grid.Instance().GetPositionGrid(Grid.Instance().GetCenterX(), 0,out position);
        var player = Instantiate(_player, position, Quaternion.identity);
    }
}
