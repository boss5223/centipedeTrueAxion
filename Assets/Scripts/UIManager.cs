﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text lifes;
    public Text score;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
