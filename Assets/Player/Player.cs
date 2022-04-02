using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{
    public float Speed;
    public Transform Body;
    public NavMeshAgent Agent;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(x,0,z);
        Agent.Move(Speed * dir * Time.smoothDeltaTime);

        if(dir != Vector3.zero){
             Body.transform.forward = dir.normalized;
        }
    }
}
