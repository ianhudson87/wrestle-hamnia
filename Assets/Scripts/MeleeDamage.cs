using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MeleeDamage : NetworkBehaviour
{
    public LayerMask layer_mask;
    public float radius = 0.6f;
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
        float boopMultiplier = 5;

        if(hitColliders.Length>0 && isServer){
            // print("isserver");
            if(GameManager.instance.gamestate == GameState.fight){
                GameObject hitPlayerObject = hitColliders[0].gameObject;
                switch(attackType){
                    case AttackType.light:
                        hitPlayerObject.GetComponent<PlayerState>().ApplyDamage(8);
                        boopMultiplier = 10;
                        break;
                    case AttackType.heavy:
                        hitPlayerObject.GetComponent<PlayerState>().ApplyDamage(3);
                        boopMultiplier = (hitPlayerObject.GetComponent<PlayerState>().maxHealth - hitPlayerObject.GetComponent<PlayerState>().health)/2;
                        boopMultiplier = Mathf.Max(boopMultiplier, 10);
                        break;
                }
                print("playername" + hitPlayerObject.GetComponent<PlayerState>().username);
                Vector3 attackerDirection = this.gameObject.transform.TransformDirection(Vector3.forward);
                Vector3 boopVector = attackerDirection;
                boopVector.y += 0.5f;
                BoopClientRpc(boopVector * boopMultiplier, hitPlayerObject);
                // playerObject.
                // playerObject.GetComponent<PlayerMove>().ApplyBoopFromServer(new Vector3(0, 10, 0));
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
