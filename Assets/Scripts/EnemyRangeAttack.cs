using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    private Transform player;
    private Vector3 targetPosition;
    public GameObject arrowHead;
    private bool initLocaReached = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = player.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initLocaReached)
        {
            transform.LookAt(arrowHead.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, arrowHead.transform.position, 7f * Time.deltaTime);
        } else
        {
            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 7f * Time.deltaTime);

            if(transform.position == targetPosition)
            {
                initLocaReached = true;
            }
        }
    }


}
