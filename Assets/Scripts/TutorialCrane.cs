using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialCrane : MonoBehaviour
{
    [SerializeField]
    GameObject shopKeeper;
    [SerializeField]
    GameObject tentacle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Attack" && !shopKeeper.activeSelf)
        {
            tentacle.SetActive(false);
        }
    }
}
