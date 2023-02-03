using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnemyAttack : MonoBehaviour
{
    public float attackLast = 1f;
    public float cooldown = 5f;

    private bool _attackAvailable = true;
    private MeshRenderer meshRenderer;
    public Collider collider;
    public EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInParent<EnemyMovement>().attack = gameObject.GetComponent<EnemyAttack>();
        enemyMovement = enemyMovement.GetComponentInParent<EnemyMovement>();

        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PiranhaAttack()
    {
        if(_attackAvailable)
        {
            enemyMovement.Charge();
            StartCoroutine(PiranhaAttactTimer());
            StartCoroutine(PiranhaCooldown());
        }
    }

    IEnumerator PiranhaAttactTimer()
    {
        collider.enabled = true;
        meshRenderer.enabled = true;
        yield return new WaitForSeconds(attackLast);
        collider.enabled = false;
        meshRenderer.enabled = false;
    }

    IEnumerator PiranhaCooldown()
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

    IEnumerator PufferAttackTimer()
    {
        collider.enabled = true;
        meshRenderer.enabled = true;
        enemyMovement._invincible = true;
        yield return new WaitForSeconds(attackLast);
        enemyMovement._invincible = false;
        collider.enabled = false;
        meshRenderer.enabled = false;
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


}
