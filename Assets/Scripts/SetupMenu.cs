using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupMenu : MonoBehaviour
{
    [SerializeField] private GameObject start_game_button;
    // Start is called before the first frame update
    void Awake(){
        print(start_game_button);
        // start_game_button.GetComponent<Button>();
        start_game_button.GetComponent<Button>().onClick.AddListener(HandleStartGame);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleStartGame(){
        // print("handle click");
        GameManager.instance.UpdateGameState(GameState.fight);
    }
}
