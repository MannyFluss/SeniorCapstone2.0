using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


//this script will be added to the main character and will manage the characters abilities;
//likely will need to link to other scripts that access hp, stats, and other things later on
//this script will now manage player money as well
public class CharacterAbilityScript : MonoBehaviour
{

    public int playerMoney = 100;


    // if you need prefabs for the abilities they go here
    [SerializeField]
    public GameObject _AbilityExplosionPrefab;

    [SerializeField]
    public GameObject _AbilityBoxPrefab;
    [SerializeField]
    public GameObject _AbilityExplosiveFish;

    [SerializeField]
    public PlayerUIScriptManager UIScript;
    //contains references to the currently chosen abilities
    List<BaseAbilityScript> playerAbilities = new List<BaseAbilityScript> {null,null,null};

    //Player Controls
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }



    void Start()
    {

        
 
        //Player input listeners for skills
        playerInput.Input.Skill1.performed += skillOnePressOrHold;
        playerInput.Input.Skill1.canceled += skillOneReleased; 
        
        playerInput.Input.Skill2.performed += skillTwoPressOrHold;
        playerInput.Input.Skill2.canceled += skillTwoReleased;
        
        playerInput.Input.Skill3.performed += skillThreePressOrHold;
        playerInput.Input.Skill3.canceled += skillThreeReleased;
    }

    //automatically safely adds the new ability to the list, for interactables
    public bool AbilityPickUpInteract(string abilityName)
    {
        bool abilityAdded = false;
        for (int i=0; i<playerAbilities.Count;i++ )
        {
            if (playerAbilities[i]!=null)
            {
                continue;
            }
            abilityAdded = true;
            equipAbility(abilityName,i);
            break;
        }
        return abilityAdded;
    }
    public void skillOnePressOrHold(InputAction.CallbackContext context)
    {
        
        if (context.interaction is TapInteraction)
        {
            Debug.Log("Tap0");
            if (playerAbilities[0] != null)
            {
                playerAbilities[0].OnButtonClick();
            }
        }
        else if(context.interaction is HoldInteraction)
        {
            Debug.Log("Held0");
            if (playerAbilities[0] != null)
            {
                playerAbilities[0].OnButtonHeldDown();
            }
        }
        
    }
    public void skillOneReleased(InputAction.CallbackContext context)
    {
        if (playerAbilities[0] != null)
        {
            playerAbilities[0].OnButtonReleased();
        }
    }
    public void skillTwoPressOrHold(InputAction.CallbackContext context)
    {
        if (context.interaction is TapInteraction)
        {
            Debug.Log("Tap1");
            if (playerAbilities[1] != null)
            {
                playerAbilities[1].OnButtonClick();
            }
        }
        else if (context.interaction is HoldInteraction)
        {
            Debug.Log("Held1");
            if (playerAbilities[1] != null)
            {
                playerAbilities[1].OnButtonHeldDown();
            }
        }
    }
    public void skillTwoReleased(InputAction.CallbackContext context)
    {
        if (playerAbilities[1] != null)
        {
            playerAbilities[1].OnButtonReleased();
        }
    }
    public void skillThreePressOrHold(InputAction.CallbackContext context)
    {
        if (context.interaction is TapInteraction)
        {
            Debug.Log("Tap2");
            if (playerAbilities[2] != null)
            {
                playerAbilities[2].OnButtonClick();
            }
        }
        else if (context.interaction is HoldInteraction)
        {
            Debug.Log("Held2");
            if (playerAbilities[2] != null)
            {
                playerAbilities[2].OnButtonHeldDown();
            }
        }
    }
    public void skillThreeReleased(InputAction.CallbackContext context)
    {
        if (playerAbilities[2] != null)
        {
            playerAbilities[2].OnButtonReleased();
        }
    }
    void Update()
    {
        //getInput();
    }

    public BaseAbilityScript getPlayerAbility(int _index)
    {
        if (_index >= playerAbilities.Count || _index < 0)
        {
            return null;
        }
        return playerAbilities[_index];
    }
    //hard coded inputs
    void getInput()
    {
        //input 1
        if(Input.GetMouseButtonDown(0))
        {
            if (playerAbilities[0]!=null)
            {
                playerAbilities[0].OnButtonClick();
            }
        }
        if(Input.GetMouseButton(0))
        {
            if (playerAbilities[0]!=null)
            {
                playerAbilities[0].OnButtonHeldDown();
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (playerAbilities[0]!=null)
            {
                playerAbilities[0].OnButtonReleased();
            }
        }
        // input 2
        if(Input.GetMouseButtonDown(1))
        {
            if (playerAbilities[1]!=null)
            {
                playerAbilities[1].OnButtonClick();
            }
        }
        if(Input.GetMouseButton(1))
        {
            if (playerAbilities[1]!=null)
            {
                playerAbilities[1].OnButtonHeldDown();
            }
        }
        if(Input.GetMouseButtonUp(1))
        {
            if (playerAbilities[1]!=null)
            {
                playerAbilities[1].OnButtonReleased();
            }
        }
        //input 3
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (playerAbilities[2]!=null)
            {
                playerAbilities[2].OnButtonClick();
            }
        }
        if(Input.GetKey(KeyCode.E))
        {
            if (playerAbilities[2]!=null)
            {
                playerAbilities[2].OnButtonHeldDown();
            }
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            if (playerAbilities[2]!=null)
            {
                playerAbilities[2].OnButtonReleased();
            }
        }
    }



    public void swapAbility(int _index1, int _index2)
    {
        //invalid indx
        if (_index1 >= playerAbilities.Count || _index2 >= playerAbilities.Count )
        {
            return;
        }
        BaseAbilityScript temp = playerAbilities[_index1];
        playerAbilities[_index1] = playerAbilities[_index2];
        playerAbilities[_index2] = temp;
    }

    void equipAbility(string _toInsert, int _index)
    {
        if (_index >= playerAbilities.Count)
        {
            return;
        }
        if (playerAbilities[_index]!=null)
        {
            removeAbility(_index);
        }
        //this will have to be modified whenever a new ability script is made
        switch(_toInsert)
        {
            case "testAbility":
                playerAbilities[_index] = gameObject.AddComponent<testAbility>();
                break;
            case "anotherAbility":
                playerAbilities[_index] = gameObject.AddComponent<anotherAbility>();
                break;
            case "ClawsOff":
                playerAbilities[_index] = gameObject.AddComponent<ClawsOff>();
                break;
            case "SchrodingerBox":
                playerAbilities[_index] = gameObject.AddComponent<SchrodingerBox>();
                break;
            case "ExplosiveFishAbility":
                playerAbilities[_index] = gameObject.AddComponent<ExplosiveFishAbility>();
                break;
            default:
                break;
        }
        
       
        playerAbilities[_index].myParent = this.gameObject;
        playerAbilities[_index].OnEquip();
       // Debug.Log("this is my parent " + playerAbilities[_index].myParent );


    }
    void removeAbility(int _index)
    {

        if (_index >= playerAbilities.Count)
        {
            return;
        }
        if (playerAbilities[_index]!=null)
        {   

                   
            //fire signal on item and remove from array
            playerAbilities[_index].OnDrop();
            Destroy(playerAbilities[_index]);  
            playerAbilities[_index] = null;
        }
    }

    public void AbilitySetCoolDown(BaseAbilityScript _ability, float _coolDown)
    {

       
        int index = playerAbilities.FindIndex(a => a == _ability);
        UIScript.setAbilityCoolDown(index,_coolDown);
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }



}
