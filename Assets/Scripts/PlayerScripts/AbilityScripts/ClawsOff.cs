using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClawsOff : BaseAbilityScript
{
    // Start is called before the first frame update

    new float abilityCoolDown = 2.5f;
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

public class SchrodingerBox : BaseAbilityScript
{
    new float abilityCoolDown = 1f;
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
    new float abilityCoolDown = 1f;
    new public string abilityName = BaseAbilityScript.AbilitiesList[2];

    public override void OnButtonClick()
    {
        if (getCoolDownStatus())
        {
            return;
        }
        Quaternion aimArrow = parentScriptRef.GetComponent<CharacterAttack>().getAimArrow().rotation;
        Vector3 direction = Vector3.Normalize(aimArrow * myParent.transform.position) * 3;
        Instantiate( parentScriptRef._AbilityExplosiveFish, myParent.transform.position + direction, Quaternion.identity);
        startCoolDown(abilityCoolDown);
    }
    public override string getAbilityName()
    {
        return abilityName;
    }
}

