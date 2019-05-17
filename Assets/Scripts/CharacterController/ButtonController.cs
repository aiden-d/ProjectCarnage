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

        GetPlayer();

    }



    public void Attack() 
    {
        GetPlayer();
        im.Attack();

    }

    
    public void AttackSlow() 
    {
        GetPlayer();
        im.AttackSlow();
    }
    public void Roll() 
    {
        GetPlayer();
        im.Roll();
    }
    public void BlockDown() 
    {
        GetPlayer();
        im.Block();
    }
    public void StopBlock() 
    {
        GetPlayer();
        
        im.StopBlock();
    }

    public void GetPlayer()
    {
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
}
