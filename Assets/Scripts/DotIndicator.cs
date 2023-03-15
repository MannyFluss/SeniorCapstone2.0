using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float timeToLive = 1.0f;
    void Start()
    {
        Destroy(gameObject,timeToLive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
