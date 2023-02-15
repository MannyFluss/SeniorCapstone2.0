using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle
{
    public GameObject tentacleObject { get; set; }
    public bool slam { get; set; }
    public Quaternion rotateTo { get; set; }
}

public class BossBehavior : MonoBehaviour
{
    //Boss properties
    private int stage = 2;
    private int health = 100;

    //attack randomization
    string[] attackList = new string[5];
    string[] possibleAttacks = { "return to sender", "tentacle frenzy", "wipe out" };

    //tentacle frenzy vars
    GameObject[] indicators =   new GameObject[4];
    Tentacle[] tentacles  =   new Tentacle[4];

    [SerializeField]
    GameObject tentacle1, tentacle2, tentacle3, tentacle4;
    
    [SerializeField]
    private GameObject indicator;



    // Start is called before the first frame update
    void Start()
    {
        tentacles[0] = new Tentacle();
        tentacles[1] = new Tentacle();
        tentacles[2] = new Tentacle();
        tentacles[3] = new Tentacle();
        tentacles[0].tentacleObject = tentacle1;
        tentacles[1].tentacleObject = tentacle2;
        tentacles[2].tentacleObject = tentacle3;
        tentacles[3].tentacleObject = tentacle4;
        tentacles[0].slam = false;
        tentacles[1].slam = false;
        tentacles[2].slam = false;
        tentacles[3].slam = false;

        getAttackPattern();
        attack();
        attackTentacleFrenzy();

        
    }

    // Update is called once per frame
    void Update()
    {
        healthCheckHandler();
    }

    private void FixedUpdate()
    {
        tentacleFrenzyHandler();
    }

    /// <summary>
    /// Code to watch the health of the boss
    /// </summary>
    private void healthCheckHandler()
    {
        if(health < 60 && health >= 30)
        {
            stage = 2;
        }
        else if(health < 30)
        {
            stage = 3;
        }
    }

    private void tentacleFrenzyHandler()
    {
        for(int i = 0; i < indicators.Length; i++)
        {
            if (indicators[i] != null)
            {
                indicators[i].transform.localScale = Vector3.Lerp(indicators[i].transform.localScale, new Vector3(13f, 1f, 1f), 0.03f);
                tentacles[i].tentacleObject.transform.position = Vector3.Lerp(tentacles[i].tentacleObject.transform.position, indicators[i].transform.position, 0.05f);
            }
        }
        for(int i = 0; i < tentacles.Length; i++)
        {
            if (tentacles[i].slam)
            {
                tentacles[i].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[i].tentacleObject.transform.localRotation, tentacles[i].rotateTo, 0.1f);
            }
        }
    }

    /// <summary>
    /// Randomizes the attacks for this phase
    /// </summary>
    private void getAttackPattern()
    {
        
        if (stage == 1)
        {
            for(int i = 0; i < 2; i++)
            {
                attackList[i] = possibleAttacks[Random.Range(0, possibleAttacks.Length)];
            }
            attackList[2] = "return to sender";
        }
        else if (stage == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                attackList[i] = possibleAttacks[Random.Range(0, possibleAttacks.Length)];
            }
            attackList[1] = "tentacle frenzy";
            attackList[3] = "return to sender";
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                attackList[i] = possibleAttacks[Random.Range(0, possibleAttacks.Length)];
            }
            attackList[0] = "tentacle frenzy";
            attackList[3] = "tentacle frenzy";
            attackList[4] = "return to sender";
        }
    }

    /// <summary>
    /// Runs the current set of attacks
    /// </summary>
    private void attack()
    {
        for (int i = 0; i < attackList.Length; i++)
        {
            if(attackList[i] != null)
            {
                Debug.Log(attackList[i]);
            }
            if (attackList[i] == "tentacle frenzy")
            {
                
            }
        }
    }

    private void attackTentacleFrenzy()
    {
        StartCoroutine(createIndicators(0.75f));
        //Wait for seconds based on version of the attack
        if (stage == 1)
        {
            StartCoroutine(attackInProgress(5f));
        }
        else if(stage == 2)
        {

        }
        else
        {

        }
        
    }

    private void attackWipeOut()
    {

    }

    private void attackReturnToSender()
    {

    }

    IEnumerator attackInProgress(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    IEnumerator createIndicators(float seconds)
    {
        for (int i = 0; i < stage + 2; i++)
        {
            int rotationInt = Random.Range(65, 115);
            indicators[i] = Instantiate(indicator, new Vector3(Random.Range(-13, 13), 0.01f, 32.5f), Quaternion.Euler(0, rotationInt, 0));
            tentacles[i].tentacleObject.transform.localRotation = Quaternion.Euler(0, 0, 90 - rotationInt);
            StartCoroutine(waitForSlam(i));
            yield return new WaitForSeconds(seconds);
        }  
    }

    IEnumerator waitForSlam(int index)
    {
        //getting rotation to lerp to
        
        
        yield return new WaitForSeconds(2.5f);
        tentacles[index].rotateTo = tentacles[index].tentacleObject.transform.localRotation * Quaternion.Euler(new Vector3(90, 0, 0));

        tentacles[index].slam = true;
        yield return new WaitForSeconds(1f);
        Destroy(indicators[index]);
    }
}


