using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;

    public float obstacleSpawnTime = 2f;
    private float timeUntilObstacleSpawn;

    private void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;
        if (timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);

    }
}
