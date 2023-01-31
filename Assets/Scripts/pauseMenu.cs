using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    CharacterAbilityScript AbilityReference;
    [SerializeField] 
    
    TextMeshProUGUI textRow1, textRow2, textRow3;
    void Start()
    {
        
    }

    private void Update()
    {
        textRow1.text = "none";
        if (AbilityReference.getPlayerAbility(0) != null)
        {
            textRow1.text = AbilityReference.getPlayerAbility(0).GetType().ToString();
        }
        textRow2.text = "none";
        if (AbilityReference.getPlayerAbility(1))
        {
            textRow2.text = AbilityReference.getPlayerAbility(1).GetType().ToString();

        }
        textRow3.text = "none";
        if (AbilityReference.getPlayerAbility(2) != null)
        {
            textRow3.text = AbilityReference.getPlayerAbility(2).GetType().ToString();
        }
    }

    public void menuSwap(int _index)
    {
        AbilityReference.swapAbility(_index, (_index+1)%3);
    }
}
