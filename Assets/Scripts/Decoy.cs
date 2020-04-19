using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : MonoBehaviour
{
    private bool hasCollided = false;
    private bool countStarted = false;
    public Animator explosionAnimation;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shark shark = collision.gameObject.GetComponent<Shark>();
        if (shark != null && !hasCollided && !countStarted)
        {
            hasCollided = true;
            countStarted = true;
            StartCoroutine("StartCountDown");
        }
    }

    IEnumerator StartCountDown()
    {
        countStarted = true;
        yield return new WaitForSeconds(2);
        PlayExplosionAnimation();
    }

    public void PlayExplosionAnimation()
    {
        explosionAnimation.Play("Explosion");
    }

    public void DestroyDecoy()
    {
        Destroy(gameObject);
    }
}
