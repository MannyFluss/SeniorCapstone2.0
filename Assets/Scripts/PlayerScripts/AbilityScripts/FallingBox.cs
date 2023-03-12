using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBox : MonoBehaviour
{
    public GameObject explosionReference;
    public GameObject dotReference;
    public float upBy = 1f;
    public float fallTime = 1f;
    // Start is called before the first frame update


    void Start()
    {
        Vector3 targetPosition = gameObject.transform.position;

        Instantiate(dotReference,targetPosition,Quaternion.Euler(90,0,0));

        this.gameObject.transform.Translate(new Vector3(0,upBy,0));
        LeanTween.moveY(gameObject,this.gameObject.transform.position.y - upBy,fallTime);
        Destroy(gameObject,fallTime);

        //startPosition
        
        //add to the Y
        //start a tween to original position
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
