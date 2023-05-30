using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTiles : MonoBehaviour
{
    //ColorSequenceManager
    ColorSequenceManager csm;

    //base vars
    string[] colors = { "green", "blue", "red", "yellow" };
    [HideInInspector]
    public string[] sequence = new string[5];

    Tile[] tiles;

    public int maxColor = 4;
    private int currColor = 0;

    //Puzzle active
    [HideInInspector]
    public bool puzzleActive = false;



    private void Start()
    {
        //pickTiles();
        //tiles = GetComponentsInChildren<Tile>();
        csm = GetComponentInParent<ColorSequenceManager>();
        tiles = GetComponentsInChildren<Tile>();
    }
    
    public void checkPuzzle(Tile tile)
    {
        if(sequence[currColor] == tile.color)
        {
            currColor++;
        }
        else
        {
            puzzleFailed();
        }

        //check if puzzle is complete
        if(currColor == maxColor)
        {
            puzzleComplete();
        }
    }


    public void pickTiles()
    {
        currColor = 0;
        for(int i = 0; i < maxColor ; i++)
        {
            if (i == 0)
            {
                sequence[0] = colors[Random.Range(0, 4)];
            }
            else
            {
                //get random color
                var colorRequest = colors[Random.Range(0, 4)];

                //if color is same as last sequence find new color and push it out
                while (colorRequest == sequence[i-1])
                {
                    colorRequest = colors[Random.Range(0, 4)];
                }
                sequence[i] = colorRequest;
            }
        }
    }

    private void puzzleFailed()
    {
        currColor = 0;
        StartCoroutine(csm.puzzleFailed());
    }

    private void puzzleComplete()
    {
        csm.puzzlesComplete++;
        if(csm.puzzlesComplete == 3)
        {
            csm.stunSequence();
        }
        else
        {
            StartCoroutine(csm.puzzleComplete());
        }
    }

    public void enableTiles()
    {
        foreach(Tile t in tiles)
        {
            t.interactable = true;
        }
    }

    public void disableTiles()
    {
        foreach (Tile t in tiles)
        {
            t.interactable = false;
        }
    }
}
