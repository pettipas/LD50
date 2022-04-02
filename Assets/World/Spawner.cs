using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Item prefab;

    public void Start() {
        StartCoroutine(SpawnItem());
    }
    public IEnumerator SpawnItem(){
        yield return new WaitForSeconds(Random.Range(3, 7));
        Instantiate(prefab, transform.position, Quaternion.identity);
        StartCoroutine(SpawnItem());
        yield break;
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
