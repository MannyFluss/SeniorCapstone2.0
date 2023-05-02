using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DialogueScript : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas myCanvas;
    TextMeshProUGUI myText;
    
    public string[] lines;
    public float textSpeed;
    private int index;
    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    void Start()
    {
        playerInput.Input.Jump.started += PLEASEWORK; 
        startDialogue();

    }

    public void PLEASEWORK(InputAction.CallbackContext context)
    {
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
        myText = GetComponentInChildren<TextMeshProUGUI>();
        myCanvas = GetComponentInChildren<Canvas>();

        myCanvas.gameObject.SetActive(true);
        myText.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
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
        myCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
    }
    // Update is called once per frame

}
