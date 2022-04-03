using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public LayerMask fallZone;
    public HeldState heldState;
    public ProjectileState projectile;
    public bool IsGun;
    public int ammo;
    public Bullet bullet;
    public bool isProjectile;

    public Transform shootPoint;

    public bool Held {
        get{
            return heldState.enabled;
        }
    }

    public bool IsProjectile {
        get{
            return projectile.enabled;
        }
    }

    public void Update()
    {
        var hits = Physics.OverlapSphere(transform.position, 0.1f, fallZone);
        if(hits.Length > 0 && !heldState.enabled ){
            Fall((hits[0].transform.position - transform.position).normalized);
        } 
    }

    public int Shoot(Vector3 shootDir){
        if(ammo > 0) {
           ammo--;
           var bu = bullet.Duplicate(shootPoint.position);
           bu.direction = shootDir.normalized;
        } else {
            IsGun = false;
        }
        return ammo;
    }

    public void Fall(Vector3 fallDir)
    {
        SceneData.Instance.Current++;
        var fv = GetComponentInChildren<FollowVectorBlocks>().enabled = false;
        var rb = this.gameObject.AddComponent<Rigidbody>();
        heldState.enabled = false;
        rb.AddForce(fallDir*4.0f, ForceMode.Impulse);
        this.enabled = false;
    }

    public void Throw(Vector3 direction, float power)
    {
        var fv = GetComponentInChildren<FollowVectorBlocks>().enabled = false;
        var rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(direction*power, ForceMode.Impulse);
        this.enabled = false;
        heldState.enabled = false;
        projectile.enabled = true;
    }
}
