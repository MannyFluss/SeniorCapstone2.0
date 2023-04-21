using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitKnives : MonoBehaviour
{
    [SerializeField]
    float speed = 7;
    Vector3 direction = new Vector3(0,0,0);
    bool moreKnives = false;
    GameObject ignore = null;

    void start()
    {
        Destroy(gameObject, 5);

    }

    
    void Update()
    {
        gameObject.transform.position += (direction * speed) * Time.deltaTime;
    }
    //we need to set the direction and moreKnives
    public void initialize(Vector3 dir, bool moreKnives, GameObject ignore = null)
    {
        direction = dir;
        this.moreKnives = moreKnives;
        this.ignore = ignore;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            onHit(other);    
        }
    }

    void onHit(Collider other)
    {
        if (other.gameObject == ignore)
        {
            return;
        }


        if (moreKnives == true)
        {
            GameObject kniveCopy = gameObject;
            GameObject knife1 = Instantiate(kniveCopy, gameObject.transform.position, Quaternion.identity);
            GameObject knife2 = Instantiate(kniveCopy, gameObject.transform.position, Quaternion.identity);
            GameObject knife3 = Instantiate(kniveCopy, gameObject.transform.position, Quaternion.identity);
            Vector3 leftRotate = Quaternion.AngleAxis(-45, Vector3.up) * direction;
            Vector3 rightRotate = Quaternion.AngleAxis(45, Vector3.up) * direction;
            knife1.GetComponent<SplitKnives>().initialize(leftRotate, false, other.gameObject);
            knife2.GetComponent<SplitKnives>().initialize(rightRotate, false, other.gameObject);
            knife3.GetComponent<SplitKnives>().initialize(direction, false, other.gameObject); 
        }

        Destroy(gameObject);
        
        
    }
}

