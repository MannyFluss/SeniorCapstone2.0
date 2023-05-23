using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject targetParent;
    [SerializeField] private GameObject Vee;

    private Renderer[] rend;

    void Start()
    {
        rend = targetParent.GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Renderer r in rend) r.enabled = false;
            Vee.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Renderer r in rend) r.enabled = true;
            Vee.GetComponent<Renderer>().enabled = true;
        }
    }
}
