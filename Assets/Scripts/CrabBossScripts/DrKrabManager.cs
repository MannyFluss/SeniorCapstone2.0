using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrKrabManager : MonoBehaviour
{

    //move scripts
    BubbleStream bs;
    ColorSequenceManager csm;
    RippleEffectManager rem;

    //boss variables
    [Header("Boss Health")]
    public int curHealth = 0;
    public float maxHealth = 100;

    [HideInInspector]
    public bool canBeHit;

    [Header("Boss Timing")]
    [SerializeField]
    private float waitTime = 0f;
    private bool isReady = false;

    public int moveNum;

    private bool firstRun = true;

    [Header("References")]

    //Animation
    [SerializeField]
    PlayableDirector pd;

    //health
    [SerializeField]
    private Image healthBar;

    [SerializeField] 
    private AudioClip DrKrabBattleMusic;

    private void Start()
    {
        if (DrKrabBattleMusic != null)
        {
            AudioSource audio = this.GetComponent<AudioSource>();
            audio.clip = DrKrabBattleMusic;
            audio.Play();
        }

        bs = GetComponentInChildren<BubbleStream>();
        csm = GetComponentInChildren<ColorSequenceManager>();
        rem = GetComponentInChildren<RippleEffectManager>();

        curHealth = (int) maxHealth;

        canBeHit = false;

        beginMoves();
        // ttbm.TicTicBoom(curHealth);

    }

    private void Update()
    {
        Debug.Log(curHealth);
        if(!bs.bubbleStreamActive && !rem.rippleEffectActive && isReady)
        {
            StartCoroutine(preformMoves());
        }
        healthBar.rectTransform.localScale = new Vector3(curHealth / maxHealth, 1f, 1f);

        if (curHealth <= 0) SceneManager.LoadScene("The Lab");
    }

    public void beginMoves()
    {
        isReady = true;
        csm.puzzlesComplete = 0;
        if(firstRun)
        {
            firstRun = false;
            return;
        }
        csm.colorSequence(curHealth, maxHealth);
    }

    public void stunSequence()
    {
        isReady = false;
        StopAllCoroutines();
        toggleHit();
        pd.Play();
    }

    public void toggleHit()
    {
        canBeHit = !canBeHit;
    }

    IEnumerator preformMoves()
    {
        isReady = false;
        yield return new WaitForSeconds(waitTime);
        if (moveNum == 0)
        {
            bs.bubbleStream(curHealth, maxHealth);
            isReady = true;
            moveNum = 1;
        }
        else
        {
            rem.rippleEffect(curHealth, maxHealth);
            isReady = true;
            moveNum = 0;
        }
    }

}
