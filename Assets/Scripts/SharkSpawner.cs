using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SharkSpawner : MonoBehaviour
{
    public enum SharkTypes
    {
        SeekAndDestroy,
        SharkWave
    }

    public Shark SeekAndDestroySharkPrefab;
    public Shark SharkWaveSharkPrefab;
    public Transform SharkContainer;
    public GameObject SharkWavePanel;
    private BarrierSharkController[] barrierSharks;

    private void Start()
    {
        barrierSharks = FindObjectsOfType<BarrierSharkController>();
        StartCoroutine("SpawnShark");
    }

    IEnumerator SpawnShark()
    {
        float remainingTime = UnityEngine.Random.Range(GameController.instance.SharkSpawnTimeMin, GameController.instance.SharkSpawnTimeMax);
        while (true)
        {
            if (GameController.instance.IsGameOver)
            {
                break;
            }
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0f)
            {
                if (GameController.instance.CanSpawnSeekAndDestroyShark())
                {
                    int sharksToSpawn = UnityEngine.Random.Range(0, 2);
                    if (sharksToSpawn == 0)
                    {
                        DoSpawnShark();
                        GameController.instance.CurrentSeekAndDestroySharks += 1;
                    }
                    else
                    {
                        DoSpawnSharkWave();
                    }
                }
                else
                {
                    DoSpawnSharkWave();
                }
                remainingTime = UnityEngine.Random.Range(GameController.instance.SharkSpawnTimeMin, GameController.instance.SharkSpawnTimeMax);
            }
            yield return null;
        }
    }

    private void DoSpawnShark()
    {
        int barrierSharkToSpawnOn = UnityEngine.Random.Range(0, barrierSharks.Length);
        BarrierSharkController spawnLocation = barrierSharks[barrierSharkToSpawnOn];

        Instantiate(SeekAndDestroySharkPrefab, spawnLocation.transform.position, Quaternion.identity, SharkContainer);
    }

    private void DoSpawnSharkWave()
    {
        SharkWavePanel.SetActive(true);
        StartCoroutine("SharkWavePanelTimeout");
        int barrierSharkToNotSpawnOn = UnityEngine.Random.Range(0, barrierSharks.Length);
        for (int i = 0; i < barrierSharks.Length; i++)
        {
            if (i != barrierSharkToNotSpawnOn)
            {
                BarrierSharkController spawnLocation = barrierSharks[i];
                Instantiate(SharkWaveSharkPrefab, spawnLocation.transform.position, Quaternion.identity, SharkContainer);
            }
        }
    }

    IEnumerator SharkWavePanelTimeout()
    {
        yield return new WaitForSeconds(1);
        SharkWavePanel.SetActive(false);
    }
}
