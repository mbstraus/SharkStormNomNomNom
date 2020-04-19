using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject StunnedAnimation;
    public RuntimeAnimatorController NormalSwimAnimation;
    public RuntimeAnimatorController ScaredSwimAnimation;
    public float MovementSpeed;
    public float ScaredDistance;
    public bool MovementDisabled = false;
    public float MovementTimeoutStartingValue;
    public float MovementTimeout = 0f;
    public int DecoyAmmo = 0;
    public GameObject DecoyPrefab;
    public GameObject SpacebarIndicator;
    private Animator animator;

    public AudioSource playerAudioSource;
    public AudioClip owClip;
    public AudioClip thankyouClip;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        animator.runtimeAnimatorController = NormalSwimAnimation;
    }

    void Update()
    {
        if (GameController.instance.IsGameOver)
        {
            return;
        }
        if(MovementDisabled)
        {
            if (MovementTimeout > 0)
            {
                MovementTimeout -= Time.deltaTime;
            }
            else
            {
                MovementDisabled = false;
                StunnedAnimation.SetActive(false);
            }
        }

        HandleMovement();
        HandleUseDecoy();

        Shark[] sharks = FindObjectsOfType<Shark>();
        Vector3 playerPosition = transform.position;
        bool isScared = false;
        foreach (var shark in sharks)
        {
            float distance = Vector3.Distance(playerPosition, shark.transform.position);
            if (distance < ScaredDistance)
            {
                isScared = true;
                break;
            }
        }
        if (isScared)
        {
            animator.runtimeAnimatorController = ScaredSwimAnimation;
        }
        else
        {
            animator.runtimeAnimatorController = NormalSwimAnimation;
        }

        if (DecoyAmmo > 0)
        {
            SpacebarIndicator.SetActive(true);
        }
        else
        {
            SpacebarIndicator.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject obj = other.gameObject;
        Obstacle hitObstacle = obj.GetComponent<Obstacle>();
        Shark hitShark = obj.GetComponent<Shark>();
        Bomb hitBomb = obj.GetComponent<Bomb>();
        DecoyPowerup hitDecoyPowerup = obj.GetComponent<DecoyPowerup>();

        if (hitShark != null)
        {
            GameController.instance.IsGameOver = true;
            hitShark.StartEating(true, null);
        }
        else if (hitBomb != null)
        {
            GameController.instance.GameOver();
        }
        else if(hitDecoyPowerup != null)
        {
           DecoyAmmo = DecoyAmmo + 1;
        }
        else if (hitObstacle != null)
        {
            if (hitObstacle is Bystander)
            {
                playerAudioSource.PlayOneShot(thankyouClip, 3);
                Bystander bystander = (Bystander)hitObstacle;
                bystander.Rescue();
            }
            else
            {
                playerAudioSource.PlayOneShot(owClip, 3);
                MovementDisabled = true;
                MovementTimeout = MovementTimeoutStartingValue;
                StunnedAnimation.SetActive(true);
            }
        }
    }

    private void HandleMovement()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        float PassiveMovementSpeed = 1f;
        if (GameSettingsManager.instance != null)
        {
            PassiveMovementSpeed = GameSettingsManager.instance.GetPassiveSwimSpeed();
        }

        Vector3 newPosition;
        if ((horizontalMovement == 0 && verticalMovement == 0) || MovementDisabled)
        {
            newPosition = transform.position + new Vector3(-1 * PassiveMovementSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            newPosition = transform.position + new Vector3(horizontalMovement * MovementSpeed * Time.deltaTime, verticalMovement * MovementSpeed * Time.deltaTime, 0);
        }
        if (newPosition.y > GameController.instance.NorthBoundary.position.y)
        {
            newPosition.y = GameController.instance.NorthBoundary.position.y;
        }
        if (newPosition.y < GameController.instance.SouthBoundary.position.y)
        {
            newPosition.y = GameController.instance.SouthBoundary.position.y;
        }
        if (newPosition.x > GameController.instance.EastBoundary.position.x)
        {
            newPosition.x = GameController.instance.EastBoundary.position.x;
        }
        if (newPosition.x < GameController.instance.WestBoundary.position.x)
        {
            newPosition.x = GameController.instance.WestBoundary.position.x;
        }

        transform.position = newPosition;
    }

    private void HandleUseDecoy()
    {

        if (Input.GetKey(KeyCode.Space) && DecoyAmmo>0)
        {
            DecoyAmmo--;
            Instantiate(DecoyPrefab, transform.position, Quaternion.identity);
        }

    }
}
