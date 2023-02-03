using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class EnemyMovement : MonoBehaviour
{
    public bool _invincible = false;

    public float invincibleCooldown = 1f;

    public int health = 1;

    public float attackDis = 1f;

    public string type = "Pufferfish";

    private Transform player;

    private NavMeshAgent navMeshAgent;

    private Rigidbody rb;

    [HideInInspector]
    public SpriteRenderer sprite;

    public EnemyAttack attack;

    public bool _pursuePlayer = true;

    public bool _avoidPlayer = false;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!_invincible)
        {
            if (_pursuePlayer)
            {
                MoveToPlayer();
            }
            else if (_avoidPlayer)
            {
                AvoidPlayer();
            }
            
        }
        if (health <= 0)
        {
            gameObject.transform.rotation = new Quaternion(x: 0, y: 0, z: 90, w: 1);
            Destroy(gameObject, invincibleCooldown);
        }

        float disToPlayer = Vector3.Distance(player.position, gameObject.transform.position);

        if(disToPlayer <= attackDis)
        {
            if(type == "Pufferfish")
            {
                attack.PufferAttack();
            }

            if(type == "Piranha")
            {
                attack.PiranhaAttack();
            }
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack")
        {
            Damage(attackObject: other.gameObject, _knockBack: true);
        }

        if(other.tag == "Player")
        {
            KnockBack(from: other.transform.position, force: 200f);
        }
    }

    private void KnockBack(Vector3 from, float force)
    {
        Vector3 moveDirection = from - gameObject.transform.position;

        if (!_invincible)
        {
            rb.AddForce(moveDirection.normalized * -force);
        }

    }

    public void Charge()
    {
        Vector3 moveDirection = (player.position - gameObject.transform.position).normalized;
        rb.AddForce(moveDirection * 500f);
    }

    private void MoveToPlayer()
    {
        navMeshAgent.destination = player.position;
    }

    private void AvoidPlayer()
    {
        Vector3 moveDirection = transform.position - player.position;
        navMeshAgent.destination = moveDirection;
    }

    private void Damage(GameObject attackObject, bool _knockBack)
    {

        if (!_invincible)
        {
            KnockBack(from: attackObject.transform.position, force: 500f);
            health -= 1;
        }
        StartCoroutine(DamageCoolDown());
    }

    IEnumerator DamageCoolDown()
    {
        sprite.color = new Color(sprite.color.r,0.3f,0.3f,0.4f);
        _invincible = true;
        yield return new WaitForSeconds(invincibleCooldown);
        _invincible = false;
        sprite.color = new Color(sprite.color.r,1f,1f,1f);
    }
}
