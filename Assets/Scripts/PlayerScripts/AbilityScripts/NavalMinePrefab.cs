using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavalMinePrefab : MonoBehaviour
{
    [SerializeField]
    GameObject _explosionPrefab;

    void Start()
    {
        Destroy(gameObject,4.0f);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(_explosionPrefab,gameObject.transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
