using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpnDown : MonoBehaviour
{ 
    public GameObject movingTarget;
    public float range;

    private Vector3 resetPos;

    // Start is called before the first frame update
    void Awake()
    {
        resetPos = movingTarget.transform.position;
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(UpNDown());
    }

    IEnumerator UpNDown()
    {
        movingTarget.transform.position = Vector3.Lerp(resetPos, new Vector3(resetPos.x, resetPos.y + range, resetPos.z), Time.deltaTime);
        yield return new WaitForSeconds(2f);
        movingTarget.transform.position = Vector3.Lerp(movingTarget.transform.position, resetPos, Time.deltaTime);
    }
}
