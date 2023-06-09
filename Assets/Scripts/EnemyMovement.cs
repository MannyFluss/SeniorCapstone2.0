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

    public NavMeshAgent navMeshAgent;

    private Rigidbody rb;

    [HideInInspector]
    public SpriteRenderer sprite;

    public EnemyAttack attack;

    public bool _spawnFreeze = false;

    public bool _pursuePlayer = true;

    public bool _avoidPlayer = false;

    private bool _initAttackVoid = true;

    public GameObject clone = null;

    public Animator animator = null;

    public MeshRenderer indicator = null;

    private Collider cd;

    private bool _isDead = false;

    private bool _eeldetectpause = false;

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
            _pursuePlayer = false;
            indicator.enabled = false;
            StartCoroutine(HideBillBorad());
        }
        StartCoroutine(SpawnCoolDown());
    }

    private void Update()
    {

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
            if (!_isDead)
            {
                if (clone != null)
                {
                    StartCoroutine(Clone());
                }
                else
                {
                    _pursuePlayer = false;
                    _avoidPlayer = false;
                    attack._attackAvailable = false;
                    navMeshAgent = null;
                    gameObject.transform.rotation = new Quaternion(x: 0, y: 0, z: 90, w: 1);

                    Destroy(gameObject, invincibleCooldown);
                }
            }
            
        }
              

        float disToPlayer = Vector3.Distance(player.position, gameObject.transform.position);

       if(!_initAttackVoid)
        {
            if (disToPlayer <= attackDis)
            {
                if (type == "Pufferfish")
                {
                    attack.PufferAttack();
                }

                if (type == "Piranha")
                {
                    attack.PiranhaAttack();

                }
                if (type == "Crab")
                {
                    attack.PiranhaAttack();
                }

                if (type == "ArcherFish")
                {
                    attack.ArcherAttack();
                }

                if (type == "Betta")
                {
                    attack.BettaFishAttack();
                }

                if (type == "Squid")
                {
                    Halt();
                    attack.SquidAttack();
                }

                if (type == "JellyFish")
                {
                    attack.PiranhaAttack();
                }

                if (type == "Eel" && !_eeldetectpause)
                {
                    _pursuePlayer = false;
                    Halt();
                    StartCoroutine(ShowBillBoard());
                }
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
        if (!_invincible && type != "Eel")
        {
            try {
                rb.AddForce(moveDirection.normalized * -force);
            } catch
            {
                print("Failed to knock back");
            }
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

    public void Halt()
    {
        navMeshAgent.destination = transform.position;
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
            if(type == "Eel")
            {
                _pursuePlayer = false;
                Halt();
                StartCoroutine(HideBillBorad());
            } else
            {
                KnockBack(from: attackObject.transform.position, force: 500f);
            }
            
            health -= 1;
            StartCoroutine(DamageCoolDown());
        }
    }

    IEnumerator HideBillBorad()
    {
        _eeldetectpause = true;
        animator.SetBool("hideBillboard", true);
        yield return new WaitForSeconds(1.5f);
        _pursuePlayer = true;
        indicator.enabled = true;
        cd.isTrigger = true;
        _invincible = true;
        _eeldetectpause = false;
    }

    IEnumerator StartAttack()
    {
        attack.EelAttack();
        yield return new WaitForSeconds(1);
        attack.EelCancelAttack();
    }

    IEnumerator ShowBillBoard()
    {
        _eeldetectpause = true;
        _pursuePlayer = false;
        Halt();
        yield return new WaitForSeconds(2f);
        _pursuePlayer = false;
        Halt();
        _eeldetectpause = true;
        animator.SetBool("hideBillboard", false);
        yield return new WaitForSeconds(0.1f);
        _pursuePlayer = false;
        Halt();
        _eeldetectpause = true;
        StartCoroutine(StartAttack());
        indicator.enabled = false;
        cd.isTrigger = false;
        _invincible = false;
        yield return new WaitForSeconds(5);
        _pursuePlayer = false;
        Halt();
        _eeldetectpause = true;
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
        bool pursueMemeory = _pursuePlayer;
        _invincible = true;
        if (_spawnFreeze)
        {
            _pursuePlayer = false;
        }
        yield return new WaitForSeconds(2);
        if (_spawnFreeze)
        {
            _pursuePlayer = pursueMemeory;
        }
        _invincible = false;
        _initAttackVoid = false;
    }

    IEnumerator Clone()
    {
        _isDead = true;
        yield return new WaitForSeconds(2);
        GameObject clone1 = clone;
        GameObject clone2 = clone;

        clone1.transform.position = new Vector3(x: gameObject.transform.position.x, y: gameObject.transform.position.y, z: gameObject.transform.position.z - 5f);
        clone2.transform.position = new Vector3(x: gameObject.transform.position.x, y: gameObject.transform.position.y, z: gameObject.transform.position.z);

        Instantiate(clone1);
        Instantiate(clone2);

        Destroy(gameObject);
    }
}
