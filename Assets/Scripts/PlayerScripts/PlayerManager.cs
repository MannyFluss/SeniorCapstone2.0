using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Stats")]
    public float health = 9;

    private bool canBeHit = true;

    PlayerMovement movement;


    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
       if(!canBeHit)
       {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r,
                                                                        0.3f,
                                                                        0.3f,
                                                                        0.4f);
       }
       else
       {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r,
                                                                        1,
                                                                        1,
                                                                        1f);
        }
    }

    IEnumerator HitCooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(1f);
        canBeHit = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && canBeHit)
        {
            
            health--;
            StartCoroutine(HitCooldown());
            //movement.playerHit(collision.transform);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && canBeHit)
        {
            health--;
            StartCoroutine(HitCooldown());
            //movement.playerHit(other.transform);
        }
    }
}
