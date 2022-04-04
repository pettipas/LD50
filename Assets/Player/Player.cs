using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float Speed;
    public Transform Body;
    public NavMeshAgent Agent;

    public static Player Instance;

    public Animator Animator;

    public Vector3 CompassDirs;

    public HoldItem holdItem;
    
    public int health; 

    public LayerMask enemyMask;

    public void Awake() {
        Instance = this;
    }
    public void Death(){
        health = 0;
    }
    public float  timer;

    public Text healthInd;

    public ParticleSystem bloodLoss;

    public PlayerDeath death;
    
    public bool dead;
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 0.7f){
            timer = 0;
            Collider[] hits = Physics.OverlapSphere(transform.position, 1.5f, enemyMask);
            if(hits.Length > 0){
                health -= 1;
                bloodLoss.Emit(3);
            }
           
        }

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

        string h = "";
        for (int i = 0; i < health; i++)
        {
             h += "||";
        }

        healthInd.text = h;
    }

    public void LateUpdate(){

        if(health <= 0 && !dead){
            dead = true;
            var deth = death.Duplicate(transform.position, Quaternion.identity);
            deth.DoExplode(this.gameObject,1.0f);
        }
    }
}
