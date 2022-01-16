using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTag : MonoBehaviour
{
    [SerializeField] GameObject HealthTagDisplay;
    [SerializeField] GameObject PlayerObj;
    private PlayerState playerState;

    void Start() {
        playerState = PlayerObj.GetComponent<PlayerState>();
    }
    void Update()
    {
        HealthTagDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = playerState.health.ToString();
    }
}
