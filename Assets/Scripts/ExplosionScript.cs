using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float explosionTime = 1.0f;
    float xScale,yScale = 1.0f;
    void Start()
    {
        Destroy(this,explosionTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
