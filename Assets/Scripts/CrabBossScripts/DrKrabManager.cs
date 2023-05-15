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
    TicTicBoomManager ttbm;
    RippleEffectManager rem;

    //boss variables
    public int curHealth = 0;
    public float maxHealth = 100;
    private int puzzleTime;
    private int moveCtr;

    public bool canBeHit;

    [SerializeField]
    private float waitTime = 0f;
    private bool isReady = false;

    public int moveNum;

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
        ttbm = GetComponentInChildren<TicTicBoomManager>();
        rem = GetComponentInChildren<RippleEffectManager>();

        curHealth = (int) maxHealth;

        canBeHit = false;

        //beginMoves();
        ttbm.TicTicBoom(health);

    }

    private void Update()
    {
        if(!bs.bubbleStreamActive && !rem.rippleEffectActive && isReady)
        {
            
            StartCoroutine(preformMoves());
        }
        healthBar.rectTransform.localScale = new Vector3(health / 100f, 1f, 1f);

        if (health <= 0) SceneManager.LoadScene("TheLab");
    }

    public void beginMoves()
    {
        moveCtr = 0;
        if(curHealth < 60)
        {
            puzzleTime = 3;
        }
        else
        {
            puzzleTime = 2;
        }
        moveNum = Random.Range(0, 2);
        isReady = true;
        
    }

    public void stunSequence()
    {
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
        if(moveCtr != puzzleTime)
        {
            if(moveNum == 0)
            {
                bs.bubbleStream(curHealth);
                isReady = true;
                moveNum = 1;
            }
            else
            {
                rem.rippleEffect(curHealth);
                isReady = true;
                moveNum = 0;
            }
        }
        else
        {
            ttbm.TicTicBoom(curHealth);
        }
    }

}
