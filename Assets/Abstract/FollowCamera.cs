using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
   public Transform Target;
   public Vector3 vel;
   public float followTime;
   public float max;
   public void Update(){
       transform.position = Vector3.SmoothDamp(transform.position, new Vector3(Target.transform.position.x,transform.position.y, Target.transform.position.z),ref vel,followTime,max);
   }
}
