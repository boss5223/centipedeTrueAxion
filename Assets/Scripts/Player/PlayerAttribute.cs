using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    public static PlayerAttribute Instance;
    public int health;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void DecreaseHealth()
    {
        health -= 1;
        if(health <= 0)
        {
            //GameOver
        }
    }
}
