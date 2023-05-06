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

    public GameObject clone = null;

    public Animator animator = null;

    public MeshRenderer indicator = null;

    private Collider cd;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        cd = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        if (type == "ArcherFish")
        {
            StartCoroutine(attack.ArcherCooldown());
        }

        if (type == "Eel")
        {
            indicator.enabled = false;
            StartCoroutine(HideBillBorad());
        }
        StartCoroutine(SpawnCoolDown());
    }

    private void Update()
    {
        //AvoidPlayer();
        if (type == "Crab" || type == "Squid")
        {
            sprite.color = Color.magenta;
        }

        if (type == "JellyFish")
        {
            sprite.color = Color.green;
        }

        if (_pursuePlayer)
        {
            MoveToPlayer();
        }

        if (_avoidPlayer)
        {
            AvoidPlayer();
        }
        
        if (health <= 0)
        {
            if (clone != null)
            {
                GameObject clone1 = clone;
                GameObject clone2 = clone;

                clone1.transform.position = gameObject.transform.position;
                clone2.transform.position = gameObject.transform.position;

                Instantiate(clone1);
                Instantiate(clone2);

                clone = null;
            }

            _pursuePlayer = false;
            _avoidPlayer = false;
            attack._attackAvailable = false;
            navMeshAgent = null;
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
            if(type == "Crab")
            {
                attack.PiranhaAttack();
            }

            if(type == "ArcherFish")
            {
                attack.ArcherAttack();
            }

            if(type == "Betta")
            {
                attack.BettaFishAttack();
            }

            if(type == "Squid")
            {
                attack.SquidAttack();
            }

            if(type == "JellyFish")
            {
                attack.PiranhaAttack();
            }

            if(type == "Eel")
            {
                StartCoroutine(ShowBillBoard());
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack")
        {
            Damage(attackObject: other.gameObject, _knockBack: true);
        }

        if(other.tag == "Player" && type != "Eel")
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

    public void Charge(float force)
    {
        Vector3 moveDirection = (player.position - gameObject.transform.position).normalized;
        rb.AddForce(moveDirection * force);
    }

    private void MoveToPlayer()
    {
        navMeshAgent.destination = player.position;
    }

    private void AvoidPlayer()
    {
        Vector3 moveDirection = transform.position - player.position;
        navMeshAgent.destination = moveDirection.normalized * 10f;

    }

    private void Damage(GameObject attackObject, bool _knockBack)
    {

        if (!_invincible)
        {
            KnockBack(from: attackObject.transform.position, force: 500f);
            health -= 1;
            StartCoroutine(DamageCoolDown());
        }
    }

    IEnumerator HideBillBorad()
    {
        animator.SetBool("hideBillboard", true);
        yield return new WaitForSeconds(1.5f);
        _pursuePlayer = true;
        indicator.enabled = true;
        cd.isTrigger = true;
        _invincible = true;
    }

    IEnumerator StartAttack()
    {
        attack.EelAttack();
        yield return new WaitForSeconds(1);
        attack.EelCancelAttack();
    }

    IEnumerator ShowBillBoard()
    {
        _pursuePlayer = false;
        animator.SetBool("hideBillboard", false);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(StartAttack());
        indicator.enabled = false;
        cd.isTrigger = false;
        _invincible = false;
        yield return new WaitForSeconds(5);
        StartCoroutine(HideBillBorad());
    }

    IEnumerator DamageCoolDown()
    {
        sprite.color = new Color(sprite.color.r,0.3f,0.3f,0.4f);
        _invincible = true;
        yield return new WaitForSeconds(invincibleCooldown);
        _invincible = false;
        sprite.color = new Color(sprite.color.r,1f,1f,1f);
    }

    IEnumerator SpawnCoolDown()
    {
        _invincible = true;
        yield return new WaitForSeconds(2);
        _invincible = false;
    }
}
