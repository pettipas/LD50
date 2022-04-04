using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public ParticleSystem partSys;
    public GameObject destroOrigin;

    public GameObject skull;
    public void DoExplode(GameObject destro, float radius){
        destroOrigin = destro;
        StartCoroutine(_Explode(radius));
    }

    public IEnumerator _Explode(float radius){
        skull.Duplicate(destroOrigin.transform.position + new Vector3(0,1.0f,0));
        Destroy(destroOrigin);
        partSys.Emit(50);
        yield return new WaitForSeconds(2.3f);
        Destroy(this.gameObject);
        SceneData.Instance.Current = SceneData.Instance.Quota;
        yield break;
    }
}
