using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CharacterAttack : MonoBehaviour
{

    //adding slash sound effect
    [SerializeField] private AudioSource SlashSoundEffect;

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
    private float hitboxCoolDown = 0.1f;
    public float atkCoolDown = 0.5f;

    private PlayerUI ui;

    CinemachineVirtualCamera vcam;
    Camera mainCam;

    //Player Controls
    private PlayerInput playerInput;
    [SerializeField]
    public GameObject AimPositionReference;
    [SerializeField]
    public GameObject TabHud,ShopHud;
    [SerializeField]
    private PlayerMovement myPlayerMovement; 
    public bool InputEnabled = true;

    private void Awake()
    {
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        mainCam = Camera.main;
        playerInput = new PlayerInput();
        myPlayerMovement = gameObject.GetComponent<PlayerMovement>();
        
        
    }

    void Start()
    {
        ui = GetComponentInChildren<PlayerUI>();

        playerInput.Input.Hit.performed += hitInput;

    }

    public void hitInput(InputAction.CallbackContext context)
    {
        if (!InputEnabled){return;}
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
        var worldToScreen = mainCam.WorldToScreenPoint(myPlayerMovement.currentFacingDirection * 5);
        var ray = mainCam.ScreenPointToRay(worldToScreen);

        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            Vector3 direction = hitInfo.point;
            direction.y = 0;
            aimArrow.forward = Quaternion.Euler(0, 90, 0) * (direction - new Vector3(transform.position.x, 0, transform.position.z));
        }
        Vector3 aimTemp = myPlayerMovement.currentFacingDirection; 
        aimTemp.y = 0;

        aimArrow.rotation = Quaternion.Euler(0, 90, 0) * Quaternion.LookRotation(aimTemp);
        

        
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
        SlashSoundEffect.Play();
        canHit = false;
        hitObj.SetActive(true);
        yield return new WaitForSeconds(hitboxCoolDown);
        hitObj.SetActive(false);
        yield return new WaitForSeconds(atkCoolDown);
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
