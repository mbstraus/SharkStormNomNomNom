using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyPowerup : Powerup
{
    public float LifetimeSec = 5f;

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
            Destroy(gameObject);
        }
    }
}
