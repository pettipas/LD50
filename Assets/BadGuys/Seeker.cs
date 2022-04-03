using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : MonoBehaviour
{
    public NavMeshAgent NavAgent;
    public GameObject target;
    public Transform body;

    public void Update() {
        NavAgent.updateRotation = false;
        if(Player.Instance != null) {
            var dir = NavAgent.velocity.normalized;
            if(dir != Vector3.zero ){
                body.forward = dir;
            }
            NavAgent.SetDestination(Player.Instance.transform.position);
        }
    }
}
