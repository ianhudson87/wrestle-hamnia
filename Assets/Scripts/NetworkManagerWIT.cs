using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using Mirror.FizzySteam;
using kcp2k;

public class NetworkManagerWIT : NetworkManager
{
    [SerializeField] private GameObject nameInputField;
    public string localUsername; // username of local player. Will automatically get grabbed when set.

    new public static NetworkManagerWIT singleton;

    public override void Awake()
    {
        base.Awake();
        singleton = this;
    }

    public void SetTransport(TransportType newTransportType){
        switch(newTransportType){
            case TransportType.steam:
                this.transport = GetComponent<FizzySteamworks>();
                break;
            case TransportType.kcp:
                this.transport = GetComponent<KcpTransport>();
                break;
            default:
                break;
        }
    }

    public void SetTransport(string newTransportType){
        switch(newTransportType){
            case "steam":
                SetTransport(TransportType.steam);
                break;
            case "kcp":
                SetTransport(TransportType.kcp);
                break;
            default:
                break;
        }
    }

    // public override void OnServerAddPlayer(NetworkConnection conn) {
    //     Transform startPos = GetStartPosition();
    //     GameObject player = startPos != null
    //         ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
    //         : Instantiate(playerPrefab);

    //     player.GetComponent<PlayerState>().username = nameInputField.GetComponent<InputField>().text; // give new player object the name that was input

    //     // instantiating a "Player" prefab gives it the name "Player(clone)"
    //     // => appending the connectionId is WAY more useful for debugging!
    //     player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
    //     NetworkServer.AddPlayerForConnection(conn, player);



        // var player = (GameObject)GameObject.Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
        // NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    // }

    public void SetLocalUsernameFromNameInput(){
        localUsername = nameInputField.GetComponent<InputField>().text;
    }

    public void SetLocalUsername(string username){
        localUsername = username;
    }
}

public enum TransportType{
    steam,
    kcp
}