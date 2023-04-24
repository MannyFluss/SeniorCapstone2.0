using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TicTicBoomManager : MonoBehaviour
{
    //general fields
    private bool puzzleActive;

    //valve fields
    private ValveManager vm;

    //countdown fields
    private TMP_Text countdown;
    private float timer;

    [SerializeField]
    GameObject screen;

    //prototype vals
    MeshRenderer m;
    [SerializeField]
    Material red;
    [SerializeField]
    Material green;

    void Start()
    {
        vm = GetComponentInChildren<ValveManager>();
        countdown = GetComponentInChildren<TMP_Text>();
        m = screen.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (puzzleActive)
        {
            countdown.text = ((int)timer).ToString();
        }
        if(timer < 0)
        {
            TicTicBoomFailed();
        }
        
    }

    public void TicTicBoom(int health)
    {
        puzzleActive = true;
        m.material = red;
        vm.startValves();
        if(health > 50)
        {
            timer = 24f;
        }
        else
        {
            timer = 19f;
        }
    }

    public void TicTicBoomSolved()
    {
        puzzleActive = false;
        vm.isComplete = false;
        countdown.text = "%^&*))#!";
        m.material = green;
    }

    public void TicTicBoomFailed()
    {
        puzzleActive = false;
        vm.isComplete = false;
        countdown.text = "BOOM";
    }
}
