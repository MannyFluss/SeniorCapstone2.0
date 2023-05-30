using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //parent class
    ColorTiles ct;

    //
    public string color;

    //Tile vars
    public bool tilePressed = false;
    public bool interactable = false;

    //"Light up"
    [HideInInspector]
    public Material mat;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        ct = GetComponentInParent<ColorTiles>();
    }

    //if tile is stoon on
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !tilePressed && interactable)
        {
            tilePressed = true;
            mat.EnableKeyword("_EMISSION");
            Debug.Log(other.gameObject.name);
            if(ct.puzzleActive)
            {
                ct.checkPuzzle(this);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tilePressed = false;
            mat.DisableKeyword("_EMISSION");
        }
    }
}
