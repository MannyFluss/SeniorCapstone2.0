using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTabContainerScript : MonoBehaviour
{
    [SerializeField]
    KeyCode inputKey = KeyCode.Tab;
    [SerializeField]
    GameObject PlayerTab;

    void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            //just pressed
            PlayerTab.SetActive(true);
        }     
        if (Input.GetKeyUp(inputKey))
        {
            PlayerTab.SetActive(false);
        }
    }
}
