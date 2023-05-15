using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TicTicBoomManager : MonoBehaviour
{
    //general fields
    private bool puzzleActive;
    [SerializeField]
    PlayerManager player;

    //valve fields
    private ValveManager vm;

    //countdown fields
    private TMP_Text countdown;
    private float timer;

    [SerializeField]
    private GameObject screen;

    //big bomb
    [SerializeField]
    private GameObject bombPrefab;
    private GameObject bomb;
    [SerializeField]
    private float waitTime = 3f;

    //prototype vals
    MeshRenderer m;
    [SerializeField]
    Material red;
    [SerializeField]
    Material green;

    //for DrKrabMaanger
    DrKrabManager dkm;
    public bool ticTicBoomActive = false;

    void Start()
    {
        vm = GetComponentInChildren<ValveManager>();
        countdown = GetComponentInChildren<TMP_Text>();
        m = screen.GetComponent<MeshRenderer>();
        dkm = GetComponentInParent<DrKrabManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (puzzleActive)
        {
            countdown.text = ((int)timer).ToString();
        }
        if(timer < 0 && puzzleActive)
        {
            TicTicBoomFailed();
        }
    }

    private void FixedUpdate()
    {
        //bomb movement
        if (bomb != null)
        {
            bomb.transform.position = Vector3.Lerp(bomb.transform.position, new Vector3(0, 0, 0), 0.01f);
        }
    }

    public void TicTicBoom(int health)
    {
        vm.stopValves();
        ticTicBoomActive = true;
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
        ticTicBoomActive = false;
        puzzleActive = false;
        vm.isComplete = false;
        countdown.text = "%^&*))#!";
        m.material = green;
        dkm.stunSequence();
    }

    public void TicTicBoomFailed()
    {
        ticTicBoomActive = false;
        puzzleActive = false;
        vm.isComplete = false;
        countdown.text = "BOOM";
        StartCoroutine(bigBomb());
    }

    private IEnumerator bigBomb()
    {
        bomb = Instantiate(bombPrefab, new Vector3(0, 30, 0), Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(3f);
        Destroy(bomb);
        player.takeDamage(1);
        yield return new WaitForSeconds(waitTime);
        dkm.beginMoves();
    }
}
