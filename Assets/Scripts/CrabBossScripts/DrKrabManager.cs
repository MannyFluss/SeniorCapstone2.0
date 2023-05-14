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
    public int health = 100;
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


    private void Start()
    {
        bs = GetComponentInChildren<BubbleStream>();
        ttbm = GetComponentInChildren<TicTicBoomManager>();
        rem = GetComponentInChildren<RippleEffectManager>();

        health = 100;

        canBeHit = false;

        beginMoves();
    }

    private void Update()
    {
        if(!bs.bubbleStreamActive && !rem.rippleEffectActive && isReady)
        {
            StartCoroutine(preformMoves());
        }
        healthBar.rectTransform.localScale = new Vector3(health / 100f, 1f, 1f);
    }

    public void beginMoves()
    {
        moveCtr = 0;
        if(health < 60)
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
        if(moveCtr != puzzleTime)
        {
            var num = Random.Range(0, 2);
            if(num == 0)
            {
                bs.bubbleStream(health);
                isReady = true;
            }
            else
            {
                rem.rippleEffect(health);
                isReady = true;
            }
            moveCtr++;
        }
        else
        {
            ttbm.TicTicBoom(health);
        }
    }

}