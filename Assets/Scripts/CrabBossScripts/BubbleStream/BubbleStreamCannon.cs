using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleStreamCannon : MonoBehaviour
{

    [SerializeField]
    private bool isSingleCannon;


    public void fireSingleCannon()
    {
        
        StartCoroutine(fireCannon());
    }

    public IEnumerator fireCannon()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
