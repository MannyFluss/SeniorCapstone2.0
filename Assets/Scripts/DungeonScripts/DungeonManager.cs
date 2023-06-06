using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class DungeonManager : MonoBehaviour
{


    //added a FadeAudioSource Function
    private static class FadeAudioSource
    {
      

        public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume, AudioSource nonbattle, float duration2, float targetVolume2)
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);    

                yield return null;
            }
            audioSource.Stop();


            yield return new WaitForSeconds(0.10f);


            nonbattle.volume = 0.0f;
            nonbattle.Play();
            currentTime = 0;
            
            start = nonbattle.volume;
            while (currentTime < duration2)
            {
                currentTime += Time.deltaTime;
                nonbattle.volume = Mathf.Lerp(start, targetVolume2, currentTime / duration2);

                yield return null;
            }
            //nonbattle.Play();
            yield break;
        }

        
    }


    //added battlemusictheme
    [SerializeField] private AudioSource BattleMusic;
    //added nonbattlemusictheme
    [SerializeField] private AudioSource NonBattleMusic;

    [SerializeField] private string NextScene;

    public bool levelT = false;
    public GameObject[] dungeons = new GameObject[0];
    public GameObject player;
    public List<GameObject> minionPrefabs = new List<GameObject>();
    public List<GameObject> levelOneMinionPrefabs = new List<GameObject>();
    public List<GameObject> levelTwoMinionPrefabs = new List<GameObject>();
    public List<GameObject> levelThreeMinionPrefabs = new List<GameObject>();

    [Header("Cleared Message Assets")]
    public Image ClearedCanvas;
    public float AppearTimer;
    public bool complete = false;
    public bool msgDelivered = false;

    // Start is called before the first frame update
    void Start()
    {
        ClearedCanvas.color = new Color(1, 1, 1, 0);
        ClearedCanvas.enabled = false;
        // Expose DungeonManager to
        foreach (var minionPrefab in minionPrefabs)
        {
            print("aaaaa");
            var level = minionPrefab.GetComponent<MinionComponent>().level;
            if (level == 1) {
                levelOneMinionPrefabs.Add(minionPrefab);
                if (levelT)
                {
                    levelTwoMinionPrefabs.Add(minionPrefab);
                }
            } else if (level == 2)
            {
                levelTwoMinionPrefabs.Add(minionPrefab);
            } else if (level == 3)
            {
                levelThreeMinionPrefabs.Add(minionPrefab);
            }
            print("llll");
            print(levelOneMinionPrefabs.Count);
        }

        var i = 0;
        foreach (var dungeon in dungeons)
        {

            if (i < 1)
            {
                InitiateDungeon(dungeon: dungeon, 1);
            }
            else if (i < 3)
            {
                InitiateDungeon(dungeon: dungeon, 2);
            }
            else
            {
                InitiateDungeon(dungeon: dungeon, 3);
            }

            i++;
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

    public void InitiateDungeon(GameObject dungeon, int difficulty)
    {
        Dungeon dungeonScript = dungeon.GetComponent<Dungeon>();
        dungeonScript.dungeonManager = gameObject.GetComponent<DungeonManager>();
        var newPrefabs = new List<GameObject>();
        if (difficulty >= 1)
        {
            newPrefabs.AddRange(levelOneMinionPrefabs);
        }
        if (difficulty >= 2)
        {
            newPrefabs.AddRange(levelTwoMinionPrefabs);
        }
        if (difficulty >= 3)
        {
            newPrefabs.AddRange(levelThreeMinionPrefabs);
        }
        dungeonScript.minonPrefabs = newPrefabs;

        //Enter Dungeon Battle music
        
        //NonBattleMusic.Play();
        BattleMusic.Play();

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

                //resume battle music again
                StartCoroutine(FadeAudioSource.StartFade(NonBattleMusic, 0.5f, 0.0f, BattleMusic, 2f, 0.35f));
                BattleMusic.volume = 0.35f;
                //NonBattleMusic.volume = 0.25f;
                //BattleMusic.Play();
            }
            else
            {
                SceneManager.LoadScene(NextScene);
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

            //fade battle music to stop
            StartCoroutine(FadeAudioSource.StartFade(BattleMusic, 1.3f, 0f, NonBattleMusic, 2f, 0.25f));
            //NonBattleMusic.volume = 0.0f;
            //StartCoroutine(FadeAudioSource.StartFade(NonBattleMusic, 1.3f, 0.25f));
            //BattleMusic.Stop();
            //NonBattleMusic.Play();

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
        ClearedCanvas.enabled = true;
        for (float alpha = 0; alpha <= 1f; alpha += 1.2f * Time.deltaTime)
        {
            ClearedCanvas.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(1.2f);
        for (float alpha = 1f; alpha >= 0f; alpha -= 1.2f * Time.deltaTime)
        {
            ClearedCanvas.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        ClearedCanvas.enabled = false;
    }
}
