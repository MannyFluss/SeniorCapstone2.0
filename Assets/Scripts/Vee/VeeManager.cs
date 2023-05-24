using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeeManager : MonoBehaviour
{
    [SerializeField] public GameObject interactSign;
    [SerializeField] public GameObject DialogueSystem;

    private void Start()
    {
        interactSign.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        if (VeeMain.Instance.TalkToVee && Input.GetKeyDown(KeyCode.F))
        {

            DialogueSystem.GetComponent<SimpleDialogue>().talkToVee();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactSign.GetComponent<SpriteRenderer>().enabled = true;
            VeeMain.Instance.TalkToVee = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactSign.GetComponent<SpriteRenderer>().enabled = false;
            VeeMain.Instance.TalkToVee = false;
        }
    }
}
