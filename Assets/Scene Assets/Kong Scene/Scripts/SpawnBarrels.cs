using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrells : MonoBehaviour
{
    public GameObject prefab;
    public float minTime = 2f;
    public float maxTime = 4f;

    private void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
    private void Start()
    {
        Spawn();
    }
}
