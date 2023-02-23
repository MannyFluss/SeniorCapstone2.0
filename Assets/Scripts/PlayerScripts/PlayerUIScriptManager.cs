using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerUIScriptManager : MonoBehaviour
{
    [SerializeField]
    CharacterAbilityScript characterAbilities;

    // Start is called before the first frame update
    [Header("Abilities")]
    [SerializeField]
    GameObject[] Abilities;
/////
//this section will have all the shopUI and things associated with it
    [SerializeField]
    GameObject ShopUIPurchase;
    string[] AbilitiesCurrentlyEquipped = new string[]{null,null,null};
    [SerializeField]
    TextMeshProUGUI PurchaseTextRef;


///// this section will be the upgradeUI
    [SerializeField]
    GameObject ShopUIUpgrade;

    public void setAbilityCoolDown(int _index, float _coolDown)
    {
        
        Abilities[_index].GetComponent<Image>().color = new Color(0.0f,0.0f,0.0f);
        //LeanTween.color(Abilities[_index],new Color(0f,0f,0f),0f);
        LeanTween.color(Abilities[_index].GetComponent<Image>().rectTransform,new Color(1f,1f,1f),_coolDown);

    }
    public void setUIElement(GameObject obj, bool setTo)
    {
        obj.SetActive(setTo);
    }

    void Update()
    {
        ShopPurchaseInitialize();
    }
    public void ShopPurchaseInitialize()
    {
        AbilitiesCurrentlyEquipped[0] = characterAbilities.getAbilityName(0);
        AbilitiesCurrentlyEquipped[1] = characterAbilities.getAbilityName(1);
        AbilitiesCurrentlyEquipped[2] = characterAbilities.getAbilityName(3);
    }

    public void ShopPreviewText(int index)
    {
        PurchaseTextRef.text = AbilitiesCurrentlyEquipped[index];
    }

}
