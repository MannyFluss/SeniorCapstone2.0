using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadPoster : MonoBehaviour
{
    [SerializeField] private GameObject Manager;
    [SerializeField] private Sprite posterImage;
    [SerializeField] private string message;
    [SerializeField] private GameObject interactSign;

    [SerializeField][ReadOnlyInspector] public bool Interactable = false;

    private void Start()
    {
        interactSign.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Interactable)
        {
            Interactable = false;
            StartCoroutine(Manager.GetComponent<InteractWithObject>().ShowPoster(posterImage, message));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactSign.GetComponent<SpriteRenderer>().enabled = true;
            Interactable = true;
        }
        return;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactSign.GetComponent<SpriteRenderer>().enabled = false;
            Interactable = false;
        }
        return;
    }
}
