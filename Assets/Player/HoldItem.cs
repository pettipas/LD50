using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
    public Item Held;
    public LayerMask itemMask;
    public Transform carryPoint;
    public Item Detected;

    public GameObject carryArms;

    public GameObject gunCarryArms;

    public Transform carryBody;
    

    public bool Holding{
        get{
            return Held != null;
        }
    }

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
            if(Held != null && Held.IsGun)
                Held.gameObject.SetActive(true);
            Held = null;
            Detected = null;
        } else if(Held != null && Input.GetKeyDown(KeyCode.Space)){
            if(!Held.IsGun){
                
                Held.transform.parent = null;
                Held.Throw(carryBody.forward.normalized, 15);
                Held = null;
                Detected = null;
            } else {
                var ammo = Held.Shoot(carryBody.forward.normalized);
                if(ammo <= 0){
                    Held.gameObject.SetActive(true);
                }
            }
  
        }

        if(Held != null){
            if(Held.IsGun) {
                Held.gameObject.SetActive(false);
                gunCarryArms.SetActive(true);
                carryArms.SetActive(false);
            } else {
                gunCarryArms.SetActive(false);
                carryArms.SetActive(true);
            }
        } else {
           gunCarryArms.SetActive(false);
           carryArms.SetActive(false);
        }
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(carryPoint.position,Vector3.one/3.0f);
    }
}
