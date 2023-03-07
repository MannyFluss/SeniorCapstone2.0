using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBoxBehavior : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    bool breaks;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            Vector3 dir = transform.position - other.transform.position;
            dir.y = 0;

            rb.AddForce(dir.normalized * 8, ForceMode.Impulse);
        }
    }
}
