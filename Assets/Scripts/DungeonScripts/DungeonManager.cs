using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public GameObject[] dungeons = new GameObject[0];
    public GameObject player;
    public GameObject minionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // Expose DungeonManager to dungeons
        foreach (var dungeon in dungeons)
        {
            InitiateDungeon(dungeon: dungeon);
        }

        // Activate 1st Dungeon
        if (dungeons.Length >= 1)
        {
            dungeons[0].GetComponent<Dungeon>().SimpleSpawn();
        }
    }

    public void InitiateDungeon(GameObject dungeon)
    {
        Dungeon dungeonScript = dungeon.GetComponent<Dungeon>();
        dungeonScript.dungeonManager = gameObject.GetComponent<DungeonManager>();
        dungeonScript.minonPrefab = minionPrefab;
    }
    /// <summary>
    /// Run when player exit any dungeon
    /// Will auto trigger by Child Exit object's OnTriggerEnter function
    /// </summary>
    /// <param name="dungeon">
    /// Get dungeon gameObject
    /// </param>
    /// <param name="other">
    /// Collider of other object enter child exit object
    /// </param>
    public void OnDungeonExitTrigger(GameObject dungeon, Collider other)
    {
        Debug.Log(other.name + " triggered " + dungeon.name + " exit");

        Dungeon currentDungeon = dungeon.GetComponent<Dungeon>();
        
        if (FindNextDungeon(dungeon: dungeon) != null)
        {
            Dungeon nextDungeon = FindNextDungeon(dungeon: dungeon).GetComponent<Dungeon>();
            player.transform.position = nextDungeon.GetSpawnPosition();
            nextDungeon.SimpleSpawn();
            currentDungeon.isActive = false;
        }
    }


    /// <summary>
    /// Return next dungeon in dungeons array
    /// </summary>
    /// <param name="dungeon"></param>
    /// <returns></returns>
    public GameObject FindNextDungeon(GameObject dungeon)
    {
        int dungeonIndex = System.Array.IndexOf(dungeons, dungeon);
        if (dungeonIndex < (dungeons.Length - 1))
        {
            return dungeons[dungeonIndex + 1];
        }
        return null;
    }
}
