using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinnionSpawning : MonoBehaviour
{
    Vector3[] spawnLocations = new Vector3[4];

    [SerializeField]
    GameObject spawnPad;
    
    Dungeon dm;

    bool spawn;

    bool started = false;

    bool firstSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        dm = GetComponent<Dungeon>();
        for(int i = 0; i < spawnLocations.Length; i++)
        {
            spawnLocations[i] = transform.GetChild(i).position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (firstSpawn)
        {
            dm.ClearMinions();
            firstSpawn = false;
        }
        if (!started)
        {
            return;
        }
        if(!spawn && dm.RemainMinions() < 8)
        {
            StartCoroutine(spawnEnemy());
        }
    }

    public void start()
    {
        started = true;
    }

    IEnumerator spawnEnemy()
    {
        spawn = true;
        dm.SimpleSpawn();
        yield return new WaitForSeconds(6f);
        spawn = false;
    }
}
