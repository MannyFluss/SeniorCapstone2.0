using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoom4 : MonoBehaviour
{
    GameObject player;

    SingleArmBehavior armScript;
    DungeonManager dm;
    private bool clearR4;
    // Start is called before the first frame update
    void Start()
    {
        armScript = GetComponent<SingleArmBehavior>();
        dm = GetComponentInParent<DungeonManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        clearR4 = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;

        if (playerPos.x >= 225 && playerPos.x <= 255 && !clearR4) {
            if (gameObject.GetComponentInParent<Dungeon>().RemainMinions() == 0) {
                clearR4 = true;
                gameObject.SetActive(false);
                return;
            }
            foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                var distanceToEnemy = Vector3.Distance(gameObject.transform.position,enemies.transform.position);
                if (distanceToEnemy < 40)
                {
                    armScript.WipeOut = true;
                    return;
                } 
            }
        } else {
            armScript.WipeOut = false;
        }
    }
}
