using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopKeeperScript : MonoBehaviour
{

    float interactRange = 5.0f;
    GameObject player;
    CharacterAbilityScript playerAbilityManager;

    [SerializeField]
    Canvas shopUI;
    [SerializeField]
    TextMeshProUGUI  playMoneyText;
    
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shopUI.enabled = false;
        playerAbilityManager = player.GetComponent<CharacterAbilityScript>();
    }

    // Update is called once per frame
    void Update()
    {
        playMoneyText.text = "Player Money: " + playerAbilityManager.playerMoney;
        
        var di = Vector3.Distance(transform.position,player.transform.position);
        Debug.Log(di);
        if (di <= interactRange)
        {
            shopUI.enabled = true;
        }
        else
        {
            shopUI.enabled = false;
        }
    }
}
