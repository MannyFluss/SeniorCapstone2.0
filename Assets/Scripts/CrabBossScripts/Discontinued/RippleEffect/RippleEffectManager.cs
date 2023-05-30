using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleEffectManager : MonoBehaviour
{
    private float interval = 0.0f;

    RippleEffectSingle[] ripples = new RippleEffectSingle[4];

    [SerializeField]
    private float nineWaveInterval = 8f;
    private float elevenWaveInterval = 6f;

    //for DrKrabManager
    public bool rippleEffectActive = false;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            ripples[i] = transform.GetChild(i).GetComponent<RippleEffectSingle>();
        }
    }

    public void rippleEffect(int health, float maxHealth)
    {
        rippleEffectActive = true;
        StartCoroutine(rippleEffectAttack(health, maxHealth));
    }

    IEnumerator rippleEffectAttack(int health, float maxHealth)
    {
        // float interval = 0;
        var num = 0;

        //determine interval and number of waves
        if (health/maxHealth > .50f)
        {
            num = 9;
            interval = nineWaveInterval;
        }
        else
        {
            num = 12;
            interval = elevenWaveInterval;
        }
        for(int i = 0; i < num; i++)
        {
            ripples[Random.Range(0, 2)].fireRipple();
            yield return new WaitForSeconds(interval);
        }

        rippleEffectActive = false;
    }
}
