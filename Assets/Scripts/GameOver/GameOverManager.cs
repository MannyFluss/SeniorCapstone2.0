using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Image BlackScreen;
    [SerializeField] private TMP_Text ContinueText;

    private float fadeRate = 1.2f;
    private bool stage1;
    private bool stage2;

    // Start is called before the first frame update
    void Start()
    {
        BlackScreen.enabled = true;
        ContinueText.color = new Color(1, 1, 1, 0);
        ContinueText.enabled = false;
        stage1 = false;
        stage2 = false;

        StartCoroutine(FadeBlackScreen());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && stage1 && stage2)
        {
            StartCoroutine(SceneTransition());
        }
    }

    IEnumerator FadeBlackScreen()
    {
        yield return new WaitForSeconds(1f);
        for (float alpha = 1f; alpha >= 0; alpha -= fadeRate * Time.deltaTime)
        {
            BlackScreen.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        BlackScreen.color = new Color(1, 1, 1, 0);
        BlackScreen.enabled = false;
        stage1 = true;

        StartCoroutine(FadeInText());
        yield return null;
    }

    IEnumerator FadeInText()
    {
        yield return new WaitForSeconds(1f);
        ContinueText.enabled = true;

        for (float alpha = 0f; alpha <= 1f; alpha += fadeRate * Time.deltaTime)
        {
            ContinueText.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        ContinueText.color = new Color(1, 1, 1, 1);

        stage2 = true;

        yield return null;
    }

    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(0.5f);
        BlackScreen.enabled = true;
        for (float alpha = 0f; alpha <= 1; alpha += fadeRate * Time.deltaTime)
        {
            BlackScreen.color = new Color(1, 1, 1, alpha);
            ContinueText.color = new Color(1, 1, 1, 1 - alpha);
            yield return null;
        }
        BlackScreen.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("The Lab");
        yield return null;
    }
}
