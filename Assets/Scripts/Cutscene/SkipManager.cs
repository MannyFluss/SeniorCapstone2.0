using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SkipManager : MonoBehaviour
{
    [Header("Target Scene")]
    public Button targetButton;
    public string sceneName;
    public bool firstClick;

    private PlayerInput playerInput;
    

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Input.Jump.performed += detectController;


        targetButton.gameObject.SetActive(false);
        firstClick = false;
    }

    void Update()
    {
        if (!firstClick)
            DetectMouseClick();
        else
        {

        }
    }

    private void detectController(InputAction.CallbackContext context)
    {
        targetButton.gameObject.SetActive(true);
    }

    private void DetectMouseClick()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            firstClick = true;
            targetButton.gameObject.SetActive(true);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
