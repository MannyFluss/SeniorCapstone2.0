using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Dungeon dungeon = gameObject.GetComponentInParent<Dungeon>();
        if (!(dungeon.minionSpawns.Contains(gameObject)))
        {
            dungeon.minionSpawns.Add(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
