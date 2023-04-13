using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleStream : MonoBehaviour
{
    GameObject[] cannons = new GameObject[5];
    GameObject[] indicators = new GameObject[5];
    GameObject[] bubbleBullet = new GameObject[5];

    [SerializeField]
    GameObject indicator;

    void Start()
    {
       for(int i = 0; i < transform.childCount; i ++)
        {
            cannons[i] = transform.GetChild(i).gameObject;
        }
        bubbleStream(100);
    }

    void FixedUpdate()
    {
        for(int i = 0; i < indicators.Length; i ++)
        {
            if (indicators[i] != null)
            {
                indicators[i].transform.localScale = Vector3.Lerp(indicators[i].transform.localScale, new Vector3(13f, 1f, 1f), 0.03f);
            }
        }
    }

    public void bubbleStream(int health)
    {
        //random values
        int[] arr = { 1, 2, 3, 4, 0 };
        for (int t = 0; t < arr.Length; t++)
        {
            int tmp = arr[t];
            int r = Random.Range(t, arr.Length);
            arr[t] = arr[r];
            arr[r] = tmp;
        }

        

        if (health >= 60)
        {
            StartCoroutine(fireCannon(arr[0]));
            StartCoroutine(fireCannon(arr[1]));
        }
        else if(health < 60 && health >= 30)
        {
            StartCoroutine(fireCannon(arr[0]));
            StartCoroutine(fireCannon(arr[1]));
            StartCoroutine(fireCannon(arr[2]));
        }
        else
        {
            StartCoroutine(fireCannon(arr[0]));
            StartCoroutine(fireCannon(arr[1]));
            StartCoroutine(fireCannon(arr[2]));
            StartCoroutine(fireCannon(arr[3]));
        }
    }

    IEnumerator fireCannon(int cannon)
    {
        indicators[cannon] = Instantiate(indicator, cannons[cannon].transform.position - new Vector3(0, 0.647f, 0), Quaternion.Euler(0, 90, 0));
        yield return new WaitForSeconds(3f);
        cannons[cannon].GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(2f);
        cannons[cannon].GetComponentInChildren<ParticleSystem>().Stop();
        Destroy(indicators[cannon]);
    }

}
