using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

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

    private bool isReady = false;

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

        beginMoves();
    }

    private void Update()
    {
        if(!bs.bubbleStreamActive && !rem.rippleEffectActive && isReady)
        {
            StartCoroutine(preformMoves());
        }
        healthBar.rectTransform.localScale = new Vector3(curHealth / maxHealth, 1f, 1f);
    }

    public void beginMoves()
    {
        moveCtr = 0;
        if(curHealth < 60)
        {
            puzzleTime = 4;
        }
        else
        {
            puzzleTime = 3;
        }
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
        yield return new WaitForSeconds(2.3f);
        moveCtr++;
        if (moveCtr != puzzleTime)
        {
            var num = Random.Range(0, 2);
            if(num == 0)
            {
                bs.bubbleStream(curHealth);
                isReady = true;
            }
            else
            {
                rem.rippleEffect(curHealth);
                isReady = true;
            }
        }
        else
        {
            ttbm.TicTicBoom(curHealth);
        }
    }

}
