using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    public GameObject[] dungeons = new GameObject[0];
    public GameObject player;
    public List<GameObject> minionPrefabs = new List<GameObject>();

    [Header("Cleared Message Assets")]
    public Canvas ClearedCanvas;
    public float AppearTimer;
    public bool complete = false;
    public bool msgDelivered = false;

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
            dungeons[0].GetComponent<Dungeon>().RandomSpawn();
            dungeons[0].GetComponent<Dungeon>().HideEntrance();
        }
    }

    private void Update()
    {
        if (player.transform.position.y <= -10f) {
            ResetDungeon();
        }
    }
    public void ResetDungeon()
    {
        foreach (var dungeon in dungeons) {
            Dungeon dungeonScript = dungeon.GetComponent<Dungeon>();
            dungeonScript.isActive = false;
            dungeonScript.ClearMinions();
        }
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = dungeons[0].GetComponent<Dungeon>().GetSpawnPosition();
        player.GetComponent<CharacterController>().enabled = true;
        dungeons[0].GetComponent<Dungeon>().RandomSpawn();
    }

    public void InitiateDungeon(GameObject dungeon)
    {
        Dungeon dungeonScript = dungeon.GetComponent<Dungeon>();
        dungeonScript.dungeonManager = gameObject.GetComponent<DungeonManager>();
        dungeonScript.minonPrefabs = minionPrefabs;
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
        Debug.Log(currentDungeon.RemainMinions());
        if (currentDungeon.RemainMinions() == 0)
        {

            if (FindNextDungeon(dungeon: dungeon) != null)
            {
                Dungeon nextDungeon = FindNextDungeon(dungeon: dungeon).GetComponent<Dungeon>();
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = nextDungeon.GetSpawnPosition();
                nextDungeon.HideEntrance();
                player.GetComponent<CharacterController>().enabled = true;
                complete = false;
                msgDelivered = false;
                nextDungeon.RandomSpawn();
                currentDungeon.isActive = false;
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //ResetDungeon();
            }
        }
    }

    public void DungeonComplete()
    {
        if (!msgDelivered)
        {
            StartCoroutine(ClearBannerAppear(AppearTimer));
            msgDelivered = true;
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

    IEnumerator ClearBannerAppear(float waitTime)
    {
        ClearedCanvas.GetComponent<Canvas>().enabled = true;
        yield return new WaitForSeconds(waitTime);
        ClearedCanvas.GetComponent<Canvas>().enabled = false;
    }
}
