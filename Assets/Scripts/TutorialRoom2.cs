using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoom2 : MonoBehaviour
{
    GameObject player;

    SingleArmBehavior armScript;
    DungeonManager dm;
    private bool clearR2;
    // Start is called before the first frame update
    void Start()
    {
        armScript = GetComponent<SingleArmBehavior>();
        dm = GetComponentInParent<DungeonManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        clearR2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        if (playerPos.x >= 65 && playerPos.x <= 95 && !clearR2) {
            if (gameObject.GetComponentInParent<Dungeon>().RemainMinions() == 0) {
                clearR2 = true;
                gameObject.SetActive(false);
                return;
            }
            foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                var distanceToEnemy = Vector3.Distance(gameObject.transform.position,enemies.transform.position);
                if (distanceToEnemy < 40)
                {
                    armScript.Frenzy = true;
                    return;
                } 
            }
        } else {
            armScript.Frenzy = false;
        }
    }
}
