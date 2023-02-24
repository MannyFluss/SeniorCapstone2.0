using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    //Player Controls
    private PlayerInput playerInput;
    private PlayerManager playerManager;

    //Canvas
    private Canvas canvas;

    //Stats
    [Header("Player Stats")]
    [SerializeField]
    private TMP_Text _hp;

    [SerializeField]
    private GameObject dodgeIcon;
    private float dodgeTime = 0;
    private bool hasDodged = false;

    //Ability menu
    public bool _abilityMenuActive = false;


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerManager = GetComponentInParent<PlayerManager>();

        canvas = GetComponent<Canvas>();
    }

    void Start()
    {
        //canvas.GetComponentInChildren<pauseMenu>(true).gameObject.SetActive(false);

        playerInput.Input.Dash.performed += dashInput; //Dash Input
        playerInput.Input.AbilityMenu.performed += abilityMenuInput;
    }

    public void dashInput(InputAction.CallbackContext context)
    {
        if(!hasDodged)
        {
            dodgeTime = 1;
            StartCoroutine(dodgeCooldown());
        }
        
    }
    /// <summary>
    /// Sets ability menu to active
    /// </summary>
    /// <param name="context"></param>
    public void abilityMenuInput(InputAction.CallbackContext context)
    {
        _abilityMenuActive = !_abilityMenuActive;
        if (_abilityMenuActive)
        {
            canvas.GetComponentInChildren<pauseMenu>(true).gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            canvas.GetComponentInChildren<pauseMenu>(true).gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleDodgeIcon();
        handleStats();
    }

    void handleStats()
    {
        //_hp.text = "HP: " + playerManager.health;
    }

    void handleDodgeIcon()
    {
        if (dodgeTime > 0)
        {
            dodgeTime -= Time.deltaTime;
        }
        dodgeIcon.GetComponentInChildren<Image>().transform.localScale = new Vector3(1, dodgeTime);
    }

    IEnumerator dodgeCooldown()
    {
        hasDodged = true;
        yield return new WaitForSeconds(1f);
        hasDodged = false;
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
