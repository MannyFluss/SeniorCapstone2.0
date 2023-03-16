using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnemyAttack : MonoBehaviour
{
    public float attackLast = 1f;
    public float cooldown = 5f;
    public GameObject projectile;
    public GameObject bettaShort;
    public GameObject bettaMedium;
    public GameObject bettaLong;
    public Animator animator;
    private bool _attackAvailable = true;
    private MeshRenderer meshRenderer;
    private Transform player;
    //public Collider collider;
    public EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInParent<EnemyMovement>().attack = gameObject.GetComponent<EnemyAttack>();
        enemyMovement = enemyMovement.GetComponentInParent<EnemyMovement>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        GetComponent<Collider>().enabled = false;
        //StartCoroutine(PiranhaCooldown());
        //StartCoroutine(PufferCooldown());
        //StartCoroutine(BettaCooldown());
    }

    // Update is called once per frame
    void Update()
    {
    }



    public void BettaFishAttack()
    {
        if(_attackAvailable)
        {
            enemyMovement.Charge();
            StartCoroutine(BettaFishAttactTimer());
            StartCoroutine(BettaFishCooldown());
        }
    }

    IEnumerator BettaFishAttactTimer()
    {
        animator.SetBool("Charge", true);
        GetComponent<Collider>().enabled = true;
        meshRenderer.enabled = true;
        yield return new WaitForSeconds(attackLast);
        animator.SetBool("Charge", false);
        GetComponent<Collider>().enabled = false;
        meshRenderer.enabled = false;
    }

    IEnumerator BettaFishCooldown()
    {
        _attackAvailable = false;
        yield return new WaitForSeconds(cooldown);
        _attackAvailable = true;
    }

    public void PufferAttack()
    {
        if (_attackAvailable)
        {
            StartCoroutine(PufferAttackTimer());
            StartCoroutine(PufferCooldown());
        }
        
    }

    public void ArcherAttack()
    {
        if (_attackAvailable)
        {
            animator.SetBool("isAttack", true);
            GameObject arrow = Instantiate(projectile);
            arrow.transform.position = transform.position;
            StartCoroutine(ArcherCooldown());
        }

    }

    public IEnumerator ArcherCooldown()
    {
        enemyMovement._pursuePlayer = false;
        enemyMovement._avoidPlayer = true;
        _attackAvailable = false;
        yield return new WaitForSeconds(cooldown);
        _attackAvailable = true;
        enemyMovement._pursuePlayer = true;
        enemyMovement._avoidPlayer = false;
        animator.SetBool("isAttack", false);
    }

    IEnumerator PufferAttackTimer()
    {
        animator.SetBool("isPuffedUp", true);
        GetComponent<Collider>().enabled = true;
        meshRenderer.enabled = true;
        enemyMovement._invincible = true;
        yield return new WaitForSeconds(attackLast);
        animator.SetBool("isPuffedUp", false);
        GetComponent<Collider>().enabled = false;
        meshRenderer.enabled = false;
        enemyMovement._invincible = false;
    }


    IEnumerator PufferCooldown()
    {
        enemyMovement._pursuePlayer = false;
        enemyMovement._avoidPlayer = true;
        _attackAvailable = false;
        yield return new WaitForSeconds(cooldown);
        _attackAvailable = true;
        enemyMovement._pursuePlayer = true;
        enemyMovement._avoidPlayer = false;
    }

    public void PiranhaAttack()
    {
        if (_attackAvailable)
        {
            StartCoroutine(PiranhaAttackTimer());
            StartCoroutine(PiranhaCooldown());
        }

    }

    IEnumerator PiranhaAttackTimer()
    {
        GetComponent<Collider>().enabled = true;
        meshRenderer.enabled = true;
        yield return new WaitForSeconds(attackLast);
        GetComponent<Collider>().enabled = false;
        meshRenderer.enabled = false;
    }

    IEnumerator BettaMediumAttackTimer()
    {
        bettaMedium.SetActive(true);
        yield return new WaitForSeconds(attackLast);
        bettaMedium.SetActive(false);
        StartCoroutine(BettaLongAttackTimer());
    }

    IEnumerator BettaLongAttackTimer()
    {
        bettaLong.SetActive(true);
        yield return new WaitForSeconds(attackLast);
        bettaLong.SetActive(false);
    }

    IEnumerator PiranhaCooldown()
    {
        //enemyMovement._pursuePlayer = false;
        _attackAvailable = false;
        yield return new WaitForSeconds(cooldown);
        _attackAvailable = true;
        //enemyMovement._pursuePlayer = true;
    }


}
