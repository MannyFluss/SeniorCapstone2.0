using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    [HideInInspector]
    public bool valveState = false;
    [HideInInspector]
    public bool isInteractable = false;
    [HideInInspector]
    public int valveIndex;

    private Transform pipe;

    public bool isSingle = false;
    ValveManager vm;

    //prototype testing purpose
    MeshRenderer m;
    [SerializeField]
    Material red;
    [SerializeField]
    Material green;


    private void Start()
    {
        pipe = transform.GetChild(0);
        m = pipe.GetComponent<MeshRenderer>();
        if(!isSingle)
        {
            vm = GetComponentInParent<ValveManager>();
        }
    }

    private void FixedUpdate()
    {
        if(!valveState)
        {
            m.material = red;
            pipe.localRotation = Quaternion.Lerp(pipe.localRotation, Quaternion.Euler(pipe.localEulerAngles.x, pipe.localEulerAngles.y, 90), 0.05f);
        }
        else
        {
            m.material = green;
            pipe.localRotation = Quaternion.Lerp(pipe.localRotation, Quaternion.Euler(pipe.localEulerAngles.x, pipe.localEulerAngles.y, 0), 0.05f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isInteractable)
        {
            return;
        }

        if(other.tag == "Attack" && !isSingle)
        {
            vm.valveSwitch(valveIndex);
        }
        else if(other.tag == "Attack")
        {
            toggleValveState();
        }
    }






    public void toggleInteractable()
    {
        isInteractable = !isInteractable;
    }

    public void toggleValveState()
    {
        valveState = !valveState;
    }
}
