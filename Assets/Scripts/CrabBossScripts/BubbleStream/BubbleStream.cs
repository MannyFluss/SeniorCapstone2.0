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

    

    public void bubbleStream(int health, float maxHealth)
    {
        bubbleStreamActive = true;
        StartCoroutine(runStream(health, maxHealth));
    }

    IEnumerator runStream(int health, float maxHealth)
    {
        var num = 0;
        var interval = 1.5f;
        if(health/maxHealth >= .40f)
        {
            num = 3;
        }
        else
        {
            num = 4;
        }
        for(int i = 0; i < num; i++)
        {
            //random values
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
            for (int t = 0; t < arr.Length; t++)
            {
                int tmp = arr[t];   
                int r = Random.Range(t, arr.Length);
                arr[t] = arr[r];
                arr[r] = tmp;
            }



            if (health/maxHealth >= 0.75f)
            {
                foreach(ParticleSystem p in bubbles)
                {
                    var main = p.main;
                    main.startSpeed = 17;
                }
                cannons[arr[0]].fireSingleCannon();
                cannons[arr[1]].fireSingleCannon();
                cannons[arr[2]].fireSingleCannon();
                cannons[arr[3]].fireSingleCannon();
                cannons[arr[4]].fireSingleCannon();

            }
            else if (health/maxHealth < 0.75f && health/maxHealth >= 0.39f)
            {
                foreach (ParticleSystem p in bubbles)
                {
                    var main = p.main;
                    main.startSpeed = 20;
                }
                interval = 1.2f;
                cannons[arr[0]].fireSingleCannon();
                cannons[arr[1]].fireSingleCannon();
                cannons[arr[2]].fireSingleCannon();
                cannons[arr[3]].fireSingleCannon();
                cannons[arr[4]].fireSingleCannon();
                cannons[arr[5]].fireSingleCannon();
            }
            else
            {
                foreach (ParticleSystem p in bubbles)
                {
                    var main = p.main;
                    main.startSpeed = 25;
                }
                interval = 1f;
                cannons[arr[0]].fireSingleCannon();
                cannons[arr[1]].fireSingleCannon();
                cannons[arr[2]].fireSingleCannon();
                cannons[arr[3]].fireSingleCannon();
                cannons[arr[4]].fireSingleCannon();
                cannons[arr[5]].fireSingleCannon();
            }
            yield return new WaitForSeconds(interval);
        }
        bubbleStreamActive = false;
    }
}
