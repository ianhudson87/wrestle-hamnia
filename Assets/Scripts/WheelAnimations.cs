using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimations : MonoBehaviour
{

    private static Animator anim = null;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     Spin();
        // }
        // else if(Input.GetKeyUp(KeyCode.Space)){
        //     StopSpin();
        // }
        
    }

    public static void Spin() {
        anim.SetBool("Spin", true);
    }

    public static void StopSpin() {
        anim.SetBool("Spin", false);
    }
}
