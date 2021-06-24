using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int health = 1;

    public void DecreaseHealthMushroom()
    {
        health += 1;
        gameObject.transform.localScale = new Vector3(1, 1f / health, 1);
        if(health > 3)
        {
            Destroy(gameObject);
        }
    }
}
