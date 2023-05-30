using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorSequenceManager : MonoBehaviour
{
    ColorTiles ct;
    DrKrabManager dkm;

    [SerializeField]
    private GameObject screen;

    [SerializeField]
    Material red;
    [SerializeField]
    Material blue;
    [SerializeField]
    Material yellow;
    [SerializeField]
    Material green;
    [SerializeField]
    Material def;

    [Header("Tile GameObject")]
    [SerializeField]
    GameObject redTile;
    [SerializeField]
    GameObject blueTile;
    [SerializeField]
    GameObject yellowTile;
    [SerializeField]
    GameObject greenTile;

    [Header("Other")]
    MeshRenderer mr;
    //pipes and materials
    private Material[] greenMats;
    private Material[] redMats;


    //Timer
    private float timer;
    TMP_Text timerText;
    private bool runTimer;

    //Number of puzzles
    public int puzzlesComplete = 0;

    private int n1 = 3;
    private int n2 = 4;

    //
    [SerializeField]
    GameObject valves;
    
    

    void Start()
    {
        dkm = GetComponentInParent<DrKrabManager>();
        ct = GetComponentInChildren<ColorTiles>();
        mr = screen.GetComponent<MeshRenderer>();
        timerText = screen.GetComponentInChildren<TMP_Text>();

        colorSequence(100, 100f);

        //get red mat
        redMats = valves.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
        redMats[1] = red;
        //get original green mat array
        greenMats = valves.transform.GetChild(0).GetComponent<MeshRenderer>().materials;


    }

    // Update is called once per frame
    void Update()
    {
        timerHandler();
        valveHandler();
    }

    private void timerHandler()
    {
        if(!runTimer)
        {
            return;
        }
        timerText.text = ((int)timer).ToString();
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Debug.Log("failed");
        }
    }

    private void valveHandler()
    {
        if(puzzlesComplete == 0)
        {
            valves.transform.GetChild(0).transform.localRotation = Quaternion.Lerp(valves.transform.GetChild(0).transform.localRotation, Quaternion.Euler(new Vector3(90, -90, 180)), Time.deltaTime / 2);
            valves.transform.GetChild(0).GetComponent<MeshRenderer>().materials = redMats;
            valves.transform.GetChild(1).transform.localRotation = Quaternion.Lerp(valves.transform.GetChild(1).transform.localRotation, Quaternion.Euler(new Vector3(90, -90, 180)), Time.deltaTime / 2);
            valves.transform.GetChild(1).GetComponent<MeshRenderer>().materials = redMats;
            valves.transform.GetChild(2).transform.localRotation = Quaternion.Lerp(valves.transform.GetChild(2).transform.localRotation, Quaternion.Euler(new Vector3(90, -90, 180)), Time.deltaTime / 2);
            valves.transform.GetChild(2).GetComponent<MeshRenderer>().materials = redMats;
        } 
        for(int i = 0; i < puzzlesComplete; i++)
        {
            valves.transform.GetChild(i).transform.localRotation = Quaternion.Lerp(valves.transform.GetChild(i).transform.localRotation, Quaternion.Euler(new Vector3(0, -90, 180)), Time.deltaTime / 2);
            valves.transform.GetChild(i).GetComponent<MeshRenderer>().materials = greenMats;
        }
    }

    public void colorSequence(int health, float maxHealth)
    {
        timer = 90;
        if (health/maxHealth >= 0.30f)
        {
            ct.maxColor = n1;
        }
        else
        {
            ct.maxColor = n2;
        }
        ct.puzzleActive = true;
        newSequence();
    }
    
    public void newSequence()
    {
        ct.pickTiles(ct.maxColor);
        StartCoroutine(playColors());
    }

    private IEnumerator playColors()
    {
        ct.disableTiles();
        mr.material = def;
        timerText.text = "Round " + (puzzlesComplete + 1);
        yield return new WaitForSeconds(2f);
        runTimer = false;
        timerText.text = "";
        foreach(string color in ct.sequence)
        {
            if (color == "") break;
            switch(color)
            {
                case "green":
                    mr.material = green;
                    greenTile.GetComponent<Tile>().lightOnTile();
                    yield return new WaitForSeconds(1f);
                    greenTile.GetComponent<Tile>().lightOffTile();
                    break;
                case "red":
                    mr.material = red;
                    redTile.GetComponent<Tile>().lightOnTile();
                    yield return new WaitForSeconds(1f);
                    redTile.GetComponent<Tile>().lightOffTile();
                    break;
                case "yellow":
                    mr.material = yellow;
                    yellowTile.GetComponent<Tile>().lightOnTile();
                    yield return new WaitForSeconds(1f);
                    yellowTile.GetComponent<Tile>().lightOffTile();
                    break;
                case "blue":
                    mr.material = blue;
                    blueTile.GetComponent<Tile>().lightOnTile();
                    yield return new WaitForSeconds(1f);
                    blueTile.GetComponent<Tile>().lightOffTile();
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
        mr.material = def;
        runTimer = true;
        ct.enableTiles();
    }

    public IEnumerator puzzleFailed()
    {
        StopAllCoroutines();
        runTimer = false;
        timerText.text = "YOU SUCK";
        mr.material = red;
        yield return new WaitForSeconds(1f);
        mr.material = def;
        timerText.text = "";
        yield return new WaitForSeconds(1f);
        StartCoroutine(playColors());
    }

    public IEnumerator puzzleComplete()
    {
        StopAllCoroutines();
        runTimer = false;
        timerText.text = "GOOD STUFF";
        mr.material = green;
        yield return new WaitForSeconds(1f);
        mr.material = def;
        yield return new WaitForSeconds(1f);
        newSequence();
    }

    public void stunSequence()
    {
        runTimer = false;
        timerText.text = "ALL 3 DONE";
        dkm.stunSequence();
    }
}
