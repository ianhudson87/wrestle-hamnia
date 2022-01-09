using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{

    public Camera cam;
    public GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer){
            cam.gameObject.SetActive(true);
            ui.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
