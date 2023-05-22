using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatWall : MonoBehaviour
{
    GameObject playerRef;
    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject,5f);
    }
    public void setPlayer(GameObject player)
    {
        playerRef = player;
    }
    void Update()
    {
        gameObject.transform.position = playerRef.transform.position;   
    }
}
