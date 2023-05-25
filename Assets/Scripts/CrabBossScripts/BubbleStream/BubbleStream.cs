using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleStream : MonoBehaviour
{
    BubbleStreamCannon[] cannons = new BubbleStreamCannon[9];
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
        }
    }

    

    public void bubbleStream(int health)
    {
        bubbleStreamActive = true;
        StartCoroutine(runStream(health));
    }

    IEnumerator runStream(int health)
    {
        var num = 0;
        var interval = 1.5;
        if(health >= 40)
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



            if (health >= 75)
            {
                cannons[arr[0]].fireSingleCannon();
                cannons[arr[1]].fireSingleCannon();
                cannons[arr[2]].fireSingleCannon();
                cannons[arr[3]].fireSingleCannon();
                cannons[arr[4]].fireSingleCannon();

            }
            else if (health < 75 && health >= 39)
            {
                cannons[arr[0]].fireSingleCannon();
                cannons[arr[1]].fireSingleCannon();
                cannons[arr[2]].fireSingleCannon();
                cannons[arr[3]].fireSingleCannon();
                cannons[arr[4]].fireSingleCannon();
                cannons[arr[5]].fireSingleCannon();
            }
            else
            {
                cannons[arr[0]].fireSingleCannon();
                cannons[arr[1]].fireSingleCannon();
                cannons[arr[2]].fireSingleCannon();
                cannons[arr[3]].fireSingleCannon();
                cannons[arr[4]].fireSingleCannon();
                cannons[arr[5]].fireSingleCannon();
            }
            yield return new WaitForSeconds(4.5f);
        }
        bubbleStreamActive = false;
    }
}
