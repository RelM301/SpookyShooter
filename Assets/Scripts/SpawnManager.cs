using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] healthUpPrefabs;
    public Transform entrance;
    public float initialEnemySpawnDelay = 3f;
    public float initialPowerUpSpawnDelay = 10f;
    public float enemySpawnInterval = 7f;
    public float powerUpSpawnInterval = 15f;
    public float searchRadius = 10f;
    public AudioSource spawnSound;

    private GameObject player;
    private int currentDifficultyLevel = 0;

    void Start()
    {
        StartCoroutine(EnemySpawnLoop());
        StartCoroutine(HealthUpSpawnLoop());
        player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator EnemySpawnLoop()
    {
        float timeSinceLastIncrease = 0f;
        float difficultyIncreaseInterval = 5f;
        int maxEnemiesToSpawn = 2; // Maximum number of enemies to spawn at once.

        yield return new WaitForSeconds(initialEnemySpawnDelay);

        while (true)
        {
            // Determine how many enemies to spawn this time
            int enemiesToSpawn = Mathf.Min(currentDifficultyLevel / 4 + 1, maxEnemiesToSpawn);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
            }

            timeSinceLastIncrease += enemySpawnInterval;

            // Check if it's time to increase the difficulty
            if (timeSinceLastIncrease >= difficultyIncreaseInterval)
            {
                timeSinceLastIncrease = 0f;
                enemySpawnInterval -= 0.2f; // Decrease the enemySpawnInterval to increase difficulty faster.
            }

            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }

    IEnumerator HealthUpSpawnLoop()
    {
        yield return new WaitForSeconds(initialPowerUpSpawnDelay);

        while (true)
        {
            SpawnHealth();

            // You can adjust the delay and interval for power-ups here
            yield return new WaitForSeconds(powerUpSpawnInterval);
        }
    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPosition = entrance.position;
        GameObject newEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);

        newEnemy.GetComponent<EnemyController>().playerTarget = player.transform;
    }

    void SpawnHealth()
    {
        int healthUpIndex = Random.Range(0, healthUpPrefabs.Length);
        Vector3 randomPosition = Vector3.zero;

        for (int i = 0; i < 10; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-searchRadius, searchRadius), 0f, Random.Range(-searchRadius, searchRadius));
            randomPosition = entrance.position + randomOffset;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                randomPosition = hit.position;

                float floatingHeight = 1.1f;
                randomPosition.y = floatingHeight;

                GameObject newHealthUp = Instantiate(healthUpPrefabs[healthUpIndex], randomPosition, Quaternion.identity);
                spawnSound.PlayOneShot(spawnSound.clip);

                Destroy(newHealthUp, 10f);

                StartCoroutine(FloatingAnimation(newHealthUp, floatingHeight, 0.2f));

                break;
            }
        }
    }

    IEnumerator FloatingAnimation(GameObject healthUpPrefab, float floatingHeight, float duration)
    {
        Vector3 originalPosition = healthUpPrefab.transform.position;
        Vector3 targetPosition = originalPosition + Vector3.up * 0.2f;

        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            healthUpPrefab.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            yield return null;
        }

        startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            healthUpPrefab.transform.position = Vector3.Lerp(targetPosition, originalPosition, t);

            yield return null;
        }
    }

}
