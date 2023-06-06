using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleStream : MonoBehaviour
{
    BubbleStreamCannon[] cannons = new BubbleStreamCannon[9];
    ParticleSystem[] bubbles = new ParticleSystem[9];
    //GameObject[] bubbleBullet = new GameObject[5];

    [SerializeField]
    public GameObject indicator;

    //For DrKrabManager
    public bool bubbleStreamActive = false;

    void Start()
    {
       for(int i = 0; i < transform.childCount; i ++)
        {
            cannons[i] = transform.GetChild(i).GetComponent<BubbleStreamCannon>();
            bubbles[i] = transform.GetChild(i).GetComponentInChildren<ParticleSystem>();
        }
    }

    public void ClearCannons()
    {
        StopAllCoroutines();
        foreach (ParticleSystem p in bubbles)
        {
            p.Stop();
            p.Clear();
        }
    }

    public void bubbleStream(int health, float maxHealth)
    {
        bubbleStreamActive = true;
        StartCoroutine(runStream(health, maxHealth));
    }

    IEnumerator runStream(int health, float maxHealth)
    {
        var num = 0;
        var interval = 1.5f;
        var oddEven = false;
        if (health/maxHealth >= .40f)
        {
            num = 3;
        }
        else
        {
            num = 4;
        }
        for(int i = 0; i < num; i++)
        {
            if (oddEven)
            {
                cannons[0].fireSingleCannon();
                cannons[2].fireSingleCannon();
                cannons[4].fireSingleCannon();
                cannons[6].fireSingleCannon();
                cannons[8].fireSingleCannon();
                oddEven = !oddEven;
            }
            else
            {
                cannons[1].fireSingleCannon();
                cannons[3].fireSingleCannon();
                cannons[5].fireSingleCannon();
                cannons[7].fireSingleCannon();
                //cannons[9].fireSingleCannon();
                oddEven = !oddEven;
            }

            if (health/maxHealth >= 0.75f)
            {
                foreach(ParticleSystem p in bubbles)
                {
                    var main = p.main;
                    main.startSpeed = 17;
                }

            }
            else if (health/maxHealth < 0.75f && health/maxHealth >= 0.39f)
            {
                foreach (ParticleSystem p in bubbles)
                {
                    var main = p.main;
                    main.startSpeed = 20;
                }
                interval = 1.2f;
            }
            else
            {
                foreach (ParticleSystem p in bubbles)
                {
                    var main = p.main;
                    main.startSpeed = 25;
                }
                interval = 1f;
            }
            yield return new WaitForSeconds(interval);
        }
        bubbleStreamActive = false;
    }
}
