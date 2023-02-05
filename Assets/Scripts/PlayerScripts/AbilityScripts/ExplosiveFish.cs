using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject explosionReference;
    float timeUntilExplode = 3.0f;
    Vector3 directionMoving;

    void Start()
    {
        Destroy(gameObject,timeUntilExplode);

    }

    void OnDestroy()
    {
        Instantiate(explosionReference,this.transform.position,Quaternion.identity);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
