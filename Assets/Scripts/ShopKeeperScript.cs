using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeperScript : MonoBehaviour
{

    float interactRange = 5.0f;
    GameObject player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        var di = Vector3.Distance(transform.position,player.transform.position);
        if (di <= interactRange)
        {
            //enable shopping functionality
        }
    }
}
