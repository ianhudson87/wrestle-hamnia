using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class LocalLobby : MonoBehaviour
{
    [SerializeField] GameObject networkManagerObj;
    NetworkManager networkManager;
    [SerializeField] GameObject gameManagerObj;
    GameManager gameManager;
    [SerializeField] GameObject hostButton;
    [SerializeField] GameObject joinButton;
    [SerializeField] GameObject joinInput;
    [SerializeField] GameObject lobbyButtons;

    void Awake(){
        networkManager = networkManagerObj.GetComponent<NetworkManager>();
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HostLocalGame(){
        networkManager.StartHost();
        gameManager.UpdateGameState(GameState.setup);
        lobbyButtons.SetActive(false);
    }

    public void JoinLocalGame(){
        networkManager.networkAddress = joinInput.GetComponent<InputField>().text;
        networkManager.StartClient();
        lobbyButtons.SetActive(false);
    }
}
