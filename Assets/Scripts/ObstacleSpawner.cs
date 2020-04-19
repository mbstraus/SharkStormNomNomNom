using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleSpawner : MonoBehaviour
{
    public List<GameObject> Obstacles = new List<GameObject>();

    private void Start()
    {
        StartCoroutine("SpawnObstacle");
    }

    IEnumerator SpawnObstacle()
    {
        float remainingTime = Random.Range(GameController.instance.ObstacleMinTimeUntilNextSpawn, GameController.instance.ObstacleMaxTimeUntilNextSpawn);
        while (true)
        {
            if (GameController.instance.IsGameOver)
            {
                break;
            }
            yield return new WaitForSeconds(remainingTime);
            DoSpawnObstacle();
            remainingTime = Random.Range(GameController.instance.ObstacleMinTimeUntilNextSpawn, GameController.instance.ObstacleMaxTimeUntilNextSpawn);
        }
    }

    private void DoSpawnObstacle()
    {
        int objectInstance = Random.Range(0, Obstacles.Count);
        Vector3 spawnLocation = new Vector3(7f, Random.Range(-5f, 2.5f), 0);
        Instantiate(Obstacles[objectInstance], spawnLocation, Quaternion.identity);
    }
}
