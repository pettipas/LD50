using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVectorBlocks : MonoBehaviour
{
    public VectorBlock vectorBlock;
    public LayerMask conveyorMask;
    public Transform testPoint;
    public float detectionRadius;

    public void Update() {
        if(vectorBlock != null) {
            transform.position += vectorBlock.Direction * vectorBlock.Speed * Time.smoothDeltaTime;
        }
        
        var hits = Physics.OverlapSphere(testPoint.position, detectionRadius, conveyorMask);
        if(hits.Length > 0 ){
            var vb = hits[0].GetComponentInParent<VectorBlock>();
            vectorBlock = vb;
        } else {
            vectorBlock = null;
        }
    }

    public void OnDrawGizmos() {

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(testPoint.position, detectionRadius);
    }
}
