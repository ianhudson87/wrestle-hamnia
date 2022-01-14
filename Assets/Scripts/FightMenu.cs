using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FightMenu : MonoBehaviour
{
    [SerializeField] private GameObject roundTimerDisplay;

    void Update(){
        roundTimerDisplay.GetComponent<Text>().text = "Round Time: " + GameManager.instance.roundTimer.ToString();
    }
}
