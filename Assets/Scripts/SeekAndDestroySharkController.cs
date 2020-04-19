using System.Collections;
using UnityEngine;

public class SeekAndDestroySharkController : Shark
{
    private PlayerController playerController;
    private Bystander currentBystanderTarget;
    private Decoy currentDecoyTarget;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (!IsActive || GameController.instance.IsGameOver)
        {
            return;
        }
        float moveSpeed = 1.5f;
        if (GameSettingsManager.instance != null)
        {
            moveSpeed = GameSettingsManager.instance.GetSwimSpeed();
        }
        var playerHeading = transform.position - playerController.transform.position;
        var playerDistance = playerHeading.magnitude;
        var playerDirection = playerHeading / playerDistance;

        Vector3 bystanderHeading = Vector3.zero;
        float bystanderDistance = 0f;
        Vector3 bystanderDirection = Vector3.zero;

        Vector3 decoyHeading = Vector3.zero;
        float decoyDistance = 0f;
        Vector3 decoyDirection = Vector3.zero;

        //Target Decoy if one is active
        if (currentDecoyTarget == null)
        {
            Decoy[] decoys = FindObjectsOfType<Decoy>();
            if(decoys.Length>0)
            {
                float closestDistance = float.MaxValue;
                Vector3 closestHeading = Vector3.zero;
                Decoy closestDecoy= null;
                foreach(Decoy decoy in decoys)
                {
                    decoyHeading = transform.position - decoy.transform.position;
                    decoyDistance = decoyHeading.magnitude;
                    if(closestDistance> decoyDistance)
                    {
                        closestDistance = decoyDistance;
                        closestHeading = decoyHeading;
                        closestDecoy = decoy;
                    }
                }
                decoyDirection = closestHeading / closestDistance;
                currentDecoyTarget = closestDecoy;
            }
        }
        else
        {
            decoyHeading = transform.position - currentDecoyTarget.transform.position;
            decoyDistance = decoyHeading.magnitude;
            decoyDirection = decoyHeading / decoyDistance;
        }


        // Target a bystander if one is active.
        if (currentBystanderTarget == null)
        {
            Bystander[] bystanders = FindObjectsOfType<Bystander>();
            if (bystanders.Length > 0)
            {
                float closestDistance = float.MaxValue;
                Vector3 closestHeading = Vector3.zero;
                Bystander closestBystander = null;
                foreach (Bystander bystander in bystanders)
                {
                    bystanderHeading = transform.position - bystander.transform.position;
                    bystanderDistance = bystanderHeading.magnitude;
                    if (closestDistance > bystanderDistance)
                    {
                        closestDistance = bystanderDistance;
                        closestHeading = bystanderHeading;
                        closestBystander = bystander;
                    }
                }
                bystanderDirection = closestHeading / closestDistance;
                currentBystanderTarget = closestBystander;
            }
        }
        else
        {
            bystanderHeading = transform.position - currentBystanderTarget.transform.position;
            bystanderDistance = bystanderHeading.magnitude;
            bystanderDirection = bystanderHeading / bystanderDistance;
        }
        if(currentDecoyTarget != null)
        {
            transform.position -= decoyDirection * moveSpeed * Time.deltaTime;
        }

        else if (bystanderDistance > playerDistance || bystanderDistance == 0f)
        {
            transform.position -= playerDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position -= bystanderDirection * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Decoy decoy = collision.GetComponent<Decoy>();
        Bystander bystander = collision.GetComponent<Bystander>();
        if(decoy != null)
        {
            StartCoroutine("StartCountDown");
        }
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }



}
