using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveFish : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject explosionReference;
    float timeUntilExplode = 3.0f;
    Vector3 directionMoving = Vector3.zero;
    float moveSpeed = 50f;

    void Start()
    {
        Destroy(gameObject,timeUntilExplode);
    }

    void OnDestroy()
    {
        Instantiate(explosionReference,this.transform.position,Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Attack")
        {
            //set moving direction 
            if (other.name == "HitBox")
            {
                GameObject player = FindObjectOfType<CharacterController>().gameObject;
                directionMoving = (this.transform.position - player.transform.position).normalized * moveSpeed;
                directionMoving.y = 0;
            }
            else
            {
                directionMoving = (this.transform.position - other.transform.position).normalized * moveSpeed;
                directionMoving.y = 0;
            }

        }
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(directionMoving * Time.deltaTime);
    }
}
