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
        UpdateGameState(GameState.setup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameState(GameState new_gamestate) {
        // print("here");
        switch(new_gamestate){
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
        // print("ere2");
        OnGameStateChanged?.Invoke(new_gamestate);
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
    setup,
    fight,
    end,
}