using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> minonPrefabs = new List<GameObject>();
    [HideInInspector]
    public GameObject entrance;
    [HideInInspector]
    public List<GameObject> spawnedMinions = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> minionSpawns = new List<GameObject>();
    [HideInInspector]
    public bool isActive = false;
    [HideInInspector]
    public DungeonManager dungeonManager;

    private void Update()
    {
        if (!isActive) {
            ClearMinions();
        }
    }


    /// <summary>
    /// Simple spawn is a test function spawn one enemy at each minion spawn location
    /// </summary>
    public void SimpleSpawn()
    {
        isActive = true;
        Debug.Log(minionSpawns.Count);
        foreach (var mimionSpawn in minionSpawns)
        {
            Vector3 spawnPosition = mimionSpawn.transform.position;
            GameObject spawnedMinion = Instantiate(minonPrefabs[0], new Vector3(spawnPosition.x, spawnPosition.y + 0.5f, spawnPosition.z), Quaternion.identity);
            spawnedMinions.Add(spawnedMinion);
        }
    }

    public void RandomSpawn()
    {
        isActive = true;
        foreach (var minionSpawn in minionSpawns)
        {
            Vector3 spawnPosition = minionSpawn.transform.position;

            GameObject spawnedMinion = Instantiate(minonPrefabs[Random.Range(0, minonPrefabs.Count)],
                new Vector3(spawnPosition.x, spawnPosition.y + 0.5f, spawnPosition.z),
                Quaternion.identity);
            spawnedMinions.Add(spawnedMinion);
        }
    }

    public int RemainMinions()
    {
        int remaindMinions = 0;
        foreach (var minion in spawnedMinions)
        {
            try
            {
                if (minion.activeSelf)
                {
                    remaindMinions++;
                }
            } catch
            {

            }
            
        }
        return remaindMinions;
    }

    /// <summary>
    /// Clear all the minions from dungeon
    /// </summary>
    public void ClearMinions()
    {
        foreach (var minion in spawnedMinions)
        {
            Destroy(minion);
        }
    }

    /// <summary>
    /// Return dungeon spawn coordinate 
    /// </summary>
    /// <returns>
    /// Return dungeon spawn transform
    /// </returns>
    public Vector3 GetSpawnPosition()
    {
        return entrance.transform.position;
    }

    /// <summary>
    /// Run when player exit current dungeon
    /// Will auto trigger by Child Exit object's OnTriggerEnter function
    /// </summary>
    /// <param name="other">
    /// Collider of other object enter child exit object
    /// </param>
    public void OnExitTrigger(Collider other)
    {
        if (other.tag == "Player")
        {
            dungeonManager.OnDungeonExitTrigger(dungeon: gameObject, other: other);
        }
    }
}
