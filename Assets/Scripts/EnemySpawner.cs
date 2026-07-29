using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // ENCAPSULATION
    // An array to store all enemy prefabs
    [SerializeField] private GameObject[] enemyPrefabs; 
    
    [SerializeField] private float spawnRadius = 15f; // How far from the player enemies spawn
    [SerializeField] private float spawnRate = 2f;    // Time between spawns
    
    private Transform playerTransform;
    private float nextSpawnTime = 0f;

    private void Start()
    {
        // Find the player once at the start to determine the spawn center
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        // If the player was destroyed, stop spawning enemies
        if (playerTransform == null || !playerTransform.gameObject.activeInHierarchy) return;

        // ABSTRACTION
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    private void SpawnEnemy()
    {
        // 1. Choose an enemy at random from our list
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[randomIndex];

        // 2. Calculate a random position in a circle around the player
        Vector3 spawnPosition = GetRandomPositionAroundPlayer();

        // 3. Instantiate the enemy
        // POLYMORPHISM
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        // Random.insideUnitCircle generates a random 2D point (X, Y) inside a circle.
        // .normalized pushes that point exactly to the edge of the circle.
        Vector2 randomPoint2D = Random.insideUnitCircle.normalized * spawnRadius;

        // Convert that 2D point (X, Y) to our 3D world (X, 0, Z)
        // and add it to the player's current position.
        Vector3 spawnOffset = new Vector3(randomPoint2D.x, 0f, randomPoint2D.y);
        
        return playerTransform.position + spawnOffset;
    }
}