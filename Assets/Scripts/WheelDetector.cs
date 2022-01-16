using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WheelDetector : NetworkBehaviour
{
    [SyncVar] List<GameObject> playersOnWheel = new List<GameObject>();
    private WheelAnimations wheelAnimator;
    
    
    void Start(){
        wheelAnimator = GetComponent<WheelAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        // print(playersOnWheel[0]);
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
}
