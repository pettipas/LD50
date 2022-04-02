using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : MonoBehaviour
{
    public NavMeshAgent NavAgent;
    public GameObject target;
    public void Update() {
        NavAgent.updateRotation = false;
        if(Player.Instance != null) {
            NavAgent.SetDestination(Player.Instance.transform.position);
        }
    }
}
