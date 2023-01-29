using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInParent<Dungeon>().minionSpawns.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
