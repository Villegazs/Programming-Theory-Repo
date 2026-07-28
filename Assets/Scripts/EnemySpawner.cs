using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 3. ENCAPSULACIÓN: Mantenemos el control del diseño oculto y seguro.
    // Un arreglo (Array) para poner todos nuestros prefabs de enemigos (Cubo, Cilindro, Esfera)
    [SerializeField] private GameObject[] enemyPrefabs; 
    
    [SerializeField] private float spawnRadius = 15f; // Qué tan lejos del jugador nacen
    [SerializeField] private float spawnRate = 2f;    // Tiempo entre cada aparición
    
    private Transform playerTransform;
    private float nextSpawnTime = 0f;

    private void Start()
    {
        // Buscamos al jugador una sola vez al inicio para saber cuál es el centro de aparición
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        // Si el jugador fue destruido, dejamos de generar enemigos
        if (playerTransform == null || !playerTransform.gameObject.activeInHierarchy) return;

        // ABSTRACCIÓN: El temporizador es simple y llama a la función compleja.
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    private void SpawnEnemy()
    {
        // 1. Elegimos un enemigo al azar de nuestra lista
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[randomIndex];

        // 2. Calculamos una posición aleatoria en un círculo alrededor del jugador
        Vector3 spawnPosition = GetRandomPositionAroundPlayer();

        // 3. Instanciamos al enemigo
        // POLIMORFISMO EN DISEÑO: Al Spawner no le importa si acaba de crear un NormalTarget 
        // o un ExplosiveTarget. Los trata a todos como simples GameObjects.
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        // Random.insideUnitCircle genera un punto 2D aleatorio (X, Y) dentro de un círculo.
        // .normalized empuja ese punto exactamente al borde del círculo.
        Vector2 randomPoint2D = Random.insideUnitCircle.normalized * spawnRadius;

        // Convertimos ese punto 2D (X, Y) a nuestro mundo 3D (X, 0, Z)
        // y se lo sumamos a la posición actual del jugador.
        Vector3 spawnOffset = new Vector3(randomPoint2D.x, 0f, randomPoint2D.y);
        
        return playerTransform.position + spawnOffset;
    }
}