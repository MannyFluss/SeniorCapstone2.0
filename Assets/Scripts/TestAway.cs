using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TestAway : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = pointB.transform.position - pointA.transform.position;
        transform.position = direction.normalized * 10f;
    }
}
