using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //for Hit
    private bool _hit = false;
    private Transform moveFrom;

    //for movement
    private Transform player;

    //changeable vars
    [Header("Changeable Values")]
    [SerializeField]
    private float knockback = 0.15f;
    [SerializeField]
    private float moveSpeed = 0.01f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed);
        if(_hit)
        {
            transform.position = new Vector3(Vector3.MoveTowards(transform.position, moveFrom.position, -knockback).x, transform.position.y, Vector3.MoveTowards(transform.position, moveFrom.position, -knockback).z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Attack")
        {
            moveFrom = other.transform.parent.parent;
            StartCoroutine(hit());
        }
    }

    IEnumerator hit()
    {
        _hit = true;
        yield return new WaitForSeconds(0.15f);
        _hit = false;
    }
}
