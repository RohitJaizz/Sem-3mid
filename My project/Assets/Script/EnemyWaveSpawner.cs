using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int enemyCount;
    public float spawnRate;
}

public class EnemyWaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    private int currentWaveIndex = 0;
    private bool spawning = false;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(2f); // Small delay before first wave

        while (currentWaveIndex < waves.Length)
        {
            Wave wave = waves[currentWaveIndex];
            Debug.Log("Starting Wave: " + wave.waveName);

            spawning = true;
            yield return StartCoroutine(SpawnWave(wave));
            spawning = false;

            Debug.Log("Waiting for enemies to die...");

            // Wait until all enemies are destroyed
            yield return new WaitUntil(() => activeEnemies.Count == 0);

            Debug.Log("Wave Complete!");
            currentWaveIndex++;

            yield return new WaitForSeconds(3f); // Delay before next wave
        }

        Debug.Log("All waves completed!");
    }

    IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        activeEnemies.Add(newEnemy);

        // When the enemy dies, remove it from list
        EnemyHealth enemyHealth = newEnemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.onEnemyDeath += () => { activeEnemies.Remove(newEnemy); };
        }
    }
}
