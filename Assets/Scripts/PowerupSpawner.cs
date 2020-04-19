using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public Powerup[] Powerups;
    public Transform PowerupContainer;

    private void Start()
    {
        StartCoroutine("SpawnPowerup");
    }

    IEnumerator SpawnPowerup()
    {
        float remainingTime = Random.Range(GameController.instance.PowerupSpawnTimeMin, GameController.instance.PowerupSpawnTimeMax);
        while (true)
        {
            if (GameController.instance.IsGameOver)
            {
                break;
            }
            yield return new WaitForSeconds(remainingTime);
            DoSpawnPowerup();
            remainingTime = Random.Range(GameController.instance.PowerupSpawnTimeMin, GameController.instance.PowerupSpawnTimeMax);
        }
    }

    private void DoSpawnPowerup()
    {
        int powerupToSpawn = Random.Range(0, Powerups.Length);
        float positionX = Random.Range(GameController.instance.WestBoundary.position.x + 1f, GameController.instance.EastBoundary.position.x);
        float positionY = Random.Range(GameController.instance.SouthBoundary.position.y, GameController.instance.NorthBoundary.position.y);
        Vector3 spawnLocation = new Vector3(positionX, positionY, 0f);
        Powerup powerUpPrefab = Powerups[powerupToSpawn];

        Instantiate(powerUpPrefab, spawnLocation, Quaternion.identity, PowerupContainer);
    }
}
