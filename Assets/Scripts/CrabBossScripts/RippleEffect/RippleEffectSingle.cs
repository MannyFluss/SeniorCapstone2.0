using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleEffectSingle : MonoBehaviour
{
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
    }

    public void fireRipple()
    {
        StartCoroutine(rippleEffect());
    }

    public IEnumerator rippleEffect()
    {
        ps.Play();
        yield return new WaitForSeconds(0.5f);
        ps.Stop();
    }
}
