using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSpawnBehaviour : MonoBehaviour
{
    [SerializeField] GameObject golemPrefab;
    private Vector3 centerOfSpawnCircle;

    public static int numActive = 0;

    // Timer
    private float timeOfLastSpawn;
    private float spawnDelay = 7.5f;


    private void Start()
    {
        centerOfSpawnCircle = new Vector3(-0.25f, 0, 0);
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
        if (numActive < 5)
        {
            numActive++;
            Instantiate(golemPrefab, getPosition(), Quaternion.identity);
        }
        
    }
    private Vector3 getPosition()
    {
        float radius = Random.Range(0, 1f);
        float angle = Random.Range(0, Mathf.PI * 2f);
        float x = centerOfSpawnCircle.x + Mathf.Cos(angle) * radius;
        float y = centerOfSpawnCircle.y + Mathf.Sin(angle) * radius;
        return new Vector3(x, y, centerOfSpawnCircle.z);
    }
}