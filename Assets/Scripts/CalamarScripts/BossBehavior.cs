using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.UI;

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
    private int stage = 1;
    public float health = 100;
    bool tentacleFrenzyInProgress = false;
    bool wipeOutInProgress = false;
    bool returnToSenderInProgress = false;
    bool moveBackInProgress = false;
    public bool canBeHit = false;

    //attack randomization
    string[] attackList = new string[20];
    string[] possibleAttacks = { "wipe out", "tentacle frenzy", "return to sender"};

    //tentacle frenzy vars
    GameObject[] indicators =   new GameObject[4];
    GameObject[] roundIndicators = new GameObject[16];
    Tentacle[] tentacles  =   new Tentacle[4];

    //return to sender vars
    GameObject[] trash = new GameObject[64];
    private int trashNum = 0;

    [SerializeField]
    GameObject tentacle1, tentacle2, tentacle3, tentacle4;
    
    [SerializeField]
    private GameObject indicator;
    [SerializeField]
    private GameObject roundIndicator;
    [SerializeField]
    private GameObject[] trashList;
    [SerializeField]
    private TMP_Text healthText;

    [SerializeField]
    PlayableDirector rightCrane;
    [SerializeField]
    PlayableDirector leftCrane;

    [SerializeField]
    private Image healthBar;


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

        runAttacks();
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
        moveBackHandler();
    }
    /// <summary>
    /// Code to watch the health of the boss
    /// </summary>
    private void healthCheckHandler()
    {
        if(health < 60 && health >= 30 && stage != 2)
        {
            changeStage(2);
        }
        else if(health < 30 && stage != 3)
        {
            changeStage(3);
        }
        healthBar.rectTransform.localScale = new Vector3(health / 100f, 1f, 1f); 
    }

    private void changeStage(int stageNum)
    {
        Debug.Log("Stage Changed:" + stageNum);
        stage = stageNum;
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
                tentacles[i].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[i].tentacleObject.transform.localRotation, tentacles[i].rotateTo, 0.3f);
            }
            if (tentacles[i].moveBack)
            {
                tentacles[i].tentacleObject.transform.position = Vector3.Lerp(tentacles[i].tentacleObject.transform.position, tentacles[i].originalPosition, 0.01f);
                tentacles[i].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[i].tentacleObject.transform.localRotation, Quaternion.Euler(0,0,0), 0.1f);
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
                tentacles[0].tentacleObject.transform.localPosition = Vector3.Lerp(tentacles[0].tentacleObject.transform.localPosition, new Vector3(-8, 3, -6f), 0.033f);
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
            tentacles[0].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[0].tentacleObject.transform.localRotation, Quaternion.Euler(0, 0, 0), 0.075f);
        }
        if (tentacles[3].moveBack)
        {
            tentacles[3].tentacleObject.transform.position = Vector3.Lerp(tentacles[3].tentacleObject.transform.position, tentacles[3].originalPosition, 0.01f);
            tentacles[3].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[3].tentacleObject.transform.localRotation, Quaternion.Euler(0, 0, 0), 0.075f);
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

    private void moveBackHandler()
    {
        if(!moveBackInProgress)
        {
            return;
        }
        tentacleFrenzyInProgress = false;
        wipeOutInProgress = false;
        returnToSenderInProgress = false;

        for(int i = 0; i < 4; i++)
        {
            tentacles[i].slam = false;
            tentacles[i].moveBack = false;
            tentacles[i].sweep = false;
            tentacles[i].wipe = false;
            tentacles[i].tentacleObject.transform.position = Vector3.Lerp(tentacles[i].tentacleObject.transform.position, tentacles[i].originalPosition, 0.01f);
            tentacles[i].tentacleObject.transform.localRotation = Quaternion.Lerp(tentacles[i].tentacleObject.transform.localRotation, Quaternion.Euler(0, 0, 0), 0.05f);
        }
        
    }

    public void runAttacks()
    {
        //Testing center
        getAttackPattern();
        StartCoroutine(attack());
    }

    public void destroyTrash()
    {
        for(int i = 0; i < trashNum; i++)
        {
            Destroy(trash[i]);
        }
        trashNum = 0;
    }
    public void canBeHitToggleOn()
    {
        StopAllCoroutines();
        moveBackInProgress = true;
        canBeHit = true;
    }

    public void canBeHitToggleOff()
    {
        moveBackInProgress = false;
        canBeHit = false;
    }

    /// <summary>
    /// Randomizes the attacks for this phase
    /// </summary>
    private void getAttackPattern()
    {
        for (int i = 0; i < 20; i++)
        {
            if (health < 50)
            {

                if (i % 3 == 0)
                {
                    attackList[i] = "tentacle frenzy";
                }
                else
                {
                    attackList[i] = possibleAttacks[Random.Range(0, 3)];
                }
            }
            else
            {
                if (i % 4 == 0)
                {
                    attackList[i] = "tentacle frenzy";
                }
                else
                {
                    attackList[i] = possibleAttacks[Random.Range(0, 3)];
                }
            }
        }
    }

    /// <summary>
    /// Running attacks based on array
    /// </summary>
    /// <returns></returns>
    IEnumerator attack()
    {
        for (int i = 0; i < attackList.Length; i++)
        {
            if (attackList[i] != null)
            {
                Debug.Log(attackList[i]);
            }
            if (attackList[i] == "tentacle frenzy")
            {
                StartCoroutine(createIndicatorsTentacleFrenzy(0.75f));
                if (stage == 1)
                {
                    tentacleFrenzyInProgress = true;
                    yield return new WaitForSeconds(9f);
                    tentacleFrenzyInProgress = false;
                }
                else if (stage == 2)
                {
                    tentacleFrenzyInProgress = true;
                    yield return new WaitForSeconds(11f);
                    tentacleFrenzyInProgress = false;
                    Debug.Log("finished");
                }
                else
                {
                    tentacleFrenzyInProgress = true;
                    yield return new WaitForSeconds(11f);
                    tentacleFrenzyInProgress = false;
                }

            }
            else if (attackList[i] == "wipe out")
            {
                StartCoroutine(wipeOutTiming());
                if (health < 50)
                {
                    wipeOutInProgress = true;
                    yield return new WaitForSeconds(28f);
                    wipeOutInProgress = false;
                }
                else
                {
                    wipeOutInProgress = true;
                    yield return new WaitForSeconds(20f);
                    wipeOutInProgress = false;
                }

            }
            else if (attackList[i] == "return to sender")
            {
                StartCoroutine(returnToSenderTiming());
                if (health < 60)
                {
                    returnToSenderInProgress = true;
                    yield return new WaitForSeconds(10f);
                    returnToSenderInProgress = false;
                }
                else
                {
                    returnToSenderInProgress = true;
                    yield return new WaitForSeconds(11f);
                    returnToSenderInProgress = false;
                }
            }
        }
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
            tentacles[i].moveBack = false;
            
            int rotationInt = Random.Range(65, 115);
            indicators[i] = Instantiate(indicator, new Vector3(Random.Range(-13, 13), 0.01f, 32.5f), Quaternion.Euler(0, rotationInt, 0));
            tentacles[i].tentacleObject.transform.localRotation = Quaternion.Euler(0, 0, 90 - rotationInt);
            tentacles[i].rotateFrom = tentacles[i].tentacleObject.transform.localRotation;
            StartCoroutine(waitForSlam(i));
            yield return new WaitForSeconds(seconds);
        }  
    }

    /// <summary>
    /// Running the slam for the indicated tentacle
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator waitForSlam(int index)
    {
        yield return new WaitForSeconds(2.5f);
        //Get initial and ending rotation for the lerp movement
        tentacles[index].rotateTo = tentacles[index].tentacleObject.transform.localRotation * Quaternion.Euler(new Vector3(90, 0, 0));

        //Destroy indicatiors and timing for slam movement
        tentacles[index].slam = true;
        yield return new WaitForSeconds(1f);
        Destroy(indicators[index]);
        indicators[index] = null;
        yield return new WaitForSeconds(1f);
        tentacles[index].slam = false;
        yield return new WaitForSeconds(1f);

        tentacles[index].moveBack = true;
    }

    /// <summary>
    /// Running the whole Wipe out attack
    /// </summary>
    /// <returns></returns>
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
                yield return new WaitForSeconds(1f);
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
                yield return new WaitForSeconds(1f);
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

    /// <summary>
    /// Running the main functionality for return to sender
    /// </summary>
    /// <returns></returns>
    IEnumerator returnToSenderTiming()
    {
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

    /// <summary>
    /// Dropping the trash for return to sender
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator dropTrash(int index)
    {
        int throws = 10;
        if (health < 60)
        {
            throws = 16;
        }
        yield return new WaitForSeconds(1f);
        if (index == throws - 1)
        {
            GameObject trashBox = Instantiate(trashList[0], new Vector3(roundIndicators[index].transform.position.x, 30, roundIndicators[index].transform.position.z), Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(2.5f);
            Destroy(roundIndicators[index]);
            yield return new WaitForSeconds(10f);
            Destroy(trashBox);
        }
        else
        {
            GameObject trashBox = Instantiate(trashList[Random.Range(1, trashList.Length)], new Vector3(roundIndicators[index].transform.position.x, 30, roundIndicators[index].transform.position.z), Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(2.5f);
            Destroy(roundIndicators[index]);
            yield return new WaitForSeconds(0.25f);
            Destroy(trashBox);
        }
        
    }

}


