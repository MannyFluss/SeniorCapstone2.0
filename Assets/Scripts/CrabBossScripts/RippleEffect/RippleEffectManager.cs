using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleEffectManager : MonoBehaviour
{
    private float interval = 0.0f;

    RippleEffectSingle[] ripples = new RippleEffectSingle[4];
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ripples[i] = transform.GetChild(i).GetComponent<RippleEffectSingle>();
        }
    }

    public void rippleEffect(int health)
    {
        StartCoroutine(rippleEffectAttack(health));
    }

    IEnumerator rippleEffectAttack(int health)
    {
        // float interval = 0;
        var num = 0;

        //determine interval and number of waves
        if (health > 50)
        {
            num = Random.Range(7, 11);
            interval = 1.5f;
        }
        else
        {
            num = Random.Range(8, 12);
            interval = 1.0f;
        }
        for(int i = 0; i < num; i++)
        {
            ripples[Random.Range(0, 4)].fireRipple();
            yield return new WaitForSeconds(interval);
        }
    }
}
