using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public LayerMask playerAndEnemy;
    public ParticleSystem partSys;
    public GameObject destroOrigin;

    public void DoExplode(GameObject destro, float radius){
        destroOrigin = destro;
        StartCoroutine(_Explode(radius));
    }

    public IEnumerator _Explode(float radius){
        Destroy(destroOrigin);
        var exphits = Physics.OverlapSphere(transform.position, radius, playerAndEnemy);
        if(partSys != null){
            partSys.Emit(10);
        }
        foreach(var e in exphits) {
            Seeker seeker = e.transform.GetComponentInParent<Seeker>();
            Player player = e.transform.GetComponentInParent<Player>();
            if(seeker != null)
                seeker.Death();
            if(player != null)
                player.Death();
        }
        yield return new WaitForSeconds(2.3f);

        Destroy(this.gameObject);
        yield break;
    }
}
