using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public GameObject dummy;
    public TextMeshPro text;
    private GameObject clone;
    private EnemyMovement em;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        initiateDummy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !em.enabled)
        {
            StartCoroutine(initiateDummy(2f));
        }
    }

    IEnumerator initiateDummy(float waitTime)
    {
        text.text = "reloading...";
        em.enabled = true;
        yield return new WaitForSeconds(waitTime);
        initiateDummy();
    }

    void initiateDummy()
    {
        clone = Instantiate(dummy);
        text.text = dummy.name;
        em = clone.GetComponent<EnemyMovement>();
        em.enabled = false;
        clone.transform.position = transform.position;
    }
}
