using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject targetParent;
    [SerializeField] private GameObject Vee;

    private Renderer[] rend1;
    [SerializeField] private SpriteRenderer[] interactableObjects;

    void Start()
    {
        rend1 = targetParent.GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Renderer r in rend1) r.enabled = false;
            foreach (SpriteRenderer r in interactableObjects) r.enabled = false;
            Vee.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Renderer r in rend1) r.enabled = true;
            foreach (SpriteRenderer r in interactableObjects) r.enabled = true;
            Vee.GetComponent<Renderer>().enabled = true;
        }
    }
}
