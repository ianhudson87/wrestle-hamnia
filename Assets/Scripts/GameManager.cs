using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gamestate;

    public static event Action<GameState> OnGameStateChanged;

    void Awake(){
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.lobby);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void UpdateGameState(GameState new_gamestate) {
        // print("here");
        switch(new_gamestate){
            case GameState.lobby:
                HandleLobby();
                break;
            case GameState.setup:
                HandleSetup();
                break;
            case GameState.fight:
                HandleFight();
                break;
            case GameState.end:
                HandleEnd();
                break;
            default:
                throw new ArgumentOutOfRangeException("bad");
        }
        OnGameStateChanged?.Invoke(new_gamestate);
    }

    public void UpdateGameState(string new_gamestate) {
        GameState e_newGamestate;
        switch(new_gamestate){
            case "lobby":
                e_newGamestate = GameState.lobby;
                break;
            case "setup":
                e_newGamestate = GameState.setup;
                break;
            case "fight":
                e_newGamestate = GameState.fight;
                break;
            case "end":
                e_newGamestate = GameState.end;
                break;
            default:
                throw new ArgumentOutOfRangeException("bad");
        }
        UpdateGameState(e_newGamestate);
    }

    void HandleLobby(){
        gamestate = GameState.lobby;
    }

    void HandleSetup(){
        gamestate = GameState.setup;
    }

    void HandleFight(){
        gamestate = GameState.fight;
    }

    void HandleEnd(){
        gamestate = GameState.end;
    }
}

public enum GameState{
    lobby,
    setup,
    fight,
    end,
}