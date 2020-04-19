using System.Collections;
using UnityEngine;

public class BomberPowerup : Powerup
{
    public float LifetimeSec = 5f;
    public GameObject BombPrefab;

    void Start()
    {
        StartCoroutine("WaitForTimeout");
    }

    IEnumerator WaitForTimeout()
    {
        yield return new WaitForSeconds(LifetimeSec);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController != null)
        {
            TargetSharks();
            SpawnRandomBombs();
            Destroy(gameObject);
        }
    }

    private void TargetSharks()
    {
        Shark[] sharks = FindObjectsOfType<Shark>();
        foreach (Shark shark in sharks)
        {
            if (shark is BarrierSharkController)
            {
                continue;
            }
            shark.TargetSharkForBombing();
        }
    }

    private void SpawnRandomBombs()
    {
        var numberOfBombs = Random.Range(GameController.instance.RandomBombMin, GameController.instance.RandomBombMax);
        for (int i = 0; i < numberOfBombs; i++)
        {
            var x = Random.Range(GameController.instance.WestBoundary.position.x, GameController.instance.EastBoundary.position.x);
            var y = Random.Range(GameController.instance.SouthBoundary.position.y, GameController.instance.NorthBoundary.position.y);

            Vector3 position = new Vector3(x, y, 0f);
            Instantiate(BombPrefab, position, Quaternion.identity, null);
        }
    }
}
