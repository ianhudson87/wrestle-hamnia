using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    [SyncVar] public GameState gamestate;

    public static event Action<GameState> OnGameStateChanged;

    [SyncVar] public GameObject currentWinner = null; // player object of the current winner

    [SyncVar] public float roundTimer = 0f;

    void Awake(){
        instance = this;
        UpdateGameState(GameState.lobby);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GetAllPlayers();
        if(players.Length <= 0) { return; }

        int num_players_alive = 0;
        GameObject lastAlivePlayerSeen = null;
        foreach (GameObject player in players){
            if(player.GetComponent<PlayerState>().isAlive){
                num_players_alive++;
                lastAlivePlayerSeen = player;
            }
        }

        if(num_players_alive <= 1 && gamestate == GameState.fight){
            currentWinner = lastAlivePlayerSeen;
            UpdateGameState(GameState.end);
        }


        if(gamestate == GameState.fight){
            roundTimer += Time.deltaTime;
        }

    }

    public void UpdateGameState(GameState new_gamestate) {
        if (!isServer) { return; }
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

        roundTimer = 0f;

        GameObject[] players = GetAllPlayers();
        foreach (GameObject player in players){
            player.GetComponent<PlayerState>().RestoreHealthToFull();
        }
    }

    void HandleFight(){
        gamestate = GameState.fight;
    }

    void HandleEnd(){
        gamestate = GameState.end;
    }

    GameObject[] GetAllPlayers(){
        return GameObject.FindGameObjectsWithTag("Player");
    }
}

public enum GameState{
    lobby,
    setup,
    fight,
    end,
}