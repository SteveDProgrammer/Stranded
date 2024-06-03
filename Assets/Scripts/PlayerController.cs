using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public float movementSpeed;
    public float jumpingForce;

    public Transform groundCheckPoint;
    private bool isOnGround;
    public LayerMask groundLayerMask;
    public Animator standingAnimator;

    public BulletController bulletController;
    public Transform bulletSpawnPoint;

    public bool canDoubleJump;

    public float dashingSpeed, dashingTimeLimit;
    private float timeToNextDash;

    public SpriteRenderer playerSpriteRenderer, afterImageSpriteRenderer;
    public float afterImageDuration, timeBetweenAfterImages;
    private float timeToNextAfterImage;
    public Color afterImageColor;

    public float dashingCooldownTime;
    private float currentDashingCooldown;

    public GameObject standingMode, ballMode;
    public float modeSwitchWaitTime;
    private float currentBallModeDuration;

    public Animator ballModeAnimator;

    public Transform bombSpawnPoint;
    public GameObject bombPrefab;

    private AbilityTracker abilityTracker;

    // Start is called before the first frame update
    void Start()
    {
        abilityTracker = GetComponent<AbilityTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDashingCooldown > 0)
        {
            currentDashingCooldown -= Time.deltaTime;
        }
        else
        {
            if(Input.GetButtonDown("Fire2") && standingMode.activeSelf && abilityTracker.dashUnlocked)
            {
                timeToNextDash = dashingTimeLimit;
                SpawnAfterImageFrame();
            }
        }

        if(timeToNextDash > 0)
        {
            timeToNextDash -= Time.deltaTime;

            playerRigidbody.velocity = new Vector2(dashingSpeed * transform.localScale.x, playerRigidbody.velocity.y);

            timeToNextAfterImage -= Time.deltaTime;
            if(timeToNextAfterImage <= 0) SpawnAfterImageFrame();

            currentDashingCooldown = dashingCooldownTime;
        }
        else
        {
            // What is GetAxis vs GetAxisRaw
            // GetAxis    = Smoothened
            // GetAxisRaw = No Smoothing
            playerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, playerRigidbody.velocity.y);

            if(playerRigidbody.velocity.x < 0) transform.localScale = new Vector3(-1, 1, 1);
            else if(playerRigidbody.velocity.x > 0) transform.localScale = new Vector3(1, 1, 1);
            
            isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, groundLayerMask);
        }

        if(Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilityTracker.doubleJumpUnlocked)))
        {
            if(isOnGround) canDoubleJump = true;
            else
            {
                canDoubleJump = false;
                standingAnimator.SetTrigger("doubleJump");
            }
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpingForce);
        }

        //Shooting
        if(Input.GetButtonDown("Fire1"))
        {
            if(standingMode.activeSelf)
            {
                Instantiate(bulletController, bulletSpawnPoint.position, bulletSpawnPoint.rotation).dir = new Vector2(transform.localScale.x, 0);
                standingAnimator.SetTrigger("shotFired");
            }
            else if(ballMode.activeSelf && abilityTracker.bombingUnlocked)
            {
                Instantiate(bombPrefab, bombSpawnPoint.position, bombSpawnPoint.rotation);
            }
        }

        bool holdDownKey = Input.GetAxisRaw("Vertical") < -0.9f;
        bool holdUpKey = Input.GetAxisRaw("Vertical") > 0.9f;

        if(holdDownKey || holdUpKey)
        {
            currentBallModeDuration -= Time.deltaTime;
            if(currentBallModeDuration <= 0)
            {
                if(standingMode.activeSelf && holdDownKey && abilityTracker.ballModeUnlocked) // Change from standing to ball
                {
                    ballMode.SetActive(true);
                    standingMode.SetActive(false);
                }
                else if(ballMode.activeSelf && holdUpKey) // Change from ball to standing
                {
                    ballMode.SetActive(false);
                    standingMode.SetActive(true);
                }
            }
        }
        else
        {
            currentBallModeDuration = modeSwitchWaitTime;
        }

        if(standingMode.activeSelf)
        {
            standingAnimator.SetBool("isOnGround", isOnGround);
            standingAnimator.SetFloat("speed", Mathf.Abs(playerRigidbody.velocity.x));
        }

        if(ballMode.activeSelf)
        {
            ballModeAnimator.SetFloat("speed", Mathf.Abs(playerRigidbody.velocity.x));
        }
    }

    public void SpawnAfterImageFrame()
    {   
        timeToNextAfterImage = timeBetweenAfterImages;
    
        SpriteRenderer frame = Instantiate(afterImageSpriteRenderer, transform.position, transform.rotation);
        frame.sprite = playerSpriteRenderer.sprite;
        frame.transform.localScale = transform.localScale;
        frame.color = afterImageColor;
        Destroy(frame.gameObject, afterImageDuration);
    }
}
