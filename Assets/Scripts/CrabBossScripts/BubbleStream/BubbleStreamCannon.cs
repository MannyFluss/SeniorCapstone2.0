using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleStreamCannon : MonoBehaviour
{

    GameObject indicator;

    [SerializeField]
    GameObject indicatorPrefab;

    [SerializeField]
    private bool isSingleCannon;

    

    void FixedUpdate()
    {
        if (indicator != null)
        {
            indicator.transform.localScale = Vector3.Lerp(indicator.transform.localScale, new Vector3(13f, 1f, 1f), 0.03f);
        }
    }

    public void fireSingleCannon()
    {
        
        StartCoroutine(fireCannon());
    }

    public IEnumerator fireCannon()
    {
        indicator = Instantiate(indicatorPrefab, transform.position - new Vector3(0, 0.647f, 0), transform.localRotation * Quaternion.Euler(new Vector3(0, 90, 0)));
        yield return new WaitForSeconds(3f);
        GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(2f);
        GetComponentInChildren<ParticleSystem>().Stop();
        Destroy(indicator);
    }
}
