using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : NetworkBehaviour
{
    private CharacterAnimations player_animations;

    // Start is called before the first frame update
    void Awake()
    {
        player_animations = GetComponent<CharacterAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer && GetComponent<PlayerState>().isAlive){
            if(Input.GetMouseButtonDown(0)){
                player_animations.Attack_1();
            }
            else if(Input.GetMouseButtonDown(1)){
                player_animations.Attack_2();
            }
            // if(Input.GetKeyDown(KeyCode.J)){
            //     player_animations.Defend(true);
            // }

            // if(Input.GetKeyUp(KeyCode.J)){
            //     player_animations.UnfreezeAnimation();
            //     player_animations.Defend(false);
            // }


            // if(Input.GetKeyDown(KeyCode.K)) {
            //     if(Random.Range(0, 2) > 0){
            //         player_animations.Attack_1();
            //     }
            //     else{
            //         player_animations.Attack_2();
            //     }
            // }
        }
    }

    // void ActivateMeleeDetector(){
    //     melee_hit_detector.SetActive(true);
    // }
    // void DeactivateMeleeDetector(){
    //     melee_hit_detector.SetActive(false);
    // }
}
