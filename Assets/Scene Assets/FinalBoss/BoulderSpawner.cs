using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{

    [SerializeField] GameObject boulderPrefab;
    [SerializeField] GameObject player;

    private float startTime;
    private float timeSinceLastSpawn = 0;


    private float spawnDistanceFromPlayer = 3f;


    private void Start()
    {
        startTime = Time.time;
    }


    private void Update()
    {
        

        if (SceneInitialization.sceneState == 2)
        {
            if (timeSinceLastSpawn >= 1)
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

        return player.transform.position + randDir * spawnDistanceFromPlayer;


    }




}
