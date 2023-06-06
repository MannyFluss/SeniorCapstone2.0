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
    [SerializeField]
    Canvas myCanvas;
    [SerializeField]
    TextMeshProUGUI myText;
    
    [SerializeField]
    private GameObject nextLineIndicator;
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
        playerInput.Enable();
        myCanvas.gameObject.SetActive(false);

    }

    public void jumpInput(InputAction.CallbackContext context)
    {


        if (myCanvas.gameObject.activeInHierarchy == false)
        {
            return;
        }
        if (myText.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            myText.text = lines[index];
            nextLineIndicator.SetActive(true);
        }
    }

    



    public void startDialogue()
    {

        
        myCanvas.gameObject.SetActive(true);
        nextLineIndicator.SetActive(false);

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
        nextLineIndicator.SetActive(true);

    }

    void NextLine()
    {
        if (index < lines.Length -1)
        {
            index++;
            myText.text = string.Empty;
            StartCoroutine(TypeLine());
            nextLineIndicator.SetActive(false);
        }
        else
        {
            finishedDialogue();
        }
    }

    void finishedDialogue()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().TogglePlayerInput();
        onShopClose.Invoke();
        myCanvas.gameObject.SetActive(false);

    }

}
