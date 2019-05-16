using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public string playerTag;
    public GameObject localPlayer;
    InputManager im;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(wait());

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator wait() 
    {
        yield return new WaitForSeconds(1);

        GameObject[] go = GameObject.FindGameObjectsWithTag(playerTag);

        foreach (GameObject g in go)
        {
            if (g.GetComponent<PhotonView>().IsMine)
            {
                localPlayer = g;

            }
        }
        im = localPlayer.GetComponent<InputManager>();

    }



    public void Attack() 
    {
        im.Attack();
    }
    public void AttackSlow() 
    {
        im.AttackSlow();
    }
    public void Roll() 
    {
        im.Roll();
    }
    public void BlockDown() 
    {
        im.Block();
    }
    public void StopBlock() 
    {
        Debug.Log("up");
        im.StopBlock();
    }
    
}
