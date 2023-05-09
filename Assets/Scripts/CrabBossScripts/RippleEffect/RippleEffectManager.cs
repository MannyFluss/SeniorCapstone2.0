using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleEffectManager : MonoBehaviour
{
    private float interval = 0.0f;

    RippleEffectSingle[] ripples = new RippleEffectSingle[4];

    //for DrKrabManager
    public bool rippleEffectActive = false;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ripples[i] = transform.GetChild(i).GetComponent<RippleEffectSingle>();
        }
    }

    public void rippleEffect(int health)
    {
        rippleEffectActive = true;
        StartCoroutine(rippleEffectAttack(health));
    }

    IEnumerator rippleEffectAttack(int health)
    {
        // float interval = 0;
        var num = 0;

        //determine interval and number of waves
        if (health > 50)
        {
            num = Random.Range(8, 10);
            interval = 1.1f;
        }
        else
        {
            num = Random.Range(10, 12);
            interval = 0.85f;
        }
        for(int i = 0; i < num; i++)
        {
            ripples[Random.Range(0, 2)].fireRipple();
            yield return new WaitForSeconds(interval);
        }

        rippleEffectActive = false;
    }
}
