using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    private Transform player;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = player.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 7f * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }


}
