using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MeleeDamage : NetworkBehaviour
{
    public LayerMask layer_mask;
    public float radius = 0.3f;
    public float damage = 1f;
    [SerializeField] GameObject meleeHitDetector;
    Transform meleeDetectorTransform;

    void Start(){
        meleeDetectorTransform = meleeHitDetector.GetComponent<Transform>();
    }

    public void AttemptHit(){
        // print("here");
        Collider[] hitColliders = Physics.OverlapSphere(meleeDetectorTransform.position, radius, layer_mask);
        print(hitColliders.Length);
        if(hitColliders.Length>0 && isServer){
            // print("isserver");
            if(GameManager.instance.gamestate == GameState.fight){
                hitColliders[0].gameObject.GetComponent<PlayerState>().ApplyDamage(12);
            }
        }
    }
}
