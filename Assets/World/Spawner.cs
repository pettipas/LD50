using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float rangeStart;
    public float rangeEnd;
    public void Start() {
        StartCoroutine(SpawnItem());
    }
    public IEnumerator SpawnItem(){
        yield return new WaitForSeconds(Random.Range(rangeStart, rangeEnd));
        Instantiate(prefab, transform.position, Quaternion.identity);
        StartCoroutine(SpawnItem());
        yield break;
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
