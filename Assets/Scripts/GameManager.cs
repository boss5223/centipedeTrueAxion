using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Grid Design")]
    public Sprite sprite;
    public TextMeshPro text;
    public Transform gridParent;

    [Header("Player")]
    public GameObject _player;

    [Header("Monster")]
    public GameObject _centipede;
    [Range(15, 40)] public int centipedeParts;
    public Transform _centipedeParent;

    [Header("Mushroom")]
    public GameObject _mushroom;
    [Range(15,40)]public int amountMushroom;
    public Transform _mushroomParent;

    public event System.Action<int> onFireCentipede;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private IEnumerator Start()
    {
        onFireCentipede += CentipedeKilled;
        Grid.Instance().SetSprite(sprite);
        Grid.Instance().SetParent(gridParent);
        Grid.Instance().CreateGrid();
        while (!Grid.Instance().GetGridCreatedIsDone())
        {
            yield return null;
        }
        CreatePlayer();
        yield return new WaitForSeconds(1f);
        CreateCentipede(centipedeParts);
        CreateMushroom(amountMushroom);
    }

    void CreatePlayer()
    {
        Vector3 position = Vector3.zero;
        Grid.Instance().GetPositionGrid(Grid.Instance().GetCenterX(), 0, out position);
        Debug.Log(Grid.Instance().GetCenterX());
        Debug.Log(position);
        var player = Instantiate(_player, position, Quaternion.identity);
    }

    void CreateCentipede(int amountCentipede)
    {
        bool _hasHead = false;
        int indexCentipede = 0;
        if (amountCentipede > Grid.Instance().GetMaxX())
        {
            var remain = amountCentipede - Grid.Instance().GetMaxX();
            remain = Grid.Instance().GetMaxX() - remain;
            for (int i = remain; i < Grid.Instance().GetMaxX(); i++)
            {

                Vector3 centipedePosition = Vector3.zero;
                Grid.Instance().GetPositionGrid(i, Grid.Instance().GetMaxY() - 1, out centipedePosition);
                var centipede = Instantiate(_centipede, centipedePosition, Quaternion.identity);
                centipede.transform.SetParent(_centipedeParent);
                centipede.GetComponent<CentipedeController>().SetDirectionCentipede(-1);
                centipede.GetComponent<CentipedeController>().indexCentipede = indexCentipede;
                indexCentipede += 1;
                if (i == remain && !_hasHead)
                {
                    centipede.transform.Find("Head").gameObject.SetActive(true);
                    _hasHead = true;
                }
            }

            amountCentipede = Grid.Instance().GetMaxX();
        }
        for (int i = amountCentipede; i > 0; i--)
        {
            Vector3 centipedePosition = Vector3.zero;
            Grid.Instance().GetPositionGrid(i, Grid.Instance().GetMaxY(), out centipedePosition);
            var centipede = Instantiate(_centipede, centipedePosition, Quaternion.identity);
            centipede.transform.SetParent(_centipedeParent);
            centipede.GetComponent<CentipedeController>().indexCentipede = indexCentipede;
            indexCentipede += 1;
            if (i == amountCentipede && !_hasHead)
            {
                centipede.transform.Find("Head").gameObject.SetActive(true);
                _hasHead = true;
            }

        }

    }

    void CreateMushroom(int loop)
    {
        //Loop
        for (int i = 0; i < loop; i++)
        {
            Vector3 position = Grid.Instance().GetRandomPosition();
            RaycastHit2D hit = Physics2D.Raycast(position, Vector3.zero);
            if (hit.collider == null)
            {
                var mushroom = Instantiate(_mushroom, position, Quaternion.identity);
                mushroom.transform.SetParent(_mushroomParent);
            }
        }
        
    }

    void SpawnMushroom(Vector3 position)
    {
        var mushroom = Instantiate(_mushroom, position, Quaternion.identity);
    }

    public void SetEventOnFireCentipede(int index)
    {
        onFireCentipede(index);
    }

    void CentipedeKilled(int index)
    {
        //var centipede = GameObject.FindGameObjectsWithTag("Centipede");
        //for(int i =0;i < centipede.Length; i++)
        //{
        //    var controller = centipede[i].GetComponent<CentipedeController>();
        //    if (controller.indexCentipede < index)
        //    {
        //        controller.SetDirectionCentipede(-1);
        //    }
        //    else 
        //    {
        //        controller.SetDirectionCentipede(1);
        //    }
        //}
    }
}
