using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class PlayerState : NetworkBehaviour
{
    public string username;
    [SyncVar] public int health;
    public int maxHealth = 100;
    public bool blocking = false;
    public bool isAlive = true;
    // [SerializeField] private GameObject health_bar;
    public UnityEvent onHealthChange;

    public void Awake(){
        RestoreHealthToFull();
    }

    public void ApplyDamage(int damage){
        health -= damage;

        if(health <= 0){
            health = 0;
            isAlive = false;
        }

        // if(isLocalPlayer){
        //     UpdateHealthBar();
        // }
    }

    public void RestoreHealthToFull(){
        health = maxHealth;

        // if(isLocalPlayer){
        //     UpdateHealthBar();
        // }
    }

    // void UpdateHealthBar(){
    //     health_bar.GetComponent<HealthBar>().UpdateValue(health);
    // }

    public int GetHealth(){
        return health;
    }
}
