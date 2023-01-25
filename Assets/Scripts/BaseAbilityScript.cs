using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is the base class, it should not be used
public  class BaseAbilityScript : MonoBehaviour
{
    
    public GameObject myParent;
    
    // signal functions for each of the child classes
    public virtual void OnEquip()
    {
        Debug.Log("equipped");
    }
    public virtual void OnDrop()
    {
        Debug.Log("dropped");
    }
    public virtual void OnButtonClick()
    {
        Debug.Log("button clicked");

    }
    public virtual void OnButtonHeldDown()
    {
        Debug.Log("button held down");

    }
    public virtual void OnButtonReleased()
    {
        Debug.Log("button released");
    }

}
//this is an example of a testAbility that just has a different print statement
public class testAbility : BaseAbilityScript
{
    
    public override void OnEquip()
    {
        base.OnEquip();
        Debug.Log("I am mr test fear me");
        
    }
}

