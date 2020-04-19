using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkWaveSharkController : Shark
{
    private float Lifespan = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Lifespan -= Time.deltaTime;
        if (Lifespan <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        if (GameController.instance.IsGameOver)
        {
            return;
        }
        float moveSpeed = 1.5f;
        if (GameSettingsManager.instance != null)
        {
            moveSpeed = GameSettingsManager.instance.GetSwimSpeed();
        }
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }
}
