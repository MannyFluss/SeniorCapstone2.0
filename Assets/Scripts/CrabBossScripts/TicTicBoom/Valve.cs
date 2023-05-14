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


    public bool isSingle = false;
    ValveManager vm;

    private ParticleSystem ps;

    //prototype testing purpose
    MeshRenderer m;
    [SerializeField]
    Material red;
    [SerializeField]
    Material green;


    private void Start()
    {
        m = GetComponent<MeshRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();
        if(!isSingle)
        {
            vm = GetComponentInParent<ValveManager>();
        }
    }

    private void Update()
    {
        if(!valveState)
        {
            m.material = red;
            ps.Play();
        }
        else
        {
            m.material = green;
            ps.Stop();
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
