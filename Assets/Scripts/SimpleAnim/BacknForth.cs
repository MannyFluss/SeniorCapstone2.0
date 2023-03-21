using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacknForth : MonoBehaviour
{
    public float speed = 0f;
    public float range = 0f;
    public bool newX, newY, newZ;

    private Vector3 pos;

    private void Start()
    {
       pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 pos = this.transform.position;
        float newVar = Mathf.Sin(Time.time * speed) * range;

        if (newX) transform.position = new Vector3(pos.x + newVar, pos.y, pos.z);
        if (newY) transform.position = new Vector3(pos.x, pos.y + newVar, pos.z);
        if (newZ) transform.position = new Vector3(pos.x, pos.y, pos.z - newVar);


    }
}
