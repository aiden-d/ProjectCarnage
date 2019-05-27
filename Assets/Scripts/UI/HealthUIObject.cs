using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class HealthUIObject : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeHealth(float health) 
    {
        healthText.text = health + "X";
    }
}
