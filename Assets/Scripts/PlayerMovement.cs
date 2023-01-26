using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
using UnityEditor.ShaderKeywordFilter;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    [Header("Movement Variable")]
    public float moveSpeed = 4f;
    public float jumpForce = 4f;
    public float dashForce = 4f;
    public float distToGround = 1f;

    // Variable for Dash
    [Header("Dash Variable")]
    public float dashCoolDownSeconds = 1f;
    private bool isDashCooledDown = true;

    // Variable for Jumping
    [Header("Jump Variable")]
    public LayerMask WhatIsGround;
    public Transform groundPoint;
    private bool isGrounded = false;

    // Debug Text Variable
    [Header("Debugging")]
    public Text GroundDebug;
    public Text DashDebug;

    // In-Script Variable
    float horz;
    float vert;
    bool _jump;
    bool _dash;
    float _slopeAngle;
    Vector3 deltaPosition;


    //Player Controls
    private PlayerInput playerInput;

    //main virtual cam
    [Header("Camera")]
    [SerializeField]
    private CinemachineVirtualCamera vcam;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Player input listeners for vectors
        playerInput.Input.Move.started += movementInput;
        playerInput.Input.Move.performed += movementInput;
        playerInput.Input.Move.canceled += movementInput;

        //Player input listeners for buttons
        playerInput.Input.Jump.performed += jumpInput; //Jump Input
        playerInput.Input.Dash.performed += dashInput; //Dash Input
    }

    /*  
     *  code ran when inputs are performed:
     *  Buttons only need to be checked when they are down while 
     *  vector inputs like movement need to be checked when pushed and released
     */
    public void movementInput(InputAction.CallbackContext context)
    {

    }

    public void jumpInput(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            _jump = true;
        }
    }

    public void dashInput(InputAction.CallbackContext context)
    {
        if(isGrounded && isDashCooledDown)
        {
            _dash = true;
        }
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        //Movement handlers
        handleMovement();
        handleRotation();
        handleDash();
        handleJump();
        

        // Ground Check
        GroundCheck();

        // Dash Debug
        DashDebug.text = "Dash Ready: " + isDashCooledDown;
    }

    void handleMovement()
    {
        deltaPosition = ((transform.forward * vert) + (transform.right * horz)) * moveSpeed * Time.fixedDeltaTime;

        float normalisedSlope = (_slopeAngle / 90f) * -1f;
        deltaPosition += (deltaPosition * normalisedSlope);

        rb.MovePosition(rb.position + deltaPosition);
    }

    void handleRotation()
    {

    }

    void handleDash()
    {
        // Dash
        if (_dash)
        {
            rb.velocity = new Vector3(x: rb.velocity.x + horz * dashForce,
                                      y: rb.velocity.y,
                                      z: rb.velocity.z + vert * dashForce);
            StartCoroutine(CoolDownWaiter());
            _dash = false;
        }
    }

    void handleJump()
    {
        // Jump
        if (_jump)
        {
            _jump = false;
            rb.velocity += (Vector3.up * jumpForce);
        }
    }


    void GetInput()
    {
        // Get axis
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
    }

    void GroundCheck()
    {
        // Check if we are on the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround))
        {
            _slopeAngle = (Vector3.Angle(hit.normal, transform.forward) - 90);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        // GroundDebug.text = "Grounded: " + isGrounded;
        GroundDebug.text = "Grounded: " + isGrounded;
    }

    IEnumerator CoolDownWaiter()
    {
        isDashCooledDown = false;
        yield return new WaitForSeconds(dashCoolDownSeconds);
        isDashCooledDown = true;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
}
