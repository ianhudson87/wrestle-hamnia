using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public LayerMask layer_mask;
    public float radius = 0.1f;
    public float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layer_mask);
        if(hitColliders.Length>0){
            hitColliders[0].gameObject.GetComponent<PlayerState>().ApplyDamage(12);
        }
    }
}
