using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class CraneBehavior : MonoBehaviour
{
    [SerializeField]
    PlayableDirector pd;
    bool timeLineStarted = false;

    [SerializeField]
    CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Attack" && !timeLineStarted)
        {
            pd.Play();
            Debug.Log("play");
            vcam.Priority = 11;
        }
    }

    public void stopTimeline()
    {
        timeLineStarted = false;
        vcam.Priority = 0;
    }
}
