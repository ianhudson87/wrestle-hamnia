using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : MonoBehaviour
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
        
    }
}
