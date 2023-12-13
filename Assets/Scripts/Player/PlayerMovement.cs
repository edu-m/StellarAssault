using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveSpeed = 7;
    private Animator animator;
    private readonly Vector3 defaultCameraPos = new (0f, 1.878f, 0.3279991f);

    [Header("Movement")]
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool canJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode rollKey = KeyCode.LeftAlt;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;
    GameObject cameraPos;

    public MovementState state;
    public enum MovementState
    {
        walking, sprinting, crouching, airborne
    }

    private void Start()
    {
        animator = GameObject.Find("PlayerCharacter").GetComponent<Animator>();
        cameraPos = GameObject.Find("CameraPos");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.25f, whatIsGround);
        animator.SetBool("OnGround", grounded);
        MyInput();
        SpeedControl();
        StateHandler();
        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(jumpKey) && canJump && grounded) { 
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }

        if (Input.GetKeyDown(crouchKey))
        {
            animator.SetBool("Crouch", true);
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            // if we scale down the player's model, it will float in air. This is due to the fact that scaling
            // a model like this reduces the scale from both ends, towards the center. We add a little force
            // to quickly push the player back to the ground.
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        if (!Input.GetKey(crouchKey) && CanUncrouch())
        {
            animator.SetBool("Crouch", false);
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private bool CanUncrouch() => !Physics.Raycast(transform.position, Vector3.up, playerHeight * 1.1f);

    private void StateHandler()
    {
        // if the rigid body is not moving, we should set the animation parameter "Speed" to 0,
        // otherwise we change it accordingly
        if (rb.velocity.magnitude < 0.3f)
        {
            animator.SetFloat("Speed", 0f);
        }
        else 
        { 
            if (grounded)
            {
                //cameraPos.transform.localPosition = defaultCameraPos;
                animator.SetBool("OnGround", true);
                if (Input.GetKey(sprintKey)) // if we're sprinting, that's the only thing we can do
                {
                    //Debug.Log("Running");
                    cameraPos.transform.localPosition = defaultCameraPos + new Vector3(0f, 0f, 0.2f);
                    state = MovementState.sprinting;
                    moveSpeed = sprintSpeed;
                }
                else // if we're not, we can either crouch or walk
                {
                    if (Input.GetKey(crouchKey))
                    {
                        //Debug.Log("Crouching");
                        state = MovementState.crouching;
                        moveSpeed = crouchSpeed;
                    }
                    else
                    {
                        //Debug.Log("Walking");
                        cameraPos.transform.localPosition = defaultCameraPos + new Vector3(0f, 0f, 0.1f);
                        state = MovementState.walking;
                        moveSpeed = walkSpeed;
                    }
                
                }
            }
            else // if grounded is False, we are airborne
            {
                //Debug.Log("Airborne");
                animator.SetBool("OnGround", false);
                state = MovementState.airborne;
            }
            animator.SetFloat("Speed", Mathf.Ceil(moveSpeed / walkSpeed)); // not necessary, just nicer since it's going to be 1-2
        }
        //Debug.Log(animator.GetFloat("Speed"));
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if(grounded)    
            rb.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(10f * airMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x,rb.velocity.y,limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        rb.AddForce(transform.up * jumpForce,ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }
}
