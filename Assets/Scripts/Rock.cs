using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Obstacle
{
    public GameObject RegularGraphic;
    public Animator RockAnimator;
    public RuntimeAnimatorController RockAnimatorController;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        RockAnimator.Play("Shatter");
    }

    private void DeleteRock()
    {
        Destroy(gameObject);
    }
}
