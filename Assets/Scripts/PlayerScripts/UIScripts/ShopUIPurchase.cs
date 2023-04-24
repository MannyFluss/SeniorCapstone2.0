using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ShopUIPurchase : MonoBehaviour
{

    private PlayerInput playerInput;
    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    //needs atleast one button in the list or else crash
    [SerializeField]
    private Button[] buttons;

    private Button selectedButton;
    void Start()
    {
        
        //Player input listeners for vectors
        playerInput.Input.Move.started += shopSelection;
        playerInput.Input.Move.performed += shopSelection;
        playerInput.Input.Move.canceled += shopSelection;

        //Player input listeners for buttons
        playerInput.Input.Jump.started += confirmPurchase;
        playerInput.Input.AbilityMenu.started += leaveShop;
        
        

        
    }


    private void shopSelection(InputAction.CallbackContext context)
    {
        
    }
    private void confirmPurchase(InputAction.CallbackContext context)
    {
        
    }
    private void leaveShop(InputAction.CallbackContext context)
    {
        
    }




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
    [SerializeField]
    Image Ability1Preview, Ability2Preview, ShopPreviewIcon1, ShopPreviewIcon2; 

    [SerializeField]
    TextMeshProUGUI PlayerAbility1Description, PlayerAbility2Description, PlayerAbility1Name, PlayerAbility2Name, ShopAbilityName; 
    [SerializeField]
    Sprite PlayerUnselectedSprite, PlayerSelectedSprite;
    [SerializeField]
    Image PlayerAbility1Inventory,PlayerAbility2Inventory;
    

    // Start is called before the first frame update
    void OnEnable()
    {
        initialSet();
        deselectMarkers();
        setTextAndIcons();
        selectedButton = buttons[0];
        selectedButton.Select();
        //find event system and set first selected
        //GameObject eventSystem = GameObject.Find("EventSystem");

        //eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(selectedButton.gameObject);
    }

    public void deselectMarkers()
    {
        playerMarker.transform.position = new Vector3(99999,99999,99999);
        shopMarker.transform.position = new Vector3(99999,99999,99999);
        playerMarkerPosition = -1;
        shopMarkerPosition = -1;

        PlayerAbility1Inventory.sprite = PlayerUnselectedSprite;
        PlayerAbility2Inventory.sprite = PlayerUnselectedSprite;
    }
    public void setShopOptions(string _input)
    {
        
        AbilityOffer = _input;
    }
    void purchase()
    {
        shopMarkerPosition = 4;
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
        playerAbilities.removeAbility(playerMarkerPosition);
        playerAbilities.AbilityPickUpInteract(shopTriplets[shopMarkerPosition].ability);
        descriptionText.text = shopTriplets[shopMarkerPosition].description;
        shopTriplets[shopMarkerPosition].ability = "purchased";
        shopTriplets[shopMarkerPosition].description = "purchased";
        deselectMarkers();
        setTextAndIcons();
        gameObject.SetActive(false);

    }
    void setTextAndIcons()
    {
        shopTriplets[0] = new triplet(playerAbilities.getAbilityName(0));
        shopTriplets[1] = new triplet(playerAbilities.getAbilityName(1));
        shopTriplets[2] = new triplet(playerAbilities.getAbilityName(2));
        //
        shopAbility1Text.text = shopTriplets[3].description;
        shopAbility2Text.text = shopTriplets[4].description;
        
        Ability1Preview.sprite = Global.Instance.getIconTexture(shopTriplets[0].ability);
        Ability2Preview.sprite = Global.Instance.getIconTexture(shopTriplets[1].ability);

        ShopPreviewIcon1.sprite = Global.Instance.getIconTexture(shopTriplets[3].ability);
        ShopPreviewIcon2.sprite = Global.Instance.getIconTexture(shopTriplets[4].ability);

        // new shop text set
        Ability1Preview.sprite = Global.Instance.getIconTexture(playerAbilities.getAbilityName(0));
        Ability2Preview.sprite = Global.Instance.getIconTexture(playerAbilities.getAbilityName(1));

        PlayerAbility1Description.text = BaseAbilityScript.AbilityDescriptions[playerAbilities.getAbilityName(0)];
        PlayerAbility2Description.text = BaseAbilityScript.AbilityDescriptions[playerAbilities.getAbilityName(1)];

        ShopAbilityName.text = shopTriplets[4].ability;
        PlayerAbility1Name.text = playerAbilities.getAbilityName(0);
        PlayerAbility2Name.text = playerAbilities.getAbilityName(1);
    




    }
    
    public void setAbilityOffer(string _set)
    {

        AbilityOffer = _set;
    }

    public void setRandomAbilityOffer()
    {
        var currAbility1 = playerAbilities.getAbilityName(0);
        var currAbility2 = playerAbilities.getAbilityName(1);
        var temp = "";
        while(true)
        {
            temp = BaseAbilityScript.AbilitiesList[Random.Range(0, BaseAbilityScript.AbilitiesList.GetLength(0))];
            if(temp != currAbility1 && temp != currAbility2)
            {
                break;
            }
        }
        AbilityOffer = temp;
    }

    [SerializeField]
    string AbilityOffer;
    void initialSet()
    {
        shopTriplets[0] = new triplet(playerAbilities.getAbilityName(0));
        shopTriplets[1] = new triplet(playerAbilities.getAbilityName(1));
        shopTriplets[2] = new triplet(playerAbilities.getAbilityName(2));

        shopTriplets[3] = new triplet("NavalMine");
        shopTriplets[4] = new triplet(AbilityOffer);
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

        if (_clickedObject.name == "ImageBackground1")
        {
            //set image
            shopMarker.transform.position = _clickedObject.transform.position;
            shopMarkerPosition = 3;
            purchase();
            return;
        }
        if (_clickedObject.name == "ImageBackground2")
        {
            //set image
            shopMarker.transform.position = _clickedObject.transform.position;
            shopMarkerPosition = 4;
            purchase();
            return;
        }
        //not player


    }
}

