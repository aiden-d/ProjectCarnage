﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public SelectionManager manager;
    public string characterPath;

    public Color normalColor;
    public Color selectedColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.activeSelect == this)
        {
            GetComponent<Image>().color = selectedColor;

        }
        else 
        {
            GetComponent<Image>().color = normalColor;
        }
    }

    public void TryPress()
    {

        manager.activeSelect = this;

        
    
    }
}
