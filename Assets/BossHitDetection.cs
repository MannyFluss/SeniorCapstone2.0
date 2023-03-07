using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitDetection : MonoBehaviour
{
    // Start is called before the first frame update
    BossBehavior bb;

    private void Start()
    {
        bb = GetComponentInParent<BossBehavior>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Attack" && bb.canBeHit)
        {
            bb.health--;
            bb.health--;
        }
    }
}
