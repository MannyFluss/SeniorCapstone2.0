using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabLayer : MonoBehaviour
{
    public int zaxis;
    GameObject player;
    // Update is called once per frame

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (player.transform.position.z <= zaxis) {
            renderer.sortingOrder = 0;
        } else {
            renderer.sortingOrder = 2;
        }
    }
}
