using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInParent<Dungeon>().entrance = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
