using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // trigger parent's OnExitTrigger function
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInParent<Dungeon>().OnExitTrigger(other);
    }
}
