using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            bb.health -= 1.0f;
        }
    }
}
