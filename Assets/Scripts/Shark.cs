using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public Animator ExplosionAnimator;
    protected bool IsActive = true;
    private bool IsEatingPlayer = false;
    private GameObject eatingGameObject = null;
    public AudioSource sharkAudioSource;
    public AudioClip nomnomAudio;
    public AudioClip explosionAudio;

    public void TargetSharkForBombing()
    {
        ExplosionAnimator.Play("seek_and_destroy_explosion");
        AudioManager.instance.PlayExplosionAudio(3);
    }

    public void DisableShark()
    {
        IsActive = false;
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.gameObject.SetActive(false);
    }

    public void KillShark()
    {
        GameController.instance.CurrentSeekAndDestroySharks -= 1;
        GameController.instance.SharkKilled();
        Destroy(gameObject);
    }

    public void FinishEating()
    {
        ExplosionAnimator.SetBool("IsEating", false);
        if (IsEatingPlayer)
        {
            GameController.instance.GameOver();
        }
        if (eatingGameObject != null)
        {
            Destroy(eatingGameObject);
        }
    }

    public void StartEating(bool isPlayer, GameObject gameObjectToDestroy)
    {
        sharkAudioSource.PlayOneShot(nomnomAudio, 5);
        ExplosionAnimator.SetBool("IsEating", true);
        IsEatingPlayer = isPlayer;
        eatingGameObject = gameObjectToDestroy;
    }


}
