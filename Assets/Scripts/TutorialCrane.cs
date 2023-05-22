using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialCrane : MonoBehaviour
{
    [SerializeField]
    GameObject shopKeeper;
    
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //checks if player attacked crane and the shopkeeper is gone before activating
        if(other.gameObject.tag == "Attack" && !shopKeeper.activeSelf)
        {
            anim.SetBool("hit", true);
        }
    }
}
