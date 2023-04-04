using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce= 5f;
    public float moveSpeed = 6f;
    public float moveMultiplier = 10f;
    public float airMoveMultiplier = 0.4f;
    public float playerHeight  =2f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    public float airDrag=2f;
    public float groundDrag= 6f;
    public bool isGrounded;
    private Rigidbody rb;
    public Vector3 moveDir;

    [SerializeField] Transform orientation;

    float horizontalMovement;
    float verticalMovement;
    

    [Header("Keybinds")]
    public KeyCode jumpKey  =KeyCode.Space;

    Vector3 slopeMoveDir;
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
        isGrounded = Physics.CheckSphere(transform.position-new Vector3(0,1,0), groundDistance,groundMask);

        PlayerInput();
        ControlDrag();

        if (isGrounded&& Input.GetKeyDown(jumpKey))
        {
            Jump();
        }

        slopeMoveDir = Vector3.ProjectOnPlane(moveDir, slopeHit.normal);
        
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
        if(isGrounded)
        {
            rb.drag = groundDrag;

        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void MovePlayer()
    {   if(isGrounded&!OnSlope())
        {
            rb.AddForce(moveDir.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded&&OnSlope())
        {
            rb.AddForce(slopeMoveDir.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
        }
        else if(!isGrounded) { }
        {
            rb.AddForce(moveDir.normalized * moveSpeed *moveMultiplier * airMoveMultiplier, ForceMode.Acceleration);
        }
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }
}
