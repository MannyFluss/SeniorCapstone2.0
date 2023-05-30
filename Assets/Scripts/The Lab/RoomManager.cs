using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Renderer> MainRoom = new List<Renderer>();
    [SerializeField] private GameObject Vee;

    [SerializeField] private SpriteRenderer[] interactableObjects;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Renderer r in MainRoom) r.enabled = false;
            foreach (SpriteRenderer r in interactableObjects) r.enabled = false;
            Vee.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Renderer r in MainRoom) r.enabled = true;
            foreach (SpriteRenderer r in interactableObjects) r.enabled = true;
            Vee.GetComponent<Renderer>().enabled = true;
        }
    }
}
