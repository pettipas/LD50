using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : MonoBehaviour
{
    public NavMeshAgent NavAgent;
    public GameObject target;
    public Transform body;
    public Explode explode;

    public void Death(){
        var epo = explode.Duplicate(transform.position, Quaternion.identity);
        epo.DoExplode(this.gameObject, 2.0f);
    }

    public void Update() {
        NavAgent.updateRotation = false;
        if(Player.Instance != null) {
            var dir = NavAgent.velocity.normalized;
            if(dir != Vector3.zero) {
                body.forward = dir;
            }
            NavAgent.SetDestination(Player.Instance.transform.position);
        }
    }
}
