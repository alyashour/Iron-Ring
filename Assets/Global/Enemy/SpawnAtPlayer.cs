using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtPlayer : MonoBehaviour
{
    [SerializeField] GameObject GenericEnemyPrefab;

    public static int numActive = 0;
    public static bool allowSpawns = true;
    // Timer
    private float timeOfLastSpawn;
    private float spawnDelay = 7.5f;
    private void Start()
    {
        allowSpawns = true;
    }
    private void Update()
    {
        if (Time.time - timeOfLastSpawn > spawnDelay)
        {
            Spawn();
            timeOfLastSpawn = Time.time;
        }
    }
    private void Spawn()
    {
        if (numActive < 5 && allowSpawns)
        {
            numActive++;
            Instantiate(GenericEnemyPrefab, getPosition(), Quaternion.identity);
        }
    }
    private Vector3 getPosition()
    {
        float radius = 2.5f;
        float angle = Random.Range(0, Mathf.PI * 2f);
        float x = transform.position.x + Mathf.Cos(angle) * radius;
        float y = transform.position.y + Mathf.Sin(angle) * radius;
        return new Vector3(x, y, transform.position.z);
    }
}