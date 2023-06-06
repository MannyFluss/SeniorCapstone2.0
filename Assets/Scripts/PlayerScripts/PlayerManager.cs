using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //added DamageTaken sound effect
    [SerializeField] 
    private AudioSource DamageTakenSoundEffect;

    [SerializeField]
    private string SceneName;

    [Header("Player Stats")]
    public float health = 9;

    public bool canBeHit = true;

    PlayerMovement movement;
    [SerializeField]
    PlayerHud PlayerHUDReference;

    private Animator animator;
    private CharacterAttack attack;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<CharacterAttack>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (health == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator HitCooldown()
    {
        canBeHit = false;
        animator.SetBool("isDamaged", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("isDamaged", false);
        attack.enabled = true;
        movement.enabled = true;
        StartCoroutine(Blink());
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator Blink(){
        for (float i = 0; i <= 3f; i += 0.6f) {
            yield return new WaitForSeconds(0.2f);
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.44f, 0.46f, 0.8f);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.4f);
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        canBeHit = true;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        PlayerHUDReference.setUIHearts(((int)health));

        StartCoroutine(HitCooldown());
        //movement.playerHit(other.transform);

        //added damagetaken sound effect
        DamageTakenSoundEffect.Play();
    }

    public void heal()
    {
        health += 1;
        PlayerHUDReference.setUIHearts(((int)health));
    }

    public void toggleHit()
    {
        canBeHit = !canBeHit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack" && canBeHit)
        {
            takeDamage(1);

        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "EnemyAttack" && canBeHit)
        {
            takeDamage(1);
        }
    }
}
