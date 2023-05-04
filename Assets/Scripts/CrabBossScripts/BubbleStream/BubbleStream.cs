using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleStream : MonoBehaviour
{
    BubbleStreamCannon[] cannons = new BubbleStreamCannon[6];
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
        for(int i = 0; i < 3; i++)
        {
            //random values
            int[] arr = { 1, 2, 3, 4, 5, 0 };
            for (int t = 0; t < arr.Length; t++)
            {
                int tmp = arr[t];
                int r = Random.Range(t, arr.Length);
                arr[t] = arr[r];
                arr[r] = tmp;
            }



            if (health >= 60)
            {
                cannons[arr[0]].fireSingleCannon();
                cannons[arr[1]].fireSingleCannon();
            }
            else if (health < 60 && health >= 30)
            {
                cannons[arr[0]].fireSingleCannon();
                cannons[arr[1]].fireSingleCannon();
                cannons[arr[2]].fireSingleCannon();
            }
            else
            {
                cannons[arr[0]].fireSingleCannon();
                cannons[arr[1]].fireSingleCannon();
                cannons[arr[2]].fireSingleCannon();
                cannons[arr[3]].fireSingleCannon();
            }
            yield return new WaitForSeconds(6f);
        }
        bubbleStreamActive = false;
    }
}
