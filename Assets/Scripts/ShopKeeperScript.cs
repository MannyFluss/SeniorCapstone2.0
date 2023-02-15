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
    
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //shopUI.enabled = false;
        playerAbilityManager = player.GetComponent<CharacterAbilityScript>();
        ConversationManager.Instance.StartConversation(myConversation);
    }

    // Update is called once per frame
    void Update()
    {
        playMoneyText.text = "Player Money: " + playerAbilityManager.playerMoney;
        
        var di = Vector3.Distance(transform.position,player.transform.position);
        if (di <= interactRange && inRange == false)
        {
            inRange = true;
            Debug.Log(myConversation);

            
        }else if (di <= interactRange)
        {
            inRange = true;
        }else if (di > interactRange)
        {
            inRange = false;
        }
    }

    public void initiateShop()
    {
        Debug.Log("this is where the shop will start doing shop things");
    }
}
