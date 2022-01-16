using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KillOnTouch : NetworkBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collider) {
        if(isServer){
            if(collider.gameObject.tag == "Player"){
                collider.gameObject.GetComponent<PlayerState>().health = 0;
                collider.gameObject.GetComponent<PlayerState>().isAlive = false; // kill the player
                collider.gameObject.GetComponent<PlayerMove>().SetPosition(GameManager.instance.getResetPosition());
            }
        }
    }
}
