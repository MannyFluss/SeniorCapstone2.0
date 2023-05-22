using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Credit Screen Variables")]
    public GameObject creditScreen;
    public bool creditState = false;

    public void CreditToggle()
    {
        creditState = !creditState;
        creditScreen.SetActive(creditState);
    }
}
