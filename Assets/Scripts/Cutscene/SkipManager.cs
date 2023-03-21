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

    void Awake()
    {
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
