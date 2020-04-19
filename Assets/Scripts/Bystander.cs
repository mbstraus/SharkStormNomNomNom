using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bystander : Obstacle
{
    public GameObject RegularGraphics;
    public GameObject RescueGraphics;
    public Animator RescueAnimator;
    public RuntimeAnimatorController RescueAnimationController;
    private BoxCollider2D boxCollider;
    private bool hasCollided = false;


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Rescue()
    {
        boxCollider.enabled = false;
        RegularGraphics.SetActive(false);
        RescueGraphics.SetActive(true);
        RescueAnimator.runtimeAnimatorController = RescueAnimationController;
        GameController.instance.BystanderRescued();
    }

    public void RescueAnimationComplete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shark shark = collision.gameObject.GetComponent<Shark>();
        if (shark != null && !hasCollided)
        {
            hasCollided = true;
            StopMoving = true;
            transform.parent = shark.transform;
            shark.StartEating(false, gameObject);
            GameController.instance.BystanderKilled();
        }
    }
}
