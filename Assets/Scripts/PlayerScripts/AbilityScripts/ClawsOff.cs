using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClawsOff : BaseAbilityScript
{
    // Start is called before the first frame update

    
    public override void OnButtonClick()
    {
        Instantiate( parentScriptRef._AbilityExplosionPrefab, myParent.transform.position, Quaternion.identity);
    }
}

public class SchrodingerBox : BaseAbilityScript
{
    public override void OnButtonClick()
    {
        Instantiate( parentScriptRef._AbilityBoxPrefab, myParent.transform.position, Quaternion.identity);
    }
}

public class ExplosiveFishAbility : BaseAbilityScript
{
    public override void OnButtonClick()
    {
        Instantiate( parentScriptRef._AbilityExplosiveFish, myParent.transform.position, Quaternion.identity);
    }
}

