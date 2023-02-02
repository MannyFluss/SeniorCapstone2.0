using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawsOff : BaseAbilityScript
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject explosionPrefab;
    public override void OnButtonClick()
    {
        Instantiate(explosionPrefab,myParent.transform.position,Quaternion.identity);
    }
}
