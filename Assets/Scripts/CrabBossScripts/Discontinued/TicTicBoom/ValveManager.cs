using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveManager : MonoBehaviour
{
    Valve[] valves = new Valve[5];

    [HideInInspector]
    public bool isComplete = false;
    
    [SerializeField]
    private bool sideValveToggle = false;

    private TicTicBoomManager ttbm;

    private void Start()
    {
        valves = GetComponentsInChildren<Valve>();
        ttbm = GetComponentInParent<TicTicBoomManager>();
    }

    private void Update()
    {
        if(!isComplete && 
            valves[0].valveState && valves[1].valveState && valves[2].valveState && valves[3].valveState && valves[4].valveState)
        {
            isComplete = true;
            resetAllValves();
            ttbm.TicTicBoomSolved();
        }
    }

    public void startValves()
    {
        isComplete = false;

        //pick 2 random, but different valves
        int v1 = Random.Range(0, 4);
        int v2;
        while(true)
        {
            v2 = Random.Range(0, 4);
            if (v2 != v1) break;
        }


        for (int i = 0; i < valves.Length; i++)
        {
            valves[i].valveIndex = i;
            valves[i].isInteractable = true;

            //determine 2 random valves and set them to off
            if(i == v1 || i == v2)
            {
                valves[i].valveState = true;
            }
            else valves[i].valveState = false;
        }
    }

    /// <summary>
    /// Switches valve states based on which valve was hit
    /// </summary>
    /// <param name="pos">Valve index</param>
    public void valveSwitch(int pos)
    {
        if (pos == 0)
        {
            if(sideValveToggle)
            {
                valves[valves.Length - 1].toggleValveState();
            }
            valves[pos].toggleValveState();
            valves[pos + 1].toggleValveState();
        }
        else if (pos == valves.Length - 1)
        {

            valves[pos - 1].toggleValveState();
            valves[pos].toggleValveState();
            if (sideValveToggle)
            {
                valves[0].toggleValveState();
            }
        }
        else
        {
            valves[pos - 1].toggleValveState();
            valves[pos].toggleValveState();
            valves[pos + 1].toggleValveState();
        }
    }

    // Reset all the valves to true state (visually)
    // Reset all the valves so they are not interactable.
    public void resetAllValves()
    {
        for (int i = 0; i < valves.Length; i++)
        {
            valves[i].valveState = false;
            valves[i].isInteractable = false;
        }
    }
}
