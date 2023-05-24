using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithObject : MonoBehaviour
{
    [SerializeField] private Image rend;
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text messageBox;
    [SerializeField] private GameObject targetItem;
    [SerializeField][ReadOnlyInspector] private bool reading;
    [SerializeField] private float fadeRate = 0.7f;

    void Start()
    {
        SetColor(0);
        rend.enabled = false;
        background.enabled = false;
        messageBox.enabled = false;
        reading = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && reading)
        {
            StartCoroutine(ClosePoster());
            targetItem.GetComponent<ReadPoster>().Interactable = true;
        }
    }

    private void SetColor(float i)
    {
        rend.color = new Color(1, 1, 1, i);
        background.color = new Color(1, 1, 1, i);
        messageBox.color = new Color(1, 1, 1, i);
    }

    public IEnumerator ShowPoster(Sprite input, string text)
    {
        rend.sprite = input;
        messageBox.text = text;
        rend.enabled = true;
        background.enabled = true;
        messageBox.enabled = true;

        for (float alpha = 0f; alpha <= 1f; alpha += fadeRate * Time.deltaTime)
        {
            SetColor(alpha);
            yield return null;
        }
        SetColor(1f);
        reading = true;
        yield return null;
    }

    IEnumerator ClosePoster()
    {
        for (float alpha = 1f; alpha >= 0f; alpha -= fadeRate * Time.deltaTime)
        {
            SetColor(alpha);
            yield return null;
        }
        SetColor(0f);
        
        rend.enabled = false;
        background.enabled = false;
        messageBox.enabled = false;
        reading = false;
        yield return null;
    }
}
