using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DockBehavior : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    void Start()
    {
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            vcam.Priority = 10;
            vcam.m_Follow = other.transform;
            
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        vcam.Priority = 0;
    }
}
