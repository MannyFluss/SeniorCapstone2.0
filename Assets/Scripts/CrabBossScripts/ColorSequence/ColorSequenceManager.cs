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

    MeshRenderer mr;

    //Timer
    private float timer;
    TMP_Text timerText;
    private bool runTimer;

    //Number of puzzles
    public int puzzlesComplete = 0;

    private int n1 = 4;
    private int n2 = 5;

    //
    [SerializeField]
    GameObject valves;
    
    

    void Start()
    {
        dkm = GetComponentInParent<DrKrabManager>();
        ct = GetComponentInChildren<ColorTiles>();
        mr = screen.GetComponent<MeshRenderer>();
        timerText = screen.GetComponentInChildren<TMP_Text>();
        colorSequence(100);
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
    }

    private void valveHandler()
    {
        if(puzzlesComplete == 0)
        {
            valves.transform.GetChild(0).transform.localRotation = Quaternion.Lerp(valves.transform.GetChild(0).transform.localRotation, Quaternion.Euler(new Vector3(0, 0, 90)), Time.deltaTime / 5);
            valves.transform.GetChild(0).GetComponent<MeshRenderer>().material = red;
            valves.transform.GetChild(1).transform.localRotation = Quaternion.Lerp(valves.transform.GetChild(1).transform.localRotation, Quaternion.Euler(new Vector3(0, 0, 90)), Time.deltaTime / 5);
            valves.transform.GetChild(1).GetComponent<MeshRenderer>().material = red;
            valves.transform.GetChild(2).transform.localRotation = Quaternion.Lerp(valves.transform.GetChild(2).transform.localRotation, Quaternion.Euler(new Vector3(0, 0, 90)), Time.deltaTime / 5);
            valves.transform.GetChild(2).GetComponent<MeshRenderer>().material = red;
        } 
        for(int i = 0; i < puzzlesComplete; i++)
        {
            valves.transform.GetChild(i).transform.localRotation = Quaternion.Lerp(valves.transform.GetChild(i).transform.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.deltaTime / 5);
            valves.transform.GetChild(i).GetComponent<MeshRenderer>().material = green;
        }
    }

    public void colorSequence(int health)
    {
        timer = 90;
        if(health > 60)
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
        ct.pickTiles();
        StartCoroutine(playColors());
    }

    private IEnumerator playColors()
    {
        mr.material = def;
        timerText.text = "Round " + (puzzlesComplete + 1);
        yield return new WaitForSeconds(2f);
        runTimer = false;
        timerText.text = "";
        foreach(string color in ct.sequence)
        {
            switch(color)
            {
                case "green":
                    mr.material = green;
                    break;
                case "red":
                    mr.material = red;
                    break;
                case "yellow":
                    mr.material = yellow;
                    break;
                case "blue":
                    mr.material = blue;
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
        mr.material = def;
        runTimer = true;
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
