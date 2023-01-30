using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    //For aim
    [Header("Aim")]
    [SerializeField]
    private Transform aimArrow;
    private Vector3 mousePos;

    //For hit
    [Header("Hit")]
    [SerializeField]
    private GameObject hitObj;
    private bool _hit = false;
    private bool canHit = true;
    private float hitCoolDown = 0.1f;
    

    //Player Controls
    private PlayerInput playerInput;


    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    void Start()
    {
        playerInput.Input.Hit.performed += hitInput;
    }

    public void hitInput(InputAction.CallbackContext context)
    {
        _hit = true;
    }

    // Update is called once per frame
    void Update()
    {
        handleAim();
        handleHit();
    }

    void handleHit()
    {
        if(canHit && _hit)
        {
            _hit = false;
            StartCoroutine(hit());
        }
    }

    /// <summary>
    /// Handles the aim of the attack using the mouse position relative to the center of the screen
    /// </summary>
    void handleAim()
    {
        //Gets mouse position in relation to center of screen
        mousePos = Input.mousePosition;
        mousePos.x -= Screen.width / 2;
        mousePos.y -= Screen.height / 2;

        //using the x and y positions of the mouse calculate the angle that the arrow will rotate
        var rot = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //apply angle to arrow
        aimArrow.rotation = Quaternion.Euler(0, -rot + 180, 0);
    }

    IEnumerator hit()
    {
        canHit = false;
        hitObj.SetActive(true);
        yield return new WaitForSeconds(hitCoolDown);
        hitObj.SetActive(false);
        canHit = true;
        
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
