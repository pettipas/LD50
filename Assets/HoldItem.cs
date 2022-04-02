using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
    public Item Held;
    public LayerMask itemMask;
    public Transform carryPoint;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && Held == null) {
            Collider[] hits = Physics.OverlapSphere(transform.position, 1.0f, itemMask);
            if(hits.Length > 0){
                Held = hits[0].GetComponentInParent<Item>();
                Held.transform.SetParent(carryPoint, true);
                Held.transform.position = carryPoint.transform.position;
                Held.GetComponent<FollowVectorBlocks>().enabled = false;
            }
        } else if(Held != null && Input.GetKeyDown(KeyCode.Return)) {
            Held.GetComponent<FollowVectorBlocks>().enabled = true;
            Held.transform.parent = null;
            Held = null;
        }
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(carryPoint.position,Vector3.one/3.0f);
    }
}
