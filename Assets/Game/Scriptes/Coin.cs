using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _coinAudio;

    //check for collision
    private void OnTriggerStay(Collider other)
    {
        //check if Player
        if (other.tag == "Player")
        {
            //Press E key to take coin
            if (Input.GetKeyDown(KeyCode.E))
            {
                //play coin sound and finally destroy th coin
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    player.hasCoin = true;
                    AudioSource.PlayClipAtPoint(_coinAudio, transform.position, 1f);
                    UIManager uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

                    if (uiManager != null)
                    {
                        uiManager.CollectedCoin();
                    }
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
