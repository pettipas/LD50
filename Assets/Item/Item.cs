using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public LayerMask fallZone;
    public HeldState heldState;

    public bool Held {
        get{
            return heldState.enabled;
        }
    }

    public void Update()
    {
        var hits = Physics.OverlapSphere(transform.position, 0.1f, fallZone);
        if(hits.Length > 0 && !heldState.enabled ){
            Fall((hits[0].transform.position - transform.position).normalized);
        } 
    }

    public void Fall(Vector3 fallDir)
    {
        var fv = GetComponentInChildren<FollowVectorBlocks>().enabled = false;
        var rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(fallDir*2.0f, ForceMode.Impulse);
        this.enabled = false;
    }
}
