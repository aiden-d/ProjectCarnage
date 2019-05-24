using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public SelectionManager manager;
    public string characterPath;

    public Color normalColor;
    public Color selectedColor;
    public bool taken = false;
    public GameObject lockGO;
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
        if (taken) 
        {
            lockGO.SetActive(true);
        }
        else 
        {
            lockGO.SetActive(false);
        }
    }

    public void TryPress()
    {
        if (taken == false) 
        {
            manager.activeSelect = this;

        }




    }
}
