using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject heartContainerParent;

    [SerializeField]
    Image Ability1, Ability2;
    [SerializeField]
    Sprite FullHeart,EmptyHeart;
    [SerializeField]
    CharacterAbilityScript AbilityScriptRef;
    [SerializeField]
    Sprite NoAbilitySprite,ClawsOffSprite,SchrodingerBoxSprite,ExplosiveFishBarrellSprite;
    [SerializeField]
    RectTransform Ability1Cooldown, Ability2Cooldown;

    void Start()
    {
        setUIHearts(9);
        setAbilityIconCoolDown(0,0);
        setAbilityIconCoolDown(1,0);
        setAbilityIcons();
    }
    public void setUIHearts(int count)
    {
        
        var children = heartContainerParent.GetComponentsInChildren<Image>();

        for (int i=0;i<children.Length;i++)
        {
            if (count > 0)
            {
                children[i].sprite = FullHeart;
            }
            else
            {
                children[i].sprite = EmptyHeart;
            }
            count -=1;
        }
    }
    public void setAbilityIcons()
    {
        string[] currAbilities = new string[] {AbilityScriptRef.getAbilityName(0),AbilityScriptRef.getAbilityName(1)};
        Ability1.sprite = Global.Instance.getIconTexture(currAbilities[0]);
        Ability2.sprite = Global.Instance.getIconTexture(currAbilities[1]);
        
        // Debug.Log(currAbilities);
        // for(var i=0;i<2;i++)
        // {
        //     Image curr;
        //     if (i==0)
        //     {
        //         curr=Ability1;
        //     }else
        //     {
        //         curr=Ability2;
        //     }
        //     switch(currAbilities[i])
        //     {
        //         case ("ClawsOff"):
        //             curr.sprite = ClawsOffSprite;
        //             break;
        //         case ("SchrodingerBox"):
        //             curr.sprite = SchrodingerBoxSprite;
        //             break;
        //         case ("ExplosiveFishAbility"):
        //             curr.sprite = ExplosiveFishBarrellSprite;
        //             break;
        //         case ("empty"):
        //             curr.sprite = NoAbilitySprite;
        //             break;
        //         case null:
        //             break;
                
        //     }
            
        // }
    }
    public void setAbilityIconCoolDown(int index, float time)
    {
        
        if (index == 0)
        {
            Ability1Cooldown.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,150);
            LeanTween.size(Ability1Cooldown,new Vector2(75,0),time);

        }
        if (index == 1)
        {
            Ability2Cooldown.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,150);
            LeanTween.size(Ability2Cooldown,new Vector2(75,0),time);

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
