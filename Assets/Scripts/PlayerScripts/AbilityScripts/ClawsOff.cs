using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClawsOff : BaseAbilityScript
{
    // Start is called before the first frame update

    new float abilityCoolDown = 1.8f;
    new public string abilityName = BaseAbilityScript.AbilitiesList[0];
    public override void OnButtonClick()
    {
        if (getCoolDownStatus())
        {
            return;
        }
        Instantiate( parentScriptRef._AbilityExplosionPrefab, myParent.transform.position, Quaternion.identity);
        startCoolDown(abilityCoolDown);
    }
    public override string getAbilityName()
    {
        return abilityName;
    }

}
public class NavalMine : BaseAbilityScript
{
    new float abilityCoolDown = 2.6f;
    new public string abilityName = BaseAbilityScript.AbilitiesList[3];
    private Vector3 spawnOffset = new Vector3(0,0.5f,0);
    public override void OnButtonClick()
    {
        if (getCoolDownStatus())
        {
            return;
        }
        Instantiate(parentScriptRef._AbilityNavalMine,myParent.transform.position - spawnOffset,Quaternion.identity);
        startCoolDown(abilityCoolDown);
    }
    public override string getAbilityName()
    {
        return abilityName;
    }
}

public class SchrodingerBox : BaseAbilityScript
{
    new float abilityCoolDown = 2.8f;
    new public string abilityName = BaseAbilityScript.AbilitiesList[1];

    public override void OnButtonClick()
    {
        if (getCoolDownStatus())
        {
            return;
        }

      //
        Instantiate( parentScriptRef._AbilityBoxPrefab, myParent.transform.position , Quaternion.identity);
        startCoolDown(abilityCoolDown);
    }
    public override string getAbilityName()
    {
        return abilityName;
    }
}

public class ExplosiveFishAbility : BaseAbilityScript
{
    new float abilityCoolDown = 3.4f;
    new public string abilityName = BaseAbilityScript.AbilitiesList[2];
    private Vector3 spawnOffset = new Vector3(0,-1,0);


    public override void OnButtonClick()
    {
        if (getCoolDownStatus())
        {
            return;
        }

        Vector3 aimArrow = parentScriptRef.GetComponent<CharacterAttack>().AimPositionReference.transform.position;
        Instantiate( parentScriptRef._AbilityExplosiveFish, aimArrow + spawnOffset, Quaternion.identity);
        startCoolDown(abilityCoolDown);

    }
    public override string getAbilityName()
    {
        return abilityName;
    }
}

