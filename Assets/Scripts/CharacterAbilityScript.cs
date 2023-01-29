using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script will be added to the main character and will manage the characters abilities;
//likely will need to link to other scripts that access hp, stats, and other things later on
public class CharacterAbilityScript : MonoBehaviour
{
    //contains references to the currently chosen abilities
    List<BaseAbilityScript> playerAbilities = new List<BaseAbilityScript> {null,null,null};




    void Start()
    {
        equipAbility("testAbility",1);
        equipAbility("anotherAbility",0);
    }
    void Update()
    {
        getInput();
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
            default:
                break;
        }
        
        playerAbilities[_index].OnEquip();
        playerAbilities[_index].myParent = this.gameObject;


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
    



}
