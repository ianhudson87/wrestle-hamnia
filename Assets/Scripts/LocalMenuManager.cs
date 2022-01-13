using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject setupMenu, lobbyMenu, endMenu;

    void Update(){
        GameState gameState = gameManager.GetComponent<GameManager>().gamestate;

        setupMenu.SetActive(gameState == GameState.setup);
        lobbyMenu.SetActive(gameState == GameState.lobby);
        endMenu.SetActive(gameState == GameState.end);
    }


}
