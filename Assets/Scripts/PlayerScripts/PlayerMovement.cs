using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEditor.ShaderKeywordFilter;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    //added Player dash sound effect
    [SerializeField] private AudioSource DashSoundEffect;
    //added Player Jump sound Effect
    [SerializeField] private AudioSource JumpSoundEffect;

    [Header("Movement Variable")]
    public float moveSpeed = 6f;
    public float jumpForce = 4f;
    public float dashForce = 4f;
    public float distToGround = 1f;

    // Variable for Dash
    [Header("Dash Variable")]
    public float dashCoolDownSeconds = 1f;
    public float cloneDeathTime = 0.08f;
    private Sprite sprite;
    private bool isDashCooledDown = true;

    // Variable for Jumping
    [Header("Jump Variable")]
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 0.4f;
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
    bool _hit;
    float _slopeAngle;
    Vector3 deltaPosition;

    Transform moveFrom;
    float knockback = 0.01f;

    float gravity = -9.8f;
    float groundedGravity = -0.5f;

    private List<GameObject> clones = new List<GameObject>();

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

        sprite = GetComponent<SpriteRenderer>().sprite;

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
            //dash sound effect
            DashSoundEffect.Play();
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
        if (DashDebug!=null)
        {
            DashDebug.text = "Dash Ready: " + isDashCooledDown;
        }

    }

    void handleMovement()
    {
        if (_hit)
        {
            transform.position = new Vector3(Vector3.MoveTowards(transform.position, moveFrom.position, -knockback).x, transform.position.y, Vector3.MoveTowards(transform.position, moveFrom.position, -knockback).z);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime * moveSpeed);
        }

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
            var storedPos = new Vector3[3]; 

            //Handle dash movement
            for(int i = 0; i < 3; i++)
            {
                characterController.Move(currentMovement * Time.deltaTime * moveSpeed * dashForce * 4);
                storedPos[i] = transform.position;
            }
            //Handle dash clones
            foreach (Vector3 pos in storedPos)
            {
                var clone = new GameObject();
                clone.transform.position = pos;
                var spriterender = clone.AddComponent<SpriteRenderer>();
                spriterender.sprite = sprite;
                spriterender.color = new Color(spriterender.color.r, spriterender.color.g, spriterender.color.b, 0.5f);
                clones.Add(clone);
            }
            StartCoroutine(CoolDownWaiter());
            _dash = false;
        }

        //clones for after effect
        foreach (var clone in clones)
        {
            var spriteRenderer = clone.GetComponent<SpriteRenderer>();
            var newAlpha = spriteRenderer.color.a - cloneDeathTime * Time.fixedDeltaTime;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            if (spriteRenderer.color.a <= 0)
            {
                clones.Remove(clone);
                Destroy(clone);
            }
        }
    }

    void handleJump()
    {
        if (characterController.isGrounded && isJumpPressed)
        {
            _jump = true;
            currentMovement.y = initialJumpVelocity;
            //jump sound effect
            JumpSoundEffect.Play();
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

    public void playerHit(Transform enemyPos)
    {
        moveFrom = enemyPos;
        StartCoroutine(knockbackState());
    }

    IEnumerator knockbackState()
    {
        _hit = true;
        yield return new WaitForSeconds(1f);
        _hit = false;
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
