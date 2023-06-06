using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrKrabManager : MonoBehaviour
{
    [SerializeField]
    PlayerManager player;

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
    [SerializeField]
    Animator anim;

    //health
    [SerializeField]
    private Image healthBar;

    [SerializeField] 
    private AudioSource DrKrabBattleMusic;

    private void Start()
    {
        DrKrabBattleMusic.Play();


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
        StopAllCoroutines();
        isReady = false;
        anim.SetBool("bubbleStream", false);
        anim.SetBool("rippleEffect", false);
        bs.ClearCannons();
        rem.ClearRipples();
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
            anim.SetBool("bubbleStream", true);
            anim.SetBool("rippleEffect", false);
            bs.bubbleStream(curHealth, maxHealth);
            isReady = true;
            moveNum = 1;
        }
        else
        {
            anim.SetBool("rippleEffect", true);
            anim.SetBool("bubbleStream", false);
            rem.rippleEffect(curHealth, maxHealth);
            isReady = true;
            moveNum = 0;
        }
    }

}
