using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoom2 : MonoBehaviour
{
    GameObject player;

    SingleArmBehavior armScript;
    DungeonManager dm;
    private bool clearR2;
    // private Vector3 originalPosition;
    // private Vector3 aboveGround;
    // private Vector3 belowGround;
    // Start is called before the first frame update
    void Start()
    {
        armScript = GetComponent<SingleArmBehavior>();
        dm = GetComponentInParent<DungeonManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        clearR2 = false;
        //originalPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position;
        //checks if player is in room 2 
        if (playerPos.x >= 65 && playerPos.x <= 95 && !clearR2) {
            //checks if room is cleared
            if (gameObject.GetComponentInParent<Dungeon>().RemainMinions() == 0) {
                clearR2 = true;
                gameObject.SetActive(false);
                return;
            }
            // originalPosition = transform.position;
            // aboveGround = new Vector3(originalPosition.x, 0.1F, 12F);
            // belowGround = new Vector3(originalPosition.x, -10F, 12F);

            // if (transform.position != originalPosition) {
            //     transform.position = Vector3.Lerp(transform.position, belowGround, 1f);
            // } else {
            //     transform.position = Vector3.Lerp(transform.position, aboveGround, 1f);
            // }

            // originalPosition = transform.position;

            //checks if room has enemies
            foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                var distanceToEnemy = Vector3.Distance(gameObject.transform.position,enemies.transform.position);
                if (distanceToEnemy < 40)
                {
                    armScript.Frenzy = true;
                    return;
                } 
            }
        }
    }
}
