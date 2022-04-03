using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask mask;
    public Explode impact;
    public Vector3 direction;
    public float Speed;
    public float timer;
    public void Update() {
        timer += Time.deltaTime;
        
        if(timer > 4.0f){
            Destroy(this.gameObject);
            return;
        }
        transform.position += direction * Speed * Time.smoothDeltaTime;    
        var enemyHits = Physics.OverlapSphere(transform.position, 1.0f, mask);
        if(enemyHits.Length > 0){
            var enemy = enemyHits[0];
            var seek = enemy.transform.GetComponentInParent<Seeker>();
            seek.Death();
            var exp = impact.Duplicate(transform.position, Quaternion.identity);
            exp.DoExplode(this.gameObject, 1.0f);
        }
    }
}
