using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float explosionTime = 1.0f;
    [SerializeField]
    float xScale = 3.0f;
    [SerializeField]
    float zScale = 3.0f;
    [SerializeField]
    float damageValue = 1.0f;
    void Start()
    {
        
        LeanTween.scaleX(this.gameObject,xScale,explosionTime);
        LeanTween.scaleZ(this.gameObject,zScale,explosionTime);
        Destroy(this.gameObject,explosionTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
