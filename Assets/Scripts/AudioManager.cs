using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip explosionClip;
    public int timeToExplode;

    private void Awake()
    {
        instance = this;
    }

    public void PlayExplosionAudio(int explosionVar)
    {
        timeToExplode = explosionVar;
        StartCoroutine("waitThreeSecondsThenBoom");
    }

    IEnumerator waitThreeSecondsThenBoom()
    {
        yield return new WaitForSeconds(timeToExplode);
        audioSource.PlayOneShot(explosionClip, 2);
    }
}
