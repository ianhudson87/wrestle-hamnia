using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject restart_game_button;
    // Start is called before the first frame update
    void Awake(){
        restart_game_button.GetComponent<Button>().onClick.AddListener(HandleRestartGame);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleRestartGame(){
        GameManager.instance.UpdateGameState(GameState.setup);
    }
}
