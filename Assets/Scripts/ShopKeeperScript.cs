using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DialogueEditor;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ShopKeeperScript : MonoBehaviour
{
    public NPCConversation myConversation;
    float interactRange = 4.0f;
    bool inRange = false;
    GameObject player;
    CharacterAbilityScript playerAbilityManager;

    [SerializeField]
    Canvas shopUI;
    [SerializeField]
    TextMeshProUGUI  playMoneyText;
    private bool used = false;
    [SerializeField]
    private UnityEvent onShopOpen;

    private PlayerInput playerInput;
  
    
    void Awake()
    {
        playerInput = new PlayerInput();
    }
    void Start()
    {
        

        playerInput.Input.Hit.performed += hitInput;

        player = GameObject.FindGameObjectWithTag("Player");
        //shopUI.enabled = false;
        playerAbilityManager = player.GetComponent<CharacterAbilityScript>();
        
    }



    private void hitInput(InputAction.CallbackContext context)
    {
        print("look at me look at me");
        var di = Vector3.Distance(transform.position,player.transform.position);
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            var distanceToEnemy = Vector3.Distance(gameObject.transform.position,enemies.transform.position);
            if (distanceToEnemy < 40)
            {
                return;
            }
        }
        if (di <= interactRange && used == false)
        {
            initiateShop();
            used = true;
        }
        
    }
    
    
    void Update()
    {
        if (used) this.gameObject.SetActive(false);
        playMoneyText.text = "Player Money: " + playerAbilityManager.playerMoney;
        var di = Vector3.Distance(transform.position,player.transform.position);

        // Check the player is in Range
        if (di <= interactRange && inRange == false) // just entered range
        {
            inRange = true;
        }
        else if (di <= interactRange)
        {
            inRange = true;
        }
        else if (di > interactRange)
        {
            inRange = false;
        }

        if (inRange == true)
        {
            if (used == true)
            {
                return;
            }
            //no enemies allowed to be around
            foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                var distanceToEnemy = Vector3.Distance(gameObject.transform.position,enemies.transform.position);
                if (distanceToEnemy < 40)
                {
                    return;
                }
            }
            initiateShop();
            used = true;
        }
    }
    


    public void pause(bool pause)
    {
        Time.timeScale = pause ? 1 : 0;
    }

    public void initiateShop()
    {
        print("initiate shop");
        onShopOpen.Invoke();
        // if(!ConversationManager.Instance.IsConversationActive)
        // {
        //     ConversationManager.Instance.StartConversation(myConversation);
        // }
    }
}
