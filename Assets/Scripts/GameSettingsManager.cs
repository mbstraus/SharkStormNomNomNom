using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public enum SwimSpeed
    {
        Slow = 0,
        Normal = 1,
        Fast = 2
    }

    public static GameSettingsManager instance;

    [Range(0, 2)]
    public int CurrentSwimSpeed = 1;
    public float SlowestSwimSpeed = 0.5f;
    public float SlowSwimSpeed = 1f;
    public float NormalSwimSpeed = 1.5f;
    public float FastSwimSpeed = 2.5f;
    public float FastestSwimSpeed = 3f;


    public int StoredCurrentSwimSpeed = 1;
    public float StoredSlowestSwimSpeed = 0.5f;
    public float StoredSlowSwimSpeed = 1f;
    public float StoredNormalSwimSpeed = 1.5f;
    public float StoredFastSwimSpeed = 2.5f;
    public float StoredFastestSwimSpeed = 3f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void ChangeSwimSpeed()
    {
        if (CurrentSwimSpeed == 0) CurrentSwimSpeed = 1;
        else if (CurrentSwimSpeed == 1) CurrentSwimSpeed = 2;
        else CurrentSwimSpeed = 0;
    }

    public float GetSwimSpeed()
    {
        if (CurrentSwimSpeed == 0) return SlowSwimSpeed;
        else if (CurrentSwimSpeed == 1) return NormalSwimSpeed;
        else return FastSwimSpeed;
    }

    public float GetPassiveSwimSpeed()
    {
        if (CurrentSwimSpeed == 0) return SlowestSwimSpeed;
        else if (CurrentSwimSpeed == 1) return SlowSwimSpeed;
        else return NormalSwimSpeed;
    }

    public float GetAcceleratedSwimSpeed()
    {
        if (CurrentSwimSpeed == 0) return NormalSwimSpeed;
        else if (CurrentSwimSpeed == 1) return FastSwimSpeed;
        else return FastestSwimSpeed;
    }

    public string GetSwimSpeedLabel()
    {
        if (CurrentSwimSpeed == 0) return "Slow";
        else if (CurrentSwimSpeed == 1) return "Normal";
        else return "Fast";
    }
}
