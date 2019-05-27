using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableIfPC : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
