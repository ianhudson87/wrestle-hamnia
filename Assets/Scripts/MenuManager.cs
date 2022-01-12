using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject GameSetupMenu;
    [SerializeField] private GameObject GameEndMenu;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.OnGameStateChanged += HandleGameStateChange;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleGameStateChange(GameState new_gamestate){
        // print("here");
        // print(new_gamestate == GameState.setup);
        GameSetupMenu.SetActive(new_gamestate == GameState.setup);
        GameEndMenu.SetActive(new_gamestate == GameState.end);
    }
}
