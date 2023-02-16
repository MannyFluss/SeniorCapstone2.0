using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle
{
    public GameObject tentacleObject { get; set; }

    public Vector3 originalPosition { get; set; }

    public bool slam { get; set; }

    public bool wipe { get; set; }

    public bool sweep { get; set; }

    public bool moveBack { get; set; }

    public Quaternion rotateTo { get; set; }
    
    public Quaternion rotateFrom { get; set; }


}

public class BossBehavior : MonoBehaviour
{
    //Boss properties
    private int stage = 2;
    private int health = 100;
    bool tentacleFrenzyInProgress = false;
    bool wipeOutInProgress = false;
    bool returnToSenderInProgress = true;


    //attack randomization
    string[] attackList = new string[5];
    string[] possibleAttacks = { "return to sender", "tentacle frenzy", "wipe out" };

    //tentacle frenzy vars
    GameObject[] indicators =   new GameObject[4];
    GameObject[] roundIndicators = new GameObject[16];
    Tentacle[] tentacles  =   new Tentacle[4];

    [SerializeField]
    GameObject tentacle1, tentacle2, tentacle3, tentacle4;
    
    [SerializeField]
    private GameObject indicator;
    [SerializeField]
    private GameObject roundIndicator;
    [SerializeField]
    private GameObject[] trashList;


    void Start()
    { 
        //Really ugly way of initializing values for struct
        tentacles[0] = new Tentacle();
        tentacles[1] = new Tentacle();
        tentacles[2] = new Tentacle();
        tentacles[3] = new Tentacle();
        tentacles[0].tentacleObject = tentacle1;
        tentacles[1].tentacleObject = tentacle2;
        tentacles[2].tentacleObject = tentacle3;
        tentacles[3].tentacleObject = tentacle4;
        tentacles[0].originalPosition = tentacle1.transform.position;
        tentacles[1].originalPosition = tentacle2.transform.position;
        tentacles[2].originalPosition = tentacle3.transform.position;
        tentacles[3].originalPosition = tentacle4.transform.position;
        tentacles[0].slam = false;
        tentacles[1].slam = false;
        tentacles[2].slam = false;
        tentacles[3].slam = false;

        //Testing center
        getAttackPattern();
        attack();
        attackReturnToSender();

        
    }

    void Update()
    {
        healthCheckHandler();
    }

