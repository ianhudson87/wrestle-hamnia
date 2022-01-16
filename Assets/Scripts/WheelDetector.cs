using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WheelDetector : NetworkBehaviour
{
    [SyncVar] List<GameObject> playersOnWheel = new List<GameObject>();
    private WheelAnimations wheelAnimator;
    [SerializeField] private int healPeriod = 1;
    [SerializeField] private int healAmountPerPeriod = 5;
    private float nextHeal = 0;
    
    
    void Start(){
        wheelAnimator = GetComponent<WheelAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer){
            if (Time.time > nextHeal){
                HealPlayers(playersOnWheel, healAmountPerPeriod);
                nextHeal += healPeriod;
            }
        }
    }

    void OnTriggerEnter(Collider collider) {
        if(isServer){
            if(collider.gameObject.tag == "Player"){
                playersOnWheel.Add(collider.gameObject);
            }
            SetWheelAnimation();
        }
    }

    void OnTriggerExit(Collider collider) {
        if(isServer){
            if(collider.gameObject.tag == "Player"){
                bool removed = playersOnWheel.Remove(collider.gameObject);
                if(!removed){
                    print("something wrong with wheeldetector.cs");
                }
            }
            SetWheelAnimation();
        }
    }

    void SetWheelAnimation(){
        if(playersOnWheel.Count == 0){
            wheelAnimator.StopSpin();
        }
        else{
            wheelAnimator.Spin();
        }
    }

    void HealPlayers(List<GameObject> players, int healAmount){
        foreach(GameObject player in players){
            player.GetComponent<PlayerState>().Heal(healAmount);
        }
    }
}
