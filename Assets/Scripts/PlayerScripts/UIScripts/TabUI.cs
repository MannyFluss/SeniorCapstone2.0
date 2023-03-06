using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabUI : MonoBehaviour
{
    [SerializeField]
    CharacterAbilityScript playerAbilityScriptReference;
    [SerializeField]
    Sprite ClawsOffSprite, SchrodingerBoxSprite, ExplosiveFishBarrellSprite,NoAbilitySprite;
    [SerializeField]
    Image Ability1,Ability2;
    [SerializeField]
    TextMeshProUGUI LeftAbilityName, LeftAbilityDescription, RightAbilityName,RightAbilityDescription;


    void Start()
    {
        Debug.Log("result of load");

    }
    void OnEnable()
    {
        updateTextAndIcons();
    }
    public void updateTextAndIcons()
    {
        string[] currAbilities = new string[] {playerAbilityScriptReference.getAbilityName(0),playerAbilityScriptReference.getAbilityName(1)};
        for(var i=0;i<2;i++)
        {
            Image curr;
            if (i==0)
            {
                curr=Ability1;
            }else
            {
                curr=Ability2;
            }
            // switch(currAbilities[i])
            // {
            //     case ("ClawsOff"):
            //         curr.sprite = ClawsOffSprite;
            //         break;
            //     case ("SchrodingerBox"):
                   
            //         //curr.sprite = Global.GetAbilityIcons()["SchrodingerBox"];
            //         break;
            //     case ("ExplosiveFishAbility"):
            //         curr.sprite = ExplosiveFishBarrellSprite;
            //         break;
            //     case ("empty"):
            //         curr.sprite = NoAbilitySprite;
            //         break;
            //     case null:
            //         break;
                
            // }
            
        }
        Ability1.sprite = Global.Instance.getIconTexture("SchrodingerBox");
        LeftAbilityName.text = currAbilities[0];
        RightAbilityName.text = currAbilities[1];
        LeftAbilityDescription.text = BaseAbilityScript.AbilityDescriptions[currAbilities[0]];
        RightAbilityDescription.text = BaseAbilityScript.AbilityDescriptions[currAbilities[1]];

    }
}
