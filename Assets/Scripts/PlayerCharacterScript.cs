using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterScript : MonoBehaviour
{
    [Header ("Components")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector3 groundCheckSize = new Vector3(2f, 0.1f, 2f);
    [SerializeField] private Transform headCheckPoint;
    [SerializeField] private Vector3 headCheckSize = new Vector3(2f, 0.1f, 2f);
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject pauseMenu;
    private Rigidbody rb;
    public Transform playerObj;

    [Header("Move")]
    [SerializeField] private float acceleration = 90f;
    [SerializeField] private float deacceleration = 60f;
    [SerializeField] private float maxMovespeed = 13f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector2 moveDirection;
    private Vector2 horizontalSpeed;
    public Vector3 SpawnPoint;

    [Header("Jump")]
    [SerializeField] private float jumpStrength = 30f;
    [SerializeField] private float minFallSpeed = 80f;
    [SerializeField] private float maxFallSpeed = 120f;
    [SerializeField] private float fallClamp = -40f;
    [SerializeField] private float jumpApexThreshold = 10f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    private bool grounded = false;
    private bool groundedPreviously = false;
    private bool headHit = false;
    private bool jumpPressed = false;
    private bool alreadyJumped = false;
    private bool endedJumpEarly = false;
    private float verticalSpeed;
    private float jumpEndEarlyGravityModifier = 3f;
    private float fallSpeed;
    private float apexPoint;
    private float coyoteTimer;
    private float jumpBufferTimer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        SpawnPoint.Set(transform.position.x,transform.position.y, transform.position.z);
    }

    void Update()
    {
        GroundedCheck();
        HeadCheck();
        CalcHorizontalSpeed();
        CalcVerticalSpeed();
        turnCharacter(); 
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontalSpeed.x, verticalSpeed, horizontalSpeed.y);
    }

    #region Movement

    private void CalcHorizontalSpeed()
    {
        if (moveDirection.x != 0 || moveDirection.y !=0)
        {
            Vector3 relMoveDirection = moveDirection.y * cam.forward + moveDirection.x * cam.right;
            horizontalSpeed.x += relMoveDirection.normalized.x * acceleration * Time.deltaTime;
            horizontalSpeed.y += relMoveDirection.normalized.z * acceleration * Time.deltaTime;
            horizontalSpeed = Vector2.ClampMagnitude(horizontalSpeed, maxMovespeed);
        }
        else
        {
            horizontalSpeed = Vector2.MoveTowards(horizontalSpeed, new Vector2(0,0), deacceleration * Time.deltaTime);
        }
    }

    private void CalcVerticalSpeed()
    {
        if (grounded)
        {
            apexPoint = 0;
            if (verticalSpeed < 0) verticalSpeed = 0;
            if (!jumpPressed) alreadyJumped = false;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            apexPoint = Mathf.InverseLerp(jumpApexThreshold, 0, Mathf.Abs(rb.velocity.y));
            fallSpeed = Mathf.Lerp(minFallSpeed, maxFallSpeed, apexPoint);
            if(endedJumpEarly && verticalSpeed > 0)
            {
                fallSpeed*= jumpEndEarlyGravityModifier;
            }
            verticalSpeed -= fallSpeed * Time.deltaTime;
            if (verticalSpeed < fallClamp) verticalSpeed = fallClamp;
            if(groundedPreviously && verticalSpeed < 0) coyoteTimer = coyoteTime;
        }

        groundedPreviously = grounded;
        jumpBufferTimer -= Time.deltaTime;

        if (jumpPressed && coyoteTimer > 0 && !alreadyJumped|| grounded && jumpBufferTimer > 0)
        {
            coyoteTimer = 0;
            jumpBufferTimer = 0;
            verticalSpeed = jumpStrength;
            endedJumpEarly = false;
            alreadyJumped = true;
        }

        if (!jumpPressed && !grounded && !endedJumpEarly && verticalSpeed > 0)
        {
            endedJumpEarly = true;
        }

        if(headHit)
        {
            verticalSpeed = 0;
            endedJumpEarly = true;
        }
    }

    private void turnCharacter()
    {
        if ((moveDirection.x != 0 || moveDirection.y != 0))
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(playerObj.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            playerObj.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    #endregion

    #region Death
    public void Death()
    {
        transform.position = SpawnPoint;
    }

    #endregion

    #region Collision

    private void GroundedCheck()
    {
        if (Physics.CheckBox(groundCheckPoint.position, groundCheckSize, new Quaternion(), groundLayer)) grounded = true;
        else grounded = false;
    }

    private void HeadCheck()
    {
        if (Physics.CheckBox(headCheckPoint.position, headCheckSize, new Quaternion(), groundLayer)) headHit = true;
        else headHit = false;
    }
    #endregion

    #region Input
    private void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
    private void OnJump(InputValue value)
    {
        jumpPressed = value.isPressed;
        if(value.isPressed)
        {
            jumpBufferTimer = jumpBufferTime;
        }
    }
    private void OnPause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    #endregion

    #region Editor Stuff
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
        Gizmos.DrawWireCube(headCheckPoint.position, headCheckSize);
    }
    #endregion
}
