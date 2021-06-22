using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Grid Design")]
    public Sprite sprite;
    public TextMeshPro text;
    public Transform gridParent;

    [Header("Player")]
    public GameObject _player;

    [Header("Monster")]
    public GameObject _centipede;

    private IEnumerator Start()
    {
        Grid.Instance().SetSprite(sprite);
        Grid.Instance().SetParent(gridParent);
        Grid.Instance().CreateGrid();
       

        while (!Grid.Instance().GetGridCreatedIsDone())
        {
            yield return null;
        }
        //Create Player;
        Vector3 position = Vector3.zero;
        Grid.Instance().GetPositionGrid(Grid.Instance().GetCenterX(), 0,out position);
        Debug.Log(Grid.Instance().GetCenterX());
        Debug.Log(position);
        var player = Instantiate(_player, position, Quaternion.identity);

        yield return new WaitForSeconds(1f);
        Vector3 centipedePosition = Vector3.zero;
        Grid.Instance().GetPositionGrid(0, Grid.Instance().GetMaxY(), out centipedePosition);
        var centipede = Instantiate(_centipede, centipedePosition, Quaternion.identity);
    }
}
