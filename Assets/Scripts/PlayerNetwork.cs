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
            CmdSendNameToServer(NetworkManagerWIT.singleton.localUsername);
        }
        
    }

    [Command] void CmdSendNameToServer(string name){
        RpcSetNameOnClients(name);
    }

    [ClientRpc] void RpcSetNameOnClients(string name){
        this.gameObject.GetComponent<PlayerState>().username = name;
    }
}
