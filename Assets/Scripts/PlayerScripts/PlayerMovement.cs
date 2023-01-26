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
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 0.25f;
    float maxJumpTime = 0.5f;


    // Debug Text Variable
    [Header("Debugging")]
    public Text GroundDebug;
    public Text DashDebug;

    // In-Script Variable
    CharacterController characterController;
    Vector3 currentMovement;
    Vector2 currentMovementInput;
    bool movementPressed;

    bool _jump;
    bool _dash;
    float _slopeAngle;
    Vector3 deltaPosition;

    float gravity = -9.8f;
    float groundedGravity = -0.5f;


    //Player Controls
    private PlayerInput playerInput;

    //main virtual cam
    [Header("Camera")]
    [SerializeField]
    private CinemachineVirtualCamera vcam;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        setupJump();
    }

    void Start()
    {

        //Player input listeners for vectors
        playerInput.Input.Move.started += movementInput;
        playerInput.Input.Move.performed += movementInput;
        playerInput.Input.Move.canceled += movementInput;

        //Player input listeners for buttons
        playerInput.Input.Jump.started += jumpInput; //Jump Input
        playerInput.Input.Jump.canceled += jumpInput; //Jump Input
        playerInput.Input.Dash.performed += dashInput; //Dash Input
    }

    /*  
     *  code ran when inputs are performed:
     *  Buttons only need to be checked when they are down while 
     *  vector inputs like movement need to be checked when pushed and released
     */
    public void movementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        movementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    public void jumpInput(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    public void dashInput(InputAction.CallbackContext context)
    {
        if(characterController.isGrounded && isDashCooledDown)
        {
            _dash = true;
        }
    }

    void setupJump()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    private void Update()
    {

        //Movement handlers
        handleJump();
        handleRotation();
        handleDash();
        handleMovement();

        handleGravity();
    }

    private void FixedUpdate()
    {

        // Dash Debug
        DashDebug.text = "Dash Ready: " + isDashCooledDown;
    }

    void handleMovement()
    {
        characterController.Move(currentMovement * Time.deltaTime * moveSpeed);



        /*deltaPosition = ((transform.forward * vert) + (transform.right * horz)) * moveSpeed * Time.fixedDeltaTime;

        float normalisedSlope = (_slopeAngle / 90f) * -1f;
        deltaPosition += (deltaPosition * normalisedSlope);

        rb.MovePosition(rb.position + deltaPosition);*/
    }

    void handleRotation()
    {

    }

    void handleDash()
    {
        // Dash
        if (_dash)
        {
            characterController.Move(currentMovement * Time.deltaTime * moveSpeed * dashForce * 5);
            StartCoroutine(CoolDownWaiter());
            _dash = false;
        }
    }

    void handleJump()
    {
        if (characterController.isGrounded && isJumpPressed)
        {
            _jump = true;
            currentMovement.y = initialJumpVelocity;
        }
    }

    void handleGravity()
    {
        if(characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
        }
        else
        {
            currentMovement.y += gravity * Time.deltaTime;
        }
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
