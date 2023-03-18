using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DialogueEditor;

public class ShopKeeperScript : MonoBehaviour
{
    public NPCConversation myConversation;
    float interactRange = 5.0f;
    bool inRange = false;
    GameObject player;
    CharacterAbilityScript playerAbilityManager;

    [SerializeField]
    Canvas shopUI;
    [SerializeField]
    TextMeshProUGUI  playMoneyText;
    private bool used = false;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //shopUI.enabled = false;
        playerAbilityManager = player.GetComponent<CharacterAbilityScript>();
    }

    void Update()
    {
        playMoneyText.text = "Player Money: " + playerAbilityManager.playerMoney;
        var di = Vector3.Distance(transform.position,player.transform.position);
        if (di <= interactRange && inRange == false) // just entered range
        {
            inRange = true;
            


            
        }else if (di <= interactRange)
        {
            inRange = true;
        }else if (di > interactRange)
        {
            inRange = false;
        }
        if (inRange == true && Input.GetKeyDown(KeyCode.F))
        {
            if (used == true)
            {
                return;
            }
            initiateShop();
            used = false;
        }
    }

    public void initiateShop()
    {
        if(!ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
}
