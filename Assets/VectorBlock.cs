using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorBlock : MonoBehaviour
{
    public Transform _direction;
    public float Speed;    
    public Vector3 Direction {
        get {
            return _direction.forward.normalized;
        }
    }

    public void OnDrawGizmos(){

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_direction.position, _direction.forward * 0.5f);
        Gizmos.DrawSphere(_direction.position + _direction.forward * 0.5f, 0.1f);
    }
}
