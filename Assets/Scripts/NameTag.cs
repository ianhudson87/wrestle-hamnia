using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTag : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject nameTagDisplay;
    private bool foundDisplayName = false;


    void Update(){
        if( !foundDisplayName ){
            // display name hasn't been set yet
            // print("name" + playerObj.GetComponent<PlayerState>().username);
            if( !string.IsNullOrEmpty(playerObj.GetComponent<PlayerState>().username) ) {
                print("SET" + playerObj.GetComponent<PlayerState>().username + "!");
                // username of player has been set already

                foundDisplayName = true;
                nameTagDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = playerObj.GetComponent<PlayerState>().username;
            }
            
        }
    }

}
