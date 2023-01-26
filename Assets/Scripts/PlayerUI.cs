using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //Player Controls
    private PlayerInput playerInput;

    //Canvas
    private Canvas canvas;

    [SerializeField]
    private GameObject dodgeIcon;
    private float dodgeTime = 0;
    private bool hasDodged = false;


    private void Awake()
    {
        playerInput = new PlayerInput();

        canvas = GetComponent<Canvas>();
    }

    void Start()
    {
        playerInput.Input.Dash.performed += dashInput; //Dash Input
    }

    public void dashInput(InputAction.CallbackContext context)
    {
        if(!hasDodged)
        {
            dodgeTime = 1;
            StartCoroutine(dodgeCooldown());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        handleDodgeIcon();
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
