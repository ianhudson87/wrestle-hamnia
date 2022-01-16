using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject setupMenu, lobbyMenu, fightMenu, endMenu;

    void Update(){
        GameState gameState = gameManager.GetComponent<GameManager>().gamestate;

        setupMenu.SetActive(gameState == GameState.setup);
        fightMenu.SetActive(gameState == GameState.fight);
        endMenu.SetActive(gameState == GameState.end);
    }


}
