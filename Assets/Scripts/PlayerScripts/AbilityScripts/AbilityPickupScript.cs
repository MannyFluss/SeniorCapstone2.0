using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    [SerializeField]
    public string abilityName = "";

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //player gets ability
            bool abilityAdd = other.GetComponent<CharacterAbilityScript>().AbilityPickUpInteract(abilityName);
            if (abilityAdd)
            {
                Destroy(gameObject);
            }

        }
    }
}
