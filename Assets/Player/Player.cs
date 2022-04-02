using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public Transform Body;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(x,0,z);
        transform.position += Speed * dir * Time.smoothDeltaTime;

        if(dir != Vector3.zero){
             Body.transform.forward = dir.normalized;
        }
    }
}
