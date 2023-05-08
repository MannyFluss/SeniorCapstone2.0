using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrKrabHitDetect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private DrKrabManager dkm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack" && dkm.canBeHit)
        {
            dkm.health -= 1;
        }
    }
}
