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

    public void AttemptHit(AttackType attackType){
        // print("here");
        Collider[] hitColliders = Physics.OverlapSphere(meleeDetectorTransform.position, radius, layer_mask);
        print(hitColliders.Length);
        if(hitColliders.Length>0 && isServer){
            // print("isserver");
            if(GameManager.instance.gamestate == GameState.fight){
                switch(attackType){
                    case AttackType.light:
                        hitColliders[0].gameObject.GetComponent<PlayerState>().ApplyDamage(10);
                        break;
                    case AttackType.heavy:
                        GameObject playerObject = hitColliders[0].gameObject;
                        playerObject.GetComponent<PlayerState>().ApplyDamage(20);
                        print("playername" + playerObject.GetComponent<PlayerState>().username);
                        BoopClientRpc(new Vector3(0, 10, 0), playerObject);
                        // playerObject.
                        // playerObject.GetComponent<PlayerMove>().ApplyBoopFromServer(new Vector3(0, 10, 0));
                        break;
                }
            }
        }
    }

    [ClientRpc] void BoopClientRpc(Vector3 boop, GameObject playerObject){
        print("got boop message");
        print("playername" + playerObject.GetComponent<PlayerState>().username);
        playerObject.GetComponent<PlayerMove>().ApplyBoop(boop);
    }
}

public enum AttackType{
    light,
    heavy
}
