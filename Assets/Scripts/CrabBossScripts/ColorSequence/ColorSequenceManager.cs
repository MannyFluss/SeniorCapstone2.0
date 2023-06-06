using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorSequenceManager : MonoBehaviour
{
    ColorTiles ct;
    DrKrabManager dkm;

    [SerializeField]
    private GameObject screen1;
    [SerializeField]
    private GameObject screen2;
    [SerializeField]
    private GameObject spotlights;


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
    MeshRenderer mr1;
    MeshRenderer mr2;
    [SerializeField][ReadOnlyInspector] Light[] lights;
    [SerializeField] private float maxIntensity = 360;
    [SerializeField] private float minIntensity = 100;
    [SerializeField] private float lightRate;
    //pipes and materials
    private Material[] greenMats;
    private Material[] redMats;


    //Timer
    private float timer;
    TMP_Text timerText1;
    TMP_Text timerText2;
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
        lights = spotlights.GetComponentsInChildren<Light>();

        dkm = GetComponentInParent<DrKrabManager>();
        ct = GetComponentInChildren<ColorTiles>();
        mr1 = screen1.GetComponent<MeshRenderer>();
        mr2 = screen2.GetComponent<MeshRenderer>();
        timerText1 = screen1.GetComponentInChildren<TMP_Text>();
        timerText2 = screen2.GetComponentInChildren<TMP_Text>();

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
        //timerHandler();
        valveHandler();
    }

    //private void timerHandler()
    //{
    //    if(!runTimer)
    //    {
    //        return;
    //    }
    //    timerText.text = ((int)timer).ToString();
    //    timer -= Time.deltaTime;

    //    if(timer <= 0)
    //    {
    //        Debug.Log("failed");
    //    }
    //}

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
        // Dim the Lights so the panel are more visible
        StartCoroutine(DimLights());

        ct.disableTiles();
        mr1.material = def;
        mr2.material = def;
        timerText1.text = "Round " + (puzzlesComplete + 1);
        timerText2.text = "Round " + (puzzlesComplete + 1);

        yield return new WaitForSeconds(2f);

        runTimer = false;
        timerText1.text = "";
        timerText2.text = "";

        foreach (string color in ct.sequence)
        {
            if (color == "") break;
            switch(color)
            {
                case "green":
                    greenTile.GetComponent<Tile>().SFX.Play();
                    mr1.material = green;
                    mr2.material = green;
                    greenTile.GetComponent<Tile>().lightOnTile();
                    yield return new WaitForSeconds(1f);
                    greenTile.GetComponent<Tile>().lightOffTile();
                    break;
                case "red":
                    redTile.GetComponent<Tile>().SFX.Play();
                    mr1.material = red;
                    mr2.material = red;
                    redTile.GetComponent<Tile>().lightOnTile();
                    yield return new WaitForSeconds(1f);
                    redTile.GetComponent<Tile>().lightOffTile();
                    break;
                case "yellow":
                    yellowTile.GetComponent<Tile>().SFX.Play();
                    mr1.material = yellow;
                    mr2.material = yellow;
                    yellowTile.GetComponent<Tile>().lightOnTile();
                    yield return new WaitForSeconds(1f);
                    yellowTile.GetComponent<Tile>().lightOffTile();
                    break;
                case "blue":
                    blueTile.GetComponent<Tile>().SFX.Play();
                    mr1.material = blue;
                    mr2.material = blue;
                    blueTile.GetComponent<Tile>().lightOnTile();
                    yield return new WaitForSeconds(1f);
                    blueTile.GetComponent<Tile>().lightOffTile();
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
        mr1.material = def;
        mr2.material = def;
        runTimer = true;

        // Rebrighten the lights
        StartCoroutine(BrigthenLights());
        ct.enableTiles();
    }

    public IEnumerator puzzleFailed()
    {
        StopAllCoroutines();
        runTimer = false;
        timerText1.text = "YOU SUCK";
        timerText2.text = "LOSER XD";

        mr1.material = red;
        mr2.material = red;
        yield return new WaitForSeconds(1f);
        mr1.material = def;
        mr2.material = def;
        timerText1.text = "";
        timerText2.text = "";
        yield return new WaitForSeconds(1f);
        StartCoroutine(playColors());
    }

    public IEnumerator puzzleComplete()
    {
        StopAllCoroutines();
        runTimer = false;
        timerText1.text = "GOOD STUFF";
        timerText2.text = "WELL DONE";
        mr1.material = green;
        mr2.material = green;

        yield return new WaitForSeconds(1f);

        mr1.material = def;
        mr2.material = def;

        yield return new WaitForSeconds(1f);
        newSequence();
    }

    public void stunSequence()
    {
        ct.disableTiles();
        runTimer = false;
        timerText1.text = "WOW!!!";
        timerText2.text = "NICE!!!";
        dkm.stunSequence();
    }

    IEnumerator DimLights()
    {
        for (float i = maxIntensity; i >= minIntensity; i -= lightRate * Time.deltaTime)
        {
            //Dim the lights
            for (int l = 1; l < lights.Length; l++)
            {
                lights[l].intensity = i;
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator BrigthenLights()
    {
        for (float i = minIntensity; i <= maxIntensity; i += lightRate * Time.deltaTime)
        {
            //Dim the lights
            for (int l = 1; l < lights.Length; l++)
            {
                lights[l].intensity = i;
            }
            yield return null;
        }
        yield return null;
    }

}
