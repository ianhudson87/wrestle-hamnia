using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject winnerDisplayText;
    // Start is called before the first frame update

    void Update(){
        GameObject currentWinner = GameManager.instance.GetComponent<GameManager>().currentWinner;
        if(currentWinner){
            winnerDisplayText.GetComponent<Text>().text = string.Format("The winner is {0}!", currentWinner.GetComponent<PlayerState>().username);
        }
    }
}
