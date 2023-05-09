using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveManager : MonoBehaviour
{
    Valve[] valves = new Valve[5];

    [HideInInspector]
    public bool isComplete = false;

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
            stopValves();
            ttbm.TicTicBoomSolved();
        }
    }

    public void startValves()
    {
        isComplete = false;

        //pick 2 random valves
        var v1 = Random.Range(0, 4);
        var v2 = Random.Range(0, 4);


        for (int i = 0; i < valves.Length; i++)
        {
            //set all interactbles to true
            valves[i].isInteractable = true;
            valves[i].valveIndex = i;

            //determine 2 random valves and set them to on
            if(i == v1 || i == v2)
            {
                valves[i].valveState = true;
            }
        }
    }

    public void stopValves()
    {
        for (int i = 0; i < valves.Length; i++)
        {
            //set all interactbles and states to false
            valves[i].isInteractable = false;
            valves[i].valveState = false;

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
            valves[valves.Length - 1].toggleValveState();
            valves[pos].toggleValveState();
            valves[pos + 1].toggleValveState();
        }
        else if (pos == valves.Length - 1)
        {
            valves[pos - 1].toggleValveState();
            valves[pos].toggleValveState();
            valves[0].toggleValveState();
        }
        else
        {
            valves[pos - 1].toggleValveState();
            valves[pos].toggleValveState();
            valves[pos + 1].toggleValveState();
        }
    }
}
