using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is the base class, it should not be used
public  class BaseAbilityScript : MonoBehaviour
{
    
    public GameObject myParent;
    public CharacterAbilityScript parentScriptRef;

    protected float abilityCoolDown;
    private bool onCoolDown = false;
    
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


