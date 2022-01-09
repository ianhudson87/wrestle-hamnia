using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    // private PlayerState p_state;
    public int player_health = 33;
    // private GameObject slider;
    private Slider hbar;

    // Start is called before the first frame update
    void Start() {
        // hbar = (Slider) this;
        hbar = this.GetComponent<Slider>();
    }

    public void UpdateValue(int health) {
        print("here" + health);
        player_health = health;
        hbar.value = player_health;
    }
}
