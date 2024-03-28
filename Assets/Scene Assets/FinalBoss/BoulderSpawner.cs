using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Aiden

public class BoulderSpawner : MonoBehaviour
{
    [SerializeField] GameObject boulderPrefab;
    [SerializeField] GameObject player;

    private SceneInitialization sceneinit;

    private float startTime;
    private float timeSinceLastSpawn = 0;

    [SerializeField] float spawnRate = 0.75f;

    private float spawnDistanceFromPlayer = 6.5f;

    private void Start()
    {
        startTime = Time.time;
        sceneinit = GameObject.Find("SceneController").GetComponent<SceneInitialization>();
    }

    private void Update()
    {
        if (sceneinit.sceneState == 2)
        {
            if (timeSinceLastSpawn >= spawnRate)
            {
                startTime = Time.time;

                Instantiate(boulderPrefab, GetPosition(), Quaternion.identity);

            }
            timeSinceLastSpawn = Time.time - startTime;
        }
    }

    private Vector3 GetPosition()
    {
        Vector3 randDir = Random.insideUnitSphere.normalized;
        return player.transform.position + (randDir * spawnDistanceFromPlayer);
    }
}