using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
    public Item Held;
    public LayerMask itemMask;
    public Transform carryPoint;
    public Item Detected;
    public void Update()
    {
        // this should feel better because we give the player lots of frames to detect the item
        Collider[] hits = Physics.OverlapSphere(transform.position, 1.0f, itemMask);
        if(hits.Length > 0) {
            Detected = hits[0].GetComponentInParent<Item>();
        } else {
            Detected = null;
        }

        if(Input.GetKeyDown(KeyCode.Return) 
        && Held == null 
        && Detected != null 
        && Detected.enabled 
        && !Detected.Held) {
            Held = Detected;
            Held.transform.SetParent(carryPoint, true);
            Held.transform.position = new Vector3(carryPoint.transform.position.x,this.transform.position.y,carryPoint.transform.position.z);
            Held.GetComponent<FollowVectorBlocks>().enabled = false;
            Held.GetComponent<HeldState>().enabled = true;
            Detected = null;
           
        } else if(Held != null && Input.GetKeyDown(KeyCode.Return)) {
            Held.GetComponent<FollowVectorBlocks>().enabled = true;
            Held.GetComponent<HeldState>().enabled = false; 
            Held.transform.parent = null;
            Held = null;
            Detected = null;
        }
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(carryPoint.position,Vector3.one/3.0f);
    }
}
