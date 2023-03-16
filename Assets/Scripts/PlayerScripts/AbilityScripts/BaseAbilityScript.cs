using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is the base class, it should not be used
public  class BaseAbilityScript : MonoBehaviour
{
    public static string[] AbilitiesList = new string[]
    {"ClawsOff","SchrodingerBox","ExplosiveFishAbility","NavalMine"};
    public static Dictionary<string,string> AbilityDescriptions = new Dictionary<string, string>
    {
        {"ClawsOff" , "Attack Enemies in a Radius around you"},
        { "SchrodingerBox", "Upon Dashing, an explosive box will fall from the sky"},
        { "ExplosiveFishAbility", "Summon an explosive fish in front of you, hit it to lo launch"},
        {"NavalMine","create a mine on your position that explodes when enemies get close"},

        {"empty","No Ability"},
    };
    private static bool initFlag = false;


    public GameObject myParent;
    public CharacterAbilityScript parentScriptRef;

    protected float abilityCoolDown;
    protected string abilityName;
    private bool onCoolDown = false;
    
    public virtual string getAbilityName()
    {
        return abilityName;
    }

    // signal functions for each of the child classes
    public virtual void OnEquip()
    {
        
        parentScriptRef = myParent.GetComponent<CharacterAbilityScript>();
        //Debug.Log("equipped");
    }
    public virtual void OnDrop()
    {
        //Debug.Log("dropped");
    }
    public virtual void OnButtonClick()
    {
        //Debug.Log("button clicked");

    }
    public virtual void OnButtonHeldDown()
    {
        //Debug.Log("button held down");

    }
    public virtual void OnButtonReleased()
    {
        //Debug.Log("button released");
    }
    public void startCoolDown(float _abilityCoolDown)
    {
        //Debug.Log(this.abilityCoolDown);
        if (_abilityCoolDown <= 0)
        {
            return;
        }
        //sends a signal to the characterAbilityScript
        parentScriptRef.AbilitySetCoolDown(this,_abilityCoolDown);
        
        onCoolDown = true;
        Invoke("endCoolDown",_abilityCoolDown);
    }
    private void endCoolDown()
    {
        onCoolDown = false;
    }
    public bool getCoolDownStatus()
    {
        return this.onCoolDown;
    }

}
//this is an example of a testAbility that just has a different print statement
public class testAbility : BaseAbilityScript
{
    
    public override void OnButtonReleased()
    {
        Debug.Log("I am mr test fear me");
        
    }
}
public class anotherAbility : BaseAbilityScript
{
    
    public override void OnButtonReleased()
    {
        Debug.Log("ABCDEFG");
        
    }
}
public class lastTestAbility : BaseAbilityScript
{
    
    public override void OnButtonReleased()
    {
        Debug.Log("replace me with something useful");
        
    }
}


