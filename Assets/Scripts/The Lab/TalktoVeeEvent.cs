using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalktoVeeEvent : MonoBehaviour
{
    [SerializeField][ReadOnlyInspector] private bool TalkedToVee;

    private GameObject door;

    private void Start()
    {
        door = this.gameObject;
    }

    private void Update()
    {
        TalkedToVee = VeeMain.Instance.NotFirstTime;
        if (TalkedToVee) 
        {
            door.SetActive(false);
        }
    }
}
