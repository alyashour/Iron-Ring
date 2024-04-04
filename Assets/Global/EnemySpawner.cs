using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float activationDistance = 1f; // Distance for activation
    private bool canSpawn = false;
    private int spawnedEnemies = 0; // Counter for the number of spawned enemies
    private const int maxEnemies = 10; // Maximum number of enemies to spawn

    // Reference to the player's Transform. Assign this via the Inspector or find it dynamically.
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        // If not assigned, try to find the player by tag
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        // Check distance to the player every frame and start spawning when close enough, if spawning hasn't already been started or completed
        if (!canSpawn && Vector3.Distance(transform.position, playerTransform.position) <= activationDistance && spawnedEnemies < maxEnemies)
        {
            canSpawn = true;
            StartCoroutine(Spawner());
        }
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn && spawnedEnemies < maxEnemies)
        {
            yield return wait;
            int rand = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            spawnedEnemies++; // Increment the counter each time an enemy is spawned

            // If maxEnemies have been spawned, stop spawning
            if (spawnedEnemies >= maxEnemies)
            {
                canSpawn = false;
                // Optionally, you could also call StopCoroutine here if you have a mechanism to restart the spawner.
            }
        }
    }
}
