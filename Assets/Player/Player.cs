using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{
    public float Speed;
    public Transform Body;
    public NavMeshAgent Agent;

    public static Player Instance;

    public Animator Animator;

    public Vector3 CompassDirs;

    public HoldItem holdItem;

    public void Awake(){
        Instance = this;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(x,0,z);
        Agent.Move(Speed * dir * Time.smoothDeltaTime);

        if(dir != Vector3.zero){
            Body.transform.forward = dir.normalized;
        }
        
        var cpd = dir.Round();
        var posfix = holdItem.Holding ? "_na" : "";
        if(cpd == Vector3.back){
            CompassDirs = cpd;
            Animator.SafePlay("player_south" + posfix);
        } else if(cpd == Vector3.forward){
            CompassDirs = cpd;
            Animator.SafePlay("player_north" + posfix);
        }else if(cpd == Vector3.left){
            CompassDirs = cpd;
            Animator.SafePlay("player_east" + posfix);
        }else if(cpd == Vector3.right){
            CompassDirs = cpd;
            Animator.SafePlay("player_west" + posfix);
        } else if(cpd == Vector3.zero){
            CompassDirs = cpd;
            Animator.SafePlay("player_rest" + posfix);
        }
    }
}