    private void FixedUpdate()
    {
        tentacleFrenzyHandler();
        wipeOutHandler();
        returnToSenderHandler();
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

    /// <summary>
    /// Handling things for Tentacle Frenzy that need to be in update such as lerping etc.
    /// </summary>
    private void tentacleFrenzyHandler()
    {
        if(!tentacleFrenzyInProgress)
        {
            return;
        }
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
                tentacles[i].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[i].tentacleObject.transform.localRotation, tentacles[i].rotateTo, 0.2f);
            }
            if (tentacles[i].moveBack)
            {
                tentacles[i].tentacleObject.transform.position = Vector3.Lerp(tentacles[i].tentacleObject.transform.position, tentacles[i].originalPosition, 0.01f);
                tentacles[i].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[i].tentacleObject.transform.localRotation, Quaternion.Euler(0,0,0), 0.05f);
            }
            else if (!tentacles[i].slam && tentacles[i].tentacleObject.transform.localRotation != tentacles[i].rotateFrom)
            {
                tentacles[i].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[i].tentacleObject.transform.localRotation, tentacles[i].rotateFrom, 0.01f);
            }
            
        }
    }

    /// <summary>
    /// Handling things for Wipe Out that need to go in update
    /// </summary>
    private void wipeOutHandler()
    {
        if(!wipeOutInProgress)
        {
            return;
        }
        if (tentacles[0].wipe)
        {
            if(tentacles[0].slam)
            {
                tentacles[0].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[0].tentacleObject.transform.localRotation, tentacles[0].rotateTo, 0.2f);
            }
            if (tentacles[0].sweep)
            {
                tentacles[0].tentacleObject.transform.localPosition = Vector3.Lerp(tentacles[0].tentacleObject.transform.localPosition, new Vector3(-8, 3, -6f), 0.03f);
            }
            else
            {
                tentacles[0].tentacleObject.transform.localPosition = Vector3.Lerp(tentacles[0].tentacleObject.transform.localPosition, new Vector3(8.5f, 3, -6f), 0.05f);
            }
        }
        else if (tentacles[3].wipe)
        {   
            if (tentacles[3].slam)
            {
                tentacles[3].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[3].tentacleObject.transform.localRotation, tentacles[3].rotateTo, 0.2f);
            }
            if (tentacles[3].sweep)
            {
                tentacles[3].tentacleObject.transform.localPosition = Vector3.Lerp(tentacles[3].tentacleObject.transform.localPosition, new Vector3(8.5f, 3, -6f), 0.03f);
            }
            else
            {
                tentacles[3].tentacleObject.transform.localPosition = Vector3.Lerp(tentacles[3].tentacleObject.transform.localPosition, new Vector3(-8, 3, -6f), 0.05f);
            }
        }
        if(tentacles[0].moveBack)
        {
            tentacles[0].tentacleObject.transform.position = Vector3.Lerp(tentacles[0].tentacleObject.transform.position, tentacles[0].originalPosition, 0.01f);
            tentacles[0].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[0].tentacleObject.transform.localRotation, Quaternion.Euler(0, 0, 0), 0.05f);
        }
        if (tentacles[3].moveBack)
        {
            tentacles[3].tentacleObject.transform.position = Vector3.Lerp(tentacles[3].tentacleObject.transform.position, tentacles[3].originalPosition, 0.01f);
            tentacles[3].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[3].tentacleObject.transform.localRotation, Quaternion.Euler(0, 0, 0), 0.05f);
        }
    }

    /// <summary>
    /// Handling things for Return to Sender that need to go in update
    /// </summary>
    private void returnToSenderHandler()
    {
        if(!returnToSenderInProgress)
        {
            return;
        }
        for(int i = 0; i < roundIndicators.Length; i++)
        {
            if (roundIndicators[i] != null)
            {
                roundIndicators[i].transform.localScale = Vector3.Lerp(roundIndicators[i].transform.localScale, new Vector3(3, roundIndicators[i].transform.localScale.y, 3), 0.05f);
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

    /// <summary>
    /// Changes time value for attack based on which version of attack. further stages need more time for attack to perform
    /// </summary>
    private void attackTentacleFrenzy()
    {
        StartCoroutine(createIndicatorsTentacleFrenzy(0.75f));
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
        StartCoroutine(wipeOutTiming());
    }

    private void attackReturnToSender()
    {
        StartCoroutine(returnToSenderTiming());
    }

    IEnumerator attackInProgress(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    /// <summary>
    /// Code for making the indicators shown in tentacle frenzy
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator createIndicatorsTentacleFrenzy(float seconds)
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

    /// <summary>
    /// Code for 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator waitForSlam(int index)
    {
        yield return new WaitForSeconds(2.5f);
        //Get initial and ending rotation for the lerp movement
        tentacles[index].rotateFrom = tentacles[index].tentacleObject.transform.localRotation;
        tentacles[index].rotateTo = tentacles[index].tentacleObject.transform.localRotation * Quaternion.Euler(new Vector3(90, 0, 0));

        //Destroy indicatiors and timing for slam movement
        tentacles[index].slam = true;
        yield return new WaitForSeconds(1f);
        Destroy(indicators[index]);
        yield return new WaitForSeconds(1f);
        tentacles[index].slam = false;
        yield return new WaitForSeconds(1f);

        tentacles[index].moveBack = true;
    }

    IEnumerator wipeOutTiming()
    {
        //If hp is less than half make sweeps 3
        int sweeps = 2;
        if (health < 50)
        {
            sweeps = 3;
        }

        bool firstTentacle = true;//Random.Range(0, 2) != 0;

        for (int i = 0; i < sweeps; i++)
        {
            if (firstTentacle)
            {
                tentacles[0].moveBack = false;
                tentacles[0].wipe = true;
                yield return new WaitForSeconds(1f);
                tentacles[0].rotateTo = tentacles[0].tentacleObject.transform.localRotation * Quaternion.Euler(new Vector3(90, 0, 0));
                tentacles[0].slam = true;
                yield return new WaitForSeconds(1.5f);
                tentacles[0].sweep = true;
                yield return new WaitForSeconds(3f);
                tentacles[0].moveBack = true;
                tentacles[0].wipe = false;
                tentacles[0].sweep = false;
                tentacles[0].slam = false;
                firstTentacle = !firstTentacle;
            }
            else
            {
                tentacles[3].moveBack = false;
                tentacles[3].wipe = true;
                yield return new WaitForSeconds(1f);
                tentacles[3].rotateTo = tentacles[3].tentacleObject.transform.localRotation * Quaternion.Euler(new Vector3(90, 0, 0));
                tentacles[3].slam = true;
                yield return new WaitForSeconds(1.5f);
                tentacles[3].sweep = true;
                yield return new WaitForSeconds(3f);
                tentacles[3].moveBack = true;
                tentacles[3].wipe = false;
                tentacles[3].sweep = false;
                tentacles[3].slam = false;
                firstTentacle = !firstTentacle;
            }
            yield return new WaitForSeconds(3f);

        }
    }

    IEnumerator returnToSenderTiming()
    {
        //If hp is less than half make sweeps 3
        int throws = 10;
        if (health < 60)
        {
            throws = 16;
        }
        for (int i = 0; i < throws + 1; i ++)
        {
            roundIndicators[i] = Instantiate(roundIndicator, new Vector3(Random.Range(-20, 20), 0.01f, Random.Range(2, 28)), Quaternion.Euler(0, 0, 0));
            StartCoroutine(dropTrash(i));
            yield return new WaitForSeconds(Random.Range(0.2f, 0.7f));
        }
        
    }

    IEnumerator dropTrash(int index)
    {
        yield return new WaitForSeconds(1f);
        Instantiate(trashList[Random.Range(0, trashList.Length)], new Vector3(roundIndicators[index].transform.position.x, 30, roundIndicators[index].transform.position.z), Quaternion.Euler(0, 0, 0));
        
    }
}


