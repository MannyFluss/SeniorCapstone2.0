using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Image BlackScreen;
    [SerializeField] private TMP_Text ContinueTMP;
    [SerializeField] private TMP_Text GameOverTMP;

    private float fadeRate = 1.2f;
    private bool stage1;
    private bool stage2;
    private bool special;
    private Color GOcolor;

    // Start is called before the first frame update
    void Start()
    {
        BlackScreen.enabled = true;

        GOcolor = GameOverTMP.color;
        GameOverTMP.color = new Color(GOcolor.r, GOcolor.b, GOcolor.g, 0);
        GameOverTMP.enabled = false;

        ContinueTMP.color = new Color(1, 1, 1, 0);
        ContinueTMP.enabled = false;

        stage1 = false;
        stage2 = false;
        special = true;

        StartCoroutine(FadeBlackScreen());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && stage1 && stage2)
        {
            special = false;
            StartCoroutine(SceneTransition());
        }
    }

    IEnumerator FadeBlackScreen()
    {
        yield return new WaitForSeconds(1f);
        for (float alpha = 1f; alpha >= 0; alpha -= fadeRate * Time.deltaTime)
        {
            BlackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        BlackScreen.color = new Color(0, 0, 0, 0);
        BlackScreen.enabled = false;
        stage1 = true;

        StartCoroutine(GameOverText());
        yield return null;
    }

    IEnumerator GameOverText()
    {
        yield return new WaitForSeconds(1f);
        GameOverTMP.enabled = true;

        for (float alpha = 0f; alpha <= 1f; alpha += fadeRate * 1.25f * Time.deltaTime)
        {
            GameOverTMP.color = new Color(GOcolor.r, GOcolor.b, GOcolor.g, alpha);
            yield return null;
        }
        GameOverTMP.color = new Color(GOcolor.r, GOcolor.b, GOcolor.g, 1);

        yield return null;
        
        StartCoroutine(ContinueText());
    }

    IEnumerator ContinueEffect()
    {
        while (special)
        {
            for (float alpha = 1f; alpha >= 0.15f; alpha -= fadeRate * Time.deltaTime)
            {
                ContinueTMP.color = new Color(1, 1, 1, alpha);
                yield return null;
            }
            for (float alpha = 0.15f; alpha <= 1f; alpha += fadeRate * Time.deltaTime)
            {
                ContinueTMP.color = new Color(1, 1, 1, alpha);
                yield return null;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator ContinueText()
    {
        yield return new WaitForSeconds(0.5f);
        ContinueTMP.enabled = true;

        for (float alpha = 0f; alpha <= 1f; alpha += fadeRate * 2f * Time.deltaTime)
        {
            ContinueTMP.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        ContinueTMP.color = new Color(1, 1, 1, 1);

        yield return null;
        StartCoroutine(ContinueEffect());

        stage2 = true;
    }

    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(0.5f);
        BlackScreen.enabled = true;
        for (float alpha = 0f; alpha <= 1; alpha += fadeRate * 2.5f * Time.deltaTime)
        {
            BlackScreen.color = new Color(0, 0, 0, alpha);
            GameOverTMP.color = new Color(GOcolor.r, GOcolor.b, GOcolor.g , 1 - alpha);
            ContinueTMP.color = new Color(1, 1, 1, 1 - alpha);
            yield return null;
        }
        BlackScreen.color = new Color(0, 0, 0, 1);
        GameOverTMP.enabled = false;
        ContinueTMP.enabled = false;

        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene("The Lab");
        yield return null;
    }
}
