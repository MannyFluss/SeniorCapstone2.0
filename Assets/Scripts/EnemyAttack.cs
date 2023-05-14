using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnemyAttack : MonoBehaviour
{
    public float attackLast = 1f;
    public float cooldown = 5f;
    public float anticipationTime = 1f;
    public GameObject projectile;
    public GameObject bettaShort;
    public GameObject bettaMedium;
    public GameObject bettaLong;
    public GameObject VFXObject;
    public Animator animator;
    public bool _attackAvailable = true;
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

    private void AnticipationVFXStart()
    {
        var instance = Instantiate(VFXObject, transform.position, Quaternion.identity);
        instance.transform.parent = transform.parent.gameObject.transform;
        // Vector3 inverseParentScale = new Vector3(transform.localScale.x / 1, transform.localScale.y / 1, transform.localScale.z / 1);
        // instance.transform.localScale = inverseParentScale;
        instance.GetComponent<ParticleSystem>().Play();
        print("Anticipation VFX");
        
        // Anticipation VFX
        
    }

    private void AnticipationVFXEnd()
    {
        // Anticipation VFX
    }

    public void EelAttack()
    {
        GetComponent<Collider>().enabled = true;
        meshRenderer.enabled = true;
    }

    public void EelCancelAttack()
    {
        GetComponent<Collider>().enabled = false;
        meshRenderer.enabled = false;
    }

    public void BettaFishAttack()
    {
        if(_attackAvailable)
        {
            enemyMovement.Charge(force: 500);
            StartCoroutine(BettaFishAttactTimer());
            StartCoroutine(BettaFishCooldown());
        }
    }

    IEnumerator BettaFishAttactTimer()
    {
        AnticipationVFXStart();
        yield return new WaitForSeconds(anticipationTime);
        AnticipationVFXEnd();

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
            StartCoroutine(ArcherAttackTimer());
            StartCoroutine(ArcherCooldown());
        }

    }

    public IEnumerator ArcherAttackTimer()
    {
        AnticipationVFXStart();
        yield return new WaitForSeconds(anticipationTime);
        AnticipationVFXEnd();

        animator.SetBool("isAttack", true);
        GameObject arrow = Instantiate(projectile);
        Destroy(arrow, attackLast);
        arrow.transform.position = transform.position;
    }

    public IEnumerator SquidCooldown()
    {
        enemyMovement._pursuePlayer = false;
        _attackAvailable = false;
        yield return new WaitForSeconds(cooldown);
        _attackAvailable = true;
        enemyMovement._pursuePlayer = true;
    }

    public IEnumerator SquidAttactTimer(int numberOfAttack)
    {
        AnticipationVFXStart();
        yield return new WaitForSeconds(anticipationTime);
        AnticipationVFXEnd();


        enemyMovement._pursuePlayer = false;

        GameObject arrow = Instantiate(projectile);
        arrow.transform.position = transform.position;
        Destroy(arrow, 5);
        yield return new WaitForSeconds(attackLast);
        if (numberOfAttack > 1)
        {
            StartCoroutine(SquidAttactTimer(numberOfAttack - 1));
        }
    }

    public void SquidAttack()
    {
        if (_attackAvailable)
        {
            StartCoroutine(SquidAttactTimer(3));
            StartCoroutine(SquidCooldown());
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
        AnticipationVFXStart();
        yield return new WaitForSeconds(anticipationTime);
        AnticipationVFXEnd();

        animator.SetBool("isPuffedUp", true);
        GetComponent<Collider>().enabled = true;
        meshRenderer.enabled = true;
        enemyMovement._invincible = true;
        yield return new WaitForSeconds(attackLast);
        animator.SetBool("isPuffedUp", false);
        GetComponent<Collider>().enabled = false;
        meshRenderer.enabled = false;
        enemyMovement._invincible = false;
        enemyMovement._avoidPlayer = true;
    }


    IEnumerator PufferCooldown()
    {
        enemyMovement._pursuePlayer = false;
        //enemyMovement._avoidPlayer = true;
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
            enemyMovement.Charge(force: 100);
            StartCoroutine(PiranhaAttackTimer());
            StartCoroutine(PiranhaCooldown());
        }

    }

    IEnumerator PiranhaAttackTimer()
    {
        AnticipationVFXStart();
        yield return new WaitForSeconds(anticipationTime);
        AnticipationVFXEnd();

        GetComponent<Collider>().enabled = true;
        meshRenderer.enabled = true;
        yield return new WaitForSeconds(attackLast);
        GetComponent<Collider>().enabled = false;
        meshRenderer.enabled = false;
    }

    IEnumerator BettaMediumAttackTimer()
    {
        AnticipationVFXStart();
        yield return new WaitForSeconds(anticipationTime);
        AnticipationVFXEnd();

        bettaMedium.SetActive(true);
        yield return new WaitForSeconds(attackLast);
        bettaMedium.SetActive(false);
        StartCoroutine(BettaLongAttackTimer());
    }

    IEnumerator BettaLongAttackTimer()
    {
        AnticipationVFXStart();
        yield return new WaitForSeconds(anticipationTime);
        AnticipationVFXEnd();

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
