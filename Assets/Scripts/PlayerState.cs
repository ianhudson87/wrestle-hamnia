using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int health = 100;
    public bool blocking = false;
    public GameObject health_bar;

    // Start is called before the first frame update
    void Start()
    {
        // health_bar = transform.Find("UI").Find("HealthBar").gameObject;
        // print("this");
        // print("health_bar" + health_bar);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyDamage(int damage){
        health -= damage;
        UpdateHealthBar();
    }

    void UpdateHealthBar(){
        health_bar.GetComponent<HealthBar>().UpdateValue(health);
    }
}
