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

    // for movement optimization
    private bool _isInContact;

    // for groudcheck
    private Rigidbody rb;

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
        if (_hit)
        {
            KnockBack(moveFrom: moveFrom.position);
        }

        if (IsGrounded())
        {
            if (!_isInContact)
            {
                MoveToPlayer();
                _isInContact = false;
            }
        }
    }

    private bool IsGrounded() {
        float _distanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, _distanceToTheGround + 0.1f);
    }

    private void MoveToPlayer()
    {
        Vector3 moveTo = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed);
    }

    private void KnockBack(Vector3 moveFrom)
    {
        transform.position = new Vector3(Vector3.MoveTowards(transform.position, moveFrom, -knockback).x, transform.position.y, Vector3.MoveTowards(transform.position, moveFrom, -knockback).z);
    }
  
    private void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject.tag == "Attack")
        {
            moveFrom = other.transform.parent.parent;
            StartCoroutine(hit());
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") {
            _isInContact = true;
        }

        if(other.tag == "Enemy")
        {
            _isInContact = true;
        }
    }

    IEnumerator hit()
    {
        _hit = true;
        yield return new WaitForSeconds(0.15f);
        _hit = false;
    }
}
