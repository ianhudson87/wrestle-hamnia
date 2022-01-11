using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class NetworkManagerWIT : NetworkManager
{
    [SerializeField] private GameObject nameInputField;

    void SetTransport(Transport newTransport){
        this.transport = newTransport;
    }

    public override void OnServerAddPlayer(NetworkConnection conn) {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        player.GetComponent<PlayerState>().username = nameInputField.GetComponent<InputField>().text; // give new player object the name that was input

        // instantiating a "Player" prefab gives it the name "Player(clone)"
        // => appending the connectionId is WAY more useful for debugging!
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, player);



        // var player = (GameObject)GameObject.Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
        // NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
