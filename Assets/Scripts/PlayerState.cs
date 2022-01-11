using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public string username;
    public int health = 100;
    public bool blocking = false;
    public bool isAlive = true;
    [SerializeField] private GameObject health_bar;

    public void ApplyDamage(int damage){
        health -= damage;

        if(health <= 0){
            health = 0;
            isAlive = false;
        }

        UpdateHealthBar();
    }

    void UpdateHealthBar(){
        health_bar.GetComponent<HealthBar>().UpdateValue(health);
    }
}
