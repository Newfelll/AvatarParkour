using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    [SerializeField] Transform orientation;
    public Camera playerCam;
    public VisualEffect vfxSpeedLines;

    [Header("Player Movement")]

    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float moveMultiplier = 10f;
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;
    [SerializeField] private float airMoveMultiplier = 0.4f;



    [Header("Jump")]
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] Transform groundCheck;
    
    public LayerMask groundMask;

    [Header("Dash")]
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private bool canDash = true;
    [SerializeField] private float maxDashDistance = 5f;
    [SerializeField] private float dashCooldown = 1.5f;
    [SerializeField] private float lastDashTime = 0f;
    [SerializeField] private int dashFov = 90;
    [SerializeField] private int originalFov = 80;
    [SerializeField] private float fovTime = 0.25f;


    [Header("VFX Variables")]
    [SerializeField] private float speedLineVelocity = 10;

    Vector3 dashDirection;
    float dashDuration = 0.2f;

    [Header("Vectors")]
    public Vector3 moveDir;
    Vector3 slopeMoveDir;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey;
    
    private float horizontalMovement;
    private float verticalMovement;

    RaycastHit slopeHit;
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2f + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else { return false; }
        }
        return false;
    }

    // Use this for initialization
    void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        PlayerInput();
        ControlDrag();

        if (isGrounded && Input.GetKeyDown(jumpKey))
        {
            Jump();
            canDoubleJump = true;
        }
        else if (Input.GetKeyDown(jumpKey) && !isGrounded && canDoubleJump)
        {
            Jump();
            canDoubleJump = false;
        }
        
        if(Input.GetKeyDown(dashKey)&& canDash&& Time.time >= lastDashTime + dashCooldown)
        {
            
            StartCoroutine(Dash());
        }
            slopeMoveDir = Vector3.ProjectOnPlane(moveDir, slopeHit.normal);


        if (Mathf.Abs(rb.velocity.x)  > speedLineVelocity|| Mathf.Abs(rb.velocity.y) > speedLineVelocity|| Mathf.Abs(rb.velocity.z) > speedLineVelocity)
        {   
            vfxSpeedLines.enabled = true;
        }
        else
        {
            vfxSpeedLines.enabled = false;
        }
    }
    void FixedUpdate()
    {
        MovePlayer();

    }

    void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDir = orientation.transform.forward * verticalMovement + orientation.transform.right * horizontalMovement;
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;

        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void MovePlayer()
    {
        if (isGrounded & !OnSlope())
        {
            rb.AddForce(moveDir.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDir.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded) { }
        {
            rb.AddForce(moveDir.normalized * moveSpeed * moveMultiplier * airMoveMultiplier, ForceMode.Acceleration);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        

    }


  /*  void Dash()
    {
        
        
        rb.velocity = new Vector3(0, 0,0);
        //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        
        if (moveDir == Vector3.zero)
        {
            rb.AddForce(orientation.transform.forward * dashForce, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(moveDir.normalized * dashForce, ForceMode.Impulse);
            
        }

        
    }*/


    /*IEnumerator DashTimer()
    {   
        Dash();
        canDash = false;
        yield return new WaitForSeconds(1);
        
        canDash = true;

    }*/

    IEnumerator Dash()
    {
        canDash = false;
        lastDashTime = Time.time;
        float startTime = Time.time;
        DoFov(dashFov);
       // vfxSpeedLines.Play();
        if (moveDir == Vector3.zero)
        {
           dashDirection = orientation.transform.forward;
        }
        else
        {
            dashDirection = moveDir.normalized;

        }
       // Vector3 dashDirection = orientation.transform.forward;
        Vector3 originalVelocity = rb.velocity;
        
        originalVelocity.y = 0;
        
        while (Time.time < startTime + dashDuration)
        {
            Vector3 dashVelocity = dashDirection* dashForce;
            dashVelocity.y = 0; // Keep the original Y-axis velocity
            rb.velocity = dashVelocity;
            yield return null;
        }

        rb.velocity = originalVelocity;
        DoFov(originalFov);
       // vfxSpeedLines.Stop();
        canDash = true;
    }
    
    void DoFov(float targetFov)
    {
        playerCam.DOFieldOfView(targetFov, fovTime);
    }


    void Sling()
    {

    }
}
