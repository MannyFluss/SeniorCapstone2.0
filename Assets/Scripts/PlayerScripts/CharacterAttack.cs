using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

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

    private PlayerUI ui;

    CinemachineVirtualCamera vcam;
    Camera mainCam;

    //Player Controls
    private PlayerInput playerInput;
    [SerializeField]
    public GameObject AimPositionReference;
    [SerializeField]
    public GameObject TabHud,ShopHud;


    private void Awake()
    {
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        mainCam = Camera.main;
        playerInput = new PlayerInput();
        
    }

    void Start()
    {
        ui = GetComponentInChildren<PlayerUI>();

        playerInput.Input.Hit.performed += hitInput;

    }

    public void hitInput(InputAction.CallbackContext context)
    {
        _hit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!ui._abilityMenuActive)
        {
            handleAim();
            handleHit();
        }
    }
    
    public Transform getAimArrow()
    {
        return aimArrow;
    }
    void handleHit()
    {
        if(canHit && _hit && ShopHud.activeSelf == false && TabHud.activeSelf==false)
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
        //added more accurate aiming
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            Vector3 direction = hitInfo.point;
            direction.y = 0;
            aimArrow.forward = Quaternion.Euler(0, 90, 0) * (direction - transform.position);
        }

        
        /*
        //If there is not a target group do usual calculations
        if(vcam.m_LookAt.gameObject.GetComponent<CinemachineTargetGroup>() == null)
        {
            //Gets mouse position in relation to center of screen
            mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2;
            mousePos.y -= Screen.height / 2;
        }
        //If there is target group do special calculations for new camera view
        else
        {
            mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2;
            mousePos.y -= Screen.height / 5f;
        }
        */
        

        //using the x and y positions of the mouse calculate the angle that the arrow will rotate
        //var rot = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //apply angle to arrow
        //aimArrow.rotation = Quaternion.Euler(0, -rot + 180, 0);
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
