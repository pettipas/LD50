using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVectorBlocks : MonoBehaviour
{
    public VectorBlock vectorBlock;
    public LayerMask conveyorMask;
    public Transform testPoint;

    public void Update() {
        if(vectorBlock != null){
            transform.position += vectorBlock.Direction * vectorBlock.Speed * Time.smoothDeltaTime;
        }
        
        var hits = Physics.OverlapSphere(testPoint.position, 0.2f, conveyorMask);
        if(hits.Length > 0 ){
            var vb = hits[0].GetComponentInParent<VectorBlock>();
            vectorBlock = vb;
        } else {
            vectorBlock = null;
        }
    }
}
