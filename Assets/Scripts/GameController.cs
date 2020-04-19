using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Transform NorthBoundary;
    public Transform SouthBoundary;
    public Transform EastBoundary;
    public Transform WestBoundary;

    public int MaxSeekAndDestroySharks;
    public int CurrentSeekAndDestroySharks;
    public float SharkSpawnTimeMin;
    public float SharkSpawnTimeMax;

    public float PowerupSpawnTimeMin;
    public float PowerupSpawnTimeMax;
    public int RandomBombMin;
    public int RandomBombMax;

    public float ObstacleMaxTimeUntilNextSpawn;
    public float ObstacleMinTimeUntilNextSpawn;

    public bool IsGameOver = false;
    public GameObject GameOverScreen;

    public float ElapsedTime;
    public int RescuedBystanders;
    public int KilledBystanders;
    public int SharksKilled;

    int lastIncreaseSec = 0;
    int StartingPlaySpeed;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (GameSettingsManager.instance == null)
        {
            GameObject obj = new GameObject();
            obj.AddComponent<GameSettingsManager>();
        }
        StartingPlaySpeed = GameSettingsManager.instance.CurrentSwimSpeed;
    }

    private void Update()
    {
        if (!IsGameOver)
        {
            ElapsedTime += Time.deltaTime;

            int timeSec = Mathf.RoundToInt(ElapsedTime);
            if (timeSec % 30 == 0 && lastIncreaseSec != timeSec)
            {
                if (timeSec % 60 == 0 && GameSettingsManager.instance.CurrentSwimSpeed < 2)
                {
                    GameSettingsManager.instance.ChangeSwimSpeed();
                }
                MaxSeekAndDestroySharks += 1;
                SharkSpawnTimeMin = Mathf.Max(0.5f, SharkSpawnTimeMin - 0.5f);
                SharkSpawnTimeMax = Mathf.Max(2f, SharkSpawnTimeMax - 0.5f);
                ObstacleMinTimeUntilNextSpawn = Mathf.Max(0.5f, ObstacleMinTimeUntilNextSpawn - 0.5f);
                ObstacleMaxTimeUntilNextSpawn = Mathf.Max(1.5f, ObstacleMaxTimeUntilNextSpawn - 0.5f);
                RandomBombMin += 1;
                RandomBombMax += 1;
                lastIncreaseSec = timeSec;
            }
        }
    }

    public bool CanSpawnSeekAndDestroyShark()
    {
        return CurrentSeekAndDestroySharks < MaxSeekAndDestroySharks;
    }

    public void GameOver()
    {
        GameSettingsManager.instance.CurrentSwimSpeed = StartingPlaySpeed;
        IsGameOver = true;
        GameOverScreen.SetActive(true);
    }

    public void BystanderRescued()
    {
        RescuedBystanders += 1;
    }

    public void BystanderKilled()
    {
        KilledBystanders += 1;
    }

    public void SharkKilled()
    {
        SharksKilled += 1;
    }
}
