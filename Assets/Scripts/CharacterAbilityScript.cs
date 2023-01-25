using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilityScript : MonoBehaviour
{
    //contains references to the currently chosen abilities
    List<BaseAbilityScript> playerAbilities = new List<BaseAbilityScript> {null,null,null};


    void Start()
    {
        equipAbility("testAbility",0);
        removeAbility(0);
    }
    void Update()
    {
        getInput();
    }

    void getInput()
    {
        
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
            default:
                break;
        }
        
        playerAbilities[_index].OnEquip();


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
