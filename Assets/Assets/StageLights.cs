using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLights : MonoBehaviour
{
    //lights
    private Transform[] lights;

    //targets
    private GameObject[] targets;
    private Vector3[] positions;

    //Spotlight target
    [SerializeField]
    private GameObject target;

    //timer
    private float timer;


    void Start()
    {
        lights = transform.GetComponentsInChildren<Transform>();
        targets = new GameObject[lights.Length];
        positions = new Vector3[lights.Length];
        for (int i = 0; i < lights.Length; i++)
        {
            targets[i] = Instantiate(target, new Vector3(Random.Range(-13, 18), 0, Random.Range(-12, 14)), Quaternion.Euler(new Vector3(0,0,0)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        for(int i = 1; i < lights.Length; i++)
        {
            lights[i].LookAt(targets[i].transform);
        }
        if (timer <= 0)
        {
            timer = 2;
            for(int i = 1; i < positions.Length; i++)
            {
                positions[i] = new Vector3(Random.Range(-13, 18), 0, Random.Range(-12, 14));
            }
        }
        for(int i = 1; i < targets.Length; i++)
        {
            targets[i].transform.position = Vector3.Lerp(targets[i].transform.position, positions[i], Time.deltaTime / 2);
        }
    }
}
