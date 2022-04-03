using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileState : MonoBehaviour
{
    public LayerMask enemyMask;
    public float Radius;
    public bool explode;
    public float timer;
    public float tte;
    public Explode expPrefab;
    void Update()
    {
        timer+= Time.deltaTime;
        if(timer > tte && !explode){
            explode = true;
            var exp = expPrefab.Duplicate(transform.position,Quaternion.identity);
            exp.DoExplode(this.gameObject, 3.0f);
        }
    }
    
}
