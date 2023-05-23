using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private bool x = false;
    [SerializeField] private bool y = false;
    [SerializeField] private bool z = false;
    [SerializeField] private float degree = 20;
    [SerializeField] private bool counterClockWise;
    [SerializeField] private float rotateSpeed = 10;

    private Vector3 xAxis = new Vector3(1, 0, 0);
    private Vector3 yAxis = new Vector3(0, 1, 0);
    private Vector3 zAxis = new Vector3(0, 0, 1);
    private float direction = -1;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = this.gameObject.transform;
        if (counterClockWise) { direction = 1; }
    }

    // Update is called once per frame
    void Update()
    {
        if (x) this.transform.RotateAround(target.position, xAxis, (direction * degree) * (rotateSpeed * Time.deltaTime));
        if (y) this.transform.RotateAround(target.position, yAxis, (direction * degree) * (rotateSpeed * Time.deltaTime));
        if (z) this.transform.RotateAround(target.position, zAxis, (direction * degree) * (rotateSpeed * Time.deltaTime));
    }
}
