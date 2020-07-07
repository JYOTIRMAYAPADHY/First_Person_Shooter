using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharkshop : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        //check for the player
        if (other.tag == "Player")
        {
            //press button E
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player!= null)
                {
                    //if player has coin
                    if (player.hasCoin == true)
                    {
                        player.hasCoin = false;
                        UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                        //update the inventory display
                        if (uiManager!= null)
                        {
                            uiManager.RemoveCoin();
                        }
                        //play the audio
                        AudioSource audio = GetComponent<AudioSource>();
                        audio.Play();
                        player.EnableWeapon();
                    }
                    else
                    {
                        Debug.Log("Get Out of Here");
                    }
                }
            }
        }       
    }
}
