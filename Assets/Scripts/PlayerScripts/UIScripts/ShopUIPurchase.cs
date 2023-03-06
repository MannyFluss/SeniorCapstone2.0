using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ShopUIPurchase : MonoBehaviour
{

    struct triplet
    {
        public triplet(string _ability)
        {
            ability = _ability;
            if(BaseAbilityScript.AbilityDescriptions.ContainsKey(_ability))
            {
                description = BaseAbilityScript.AbilityDescriptions[_ability];   
            }
            else
            {
                description = "No ability currently equipped in selected slot";
            }
        }
        public string ability;
        public string description;
        //to add image
    };
    //triplets 0-2 are player abilities, 4-5 are the shop availability
    [SerializeField]
    CharacterAbilityScript playerAbilities;
    triplet[] shopTriplets = new triplet[5];

    [SerializeField]
    TextMeshProUGUI descriptionText;

    string shopItem1 = BaseAbilityScript.AbilitiesList[0];
    string shopItem2 = BaseAbilityScript.AbilitiesList[1];
    [SerializeField]
    GameObject playerMarker;
    [SerializeField]
    GameObject shopMarker;
    int playerMarkerPosition = -1;
    int shopMarkerPosition = -1;

    [SerializeField]
    TextMeshProUGUI shopAbility1Text;

    [SerializeField]
    TextMeshProUGUI shopAbility2Text;

    // Start is called before the first frame update

    void OnEnable()
    {
        initialSet();
        deselectMarkers();
        setTextAndIcons();
    }

    public void deselectMarkers()
    {
        playerMarker.transform.position = new Vector3(99999,99999,99999);
        shopMarker.transform.position = new Vector3(99999,99999,99999);
        playerMarkerPosition = -1;
        shopMarkerPosition = -1;
    }
    void purchase()
    {
        if (shopMarkerPosition == -1 || playerMarkerPosition == -1 || shopTriplets[shopMarkerPosition].ability == "purchased")
        {
            return;
        }

        if (playerAbilities.AbilitiesFull())
        {
            //we swap
            // triplet[shopMarkerPosition]. 
            playerAbilities.equipAbility(shopTriplets[shopMarkerPosition].ability,playerMarkerPosition);
            descriptionText.text = shopTriplets[shopMarkerPosition].description;
            shopTriplets[shopMarkerPosition].ability = "purchased";
            shopTriplets[shopMarkerPosition].description = "purchased";
            deselectMarkers();
            setTextAndIcons();
            return;
        }
        playerAbilities.AbilityPickUpInteract(shopTriplets[shopMarkerPosition].ability);
        descriptionText.text = shopTriplets[shopMarkerPosition].description;
        shopTriplets[shopMarkerPosition].ability = "purchased";
        shopTriplets[shopMarkerPosition].description = "purchased";
        deselectMarkers();
        setTextAndIcons();

    }
    void setTextAndIcons()
    {
        shopTriplets[0] = new triplet(playerAbilities.getAbilityName(0));
        shopTriplets[1] = new triplet(playerAbilities.getAbilityName(1));
        shopTriplets[2] = new triplet(playerAbilities.getAbilityName(2));
        //
        shopAbility1Text.text = shopTriplets[3].description;
        shopAbility2Text.text = shopTriplets[4].description;

    }
    void initialSet()
    {
        shopTriplets[0] = new triplet(playerAbilities.getAbilityName(0));
        shopTriplets[1] = new triplet(playerAbilities.getAbilityName(1));
        shopTriplets[2] = new triplet(playerAbilities.getAbilityName(2));

        shopTriplets[3] = new triplet(BaseAbilityScript.AbilitiesList[0]);
        shopTriplets[4] = new triplet(BaseAbilityScript.AbilitiesList[1]);
        setTextAndIcons();
    }
    //for event trigger system
    public void onHighlight(int _index)
    {
        descriptionText.text = shopTriplets[_index].description;
    }

    //idk why but you can only have arg
    public void doThisOnClick(GameObject _clickedObject)
    {
        
        if(_clickedObject.name == "Ability1")
        {
            playerMarker.transform.position = _clickedObject.transform.position;
            playerMarkerPosition = 0;
            return;
        }
        if(_clickedObject.name == "Ability2")
        {
            playerMarker.transform.position = _clickedObject.transform.position;
            playerMarkerPosition = 1;
            return;
        }
        if(_clickedObject.name == "Ability3")
        {
            playerMarker.transform.position = _clickedObject.transform.position;
            playerMarkerPosition = 2;
            return;
        }
        if(_clickedObject.name == "AbilityImage1")
        {
            shopMarker.transform.position = _clickedObject.transform.position;
            shopMarkerPosition = 3;
            return;
        }
        if(_clickedObject.name == "AbilityImage2")
        {
            shopMarker.transform.position = _clickedObject.transform.position;
            shopMarkerPosition = 4;
            return;
        }
        //not player


    }
}

