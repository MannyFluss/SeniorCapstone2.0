using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Exit : MonoBehaviour
{
    public Animator am;
    public SpriteRenderer sr;
    // trigger parent's OnExitTrigger function
    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInParent<Dungeon>().OnExitTrigger(other);
        
    }

    void Start()
    {
        gameObject.GetComponentInParent<Dungeon>().exit = gameObject;
        //gameObject.transform.localScale = new Vector3(0, 0, 0);
        sr.enabled = false;
    }
}
