using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    // private PlayerState p_state;
    // private GameObject slider;
    private Slider hbar;
    [SerializeField] private GameObject playerObject;

    // Start is called before the first frame update
    void Start() {
        int initialHealth = playerObject.GetComponent<PlayerState>().health; // initialize healthbar value

        hbar = this.GetComponent<Slider>();
        hbar.maxValue = initialHealth;

        UpdateValue(initialHealth);
    }

    public void UpdateValue(int health) {
        hbar.value = health;
    }
}
