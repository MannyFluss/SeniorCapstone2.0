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
    private Vector3 spawnOffset = new Vector3(0,0.5f,0);


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
public class KnivesOut : BaseAbilityScript
{
    new float abilityCoolDown = 3.0f;
    new public string abilityName = BaseAbilityScript.AbilitiesList[4];

    public override void OnButtonClick()
    {
        if (getCoolDownStatus())
        {
            return;
        }
        Vector3 aimArrow = parentScriptRef.GetComponent<CharacterAttack>().AimPositionReference.transform.position;
        GameObject instance = Instantiate( parentScriptRef._AbilityKnivesOut, myParent.transform.position, Quaternion.identity);
        
        Vector3 direction = aimArrow - myParent.transform.position;
        direction.y = 0;
        instance.GetComponent<SplitKnives>().initialize(direction,true);
        startCoolDown(abilityCoolDown);
    }    

    public override string getAbilityName()
    {
        return abilityName;
    }

}
public class HeartyFix : BaseAbilityScript
{
    new float abilityCoolDown = 30.0f;
    new public string abilityName = BaseAbilityScript.AbilitiesList[5];

    public override void OnButtonClick()
    {
        
        if (getCoolDownStatus())
        {
            return;
        }
        myParent.GetComponent<PlayerManager>().heal();

        startCoolDown(abilityCoolDown);
    
    }

    public override string getAbilityName()
    {
        return abilityName;
    }
}
public class KittyFortress : BaseAbilityScript
{
    new float abilityCoolDown = 12.0f;
    new public string abilityName = BaseAbilityScript.AbilitiesList[6];
    public override void OnButtonClick()
    {
        if (getCoolDownStatus())
        {
            return;
        }
        GameObject instance = Instantiate( parentScriptRef._AbilityKittyFortress, myParent.transform.position, Quaternion.identity);
        instance.GetComponent<CatWall>().setPlayer(myParent);
        startCoolDown(abilityCoolDown);
    }

}



