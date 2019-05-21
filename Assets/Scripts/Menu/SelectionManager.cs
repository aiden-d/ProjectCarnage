using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ES3Types;

public class SelectionManager : MonoBehaviour
{

    public CharacterSelect activeSelect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void saveSelection() 
    {
        ES3.Save<string>("character", activeSelect.characterPath);
    }


}
