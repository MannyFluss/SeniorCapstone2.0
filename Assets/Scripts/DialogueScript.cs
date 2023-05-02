using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class DialogueScript : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas myCanvas;
    TextMeshProUGUI myText;
    
    public string[] lines;
    public float textSpeed;
    private int index;
    private PlayerInput playerInput;


    [SerializeField]
    private UnityEvent onShopOpen;

    
    [SerializeField]
    private UnityEvent onShopClose;


    private void Awake()
    {
        playerInput = new PlayerInput();
        
    }
    void Start()
    {
        playerInput.Input.Jump.started += jumpInput; 
        startDialogue();

    }

    public void jumpInput(InputAction.CallbackContext context)
    {
        print(context);
        if (myText.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            myText.text = lines[index];
        }
    }

    



    public void startDialogue()
    {

        playerInput.Enable();
        myText = GetComponentInChildren<TextMeshProUGUI>();
        myCanvas = GetComponentInChildren<Canvas>();

        myCanvas.gameObject.SetActive(true);
        myText.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
        onShopOpen.Invoke();
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            myText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length -1)
        {
            index++;
            myText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            finishedDialogue();
        }
    }

    void finishedDialogue()
    {
        onShopClose.Invoke();
        myCanvas.gameObject.SetActive(false);

    }

}
