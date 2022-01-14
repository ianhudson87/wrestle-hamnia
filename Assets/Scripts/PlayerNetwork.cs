using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{

    public Camera cam;
    public GameObject ui;



    void Start()
    {
        if(isLocalPlayer){
            cam.gameObject.SetActive(true);
            ui.SetActive(true);
            CmdSendNameToServer(NetworkManagerWIT.singleton.localUsername, this.gameObject);
        }
        
    }

    [Command] void CmdSendNameToServer(string name, GameObject playerObj){
        RpcSetNameOnClients(name, playerObj);
    }

    [ClientRpc] void RpcSetNameOnClients(string name, GameObject playerObj){
        playerObj.GetComponent<PlayerState>().username = name;
    }
}
