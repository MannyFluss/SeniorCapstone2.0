using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Runtime.ConstrainedExecution;
using UnityEngine.SceneManagement;
using System.IO;

public class CutsceneManagerV3 : MonoBehaviour
{
    [System.Serializable]
    public class Panel
    {
        public string panelSN;
        public Image panelImg;
        public Image panelTxtBg;
        public TMP_Text panelTxt;
    }

    [System.Serializable]
    public class Page
    {
        public string PageName;
        public List<Panel> panel = new List<Panel>();
    }

    private static class FadeAudioSource
    {
        public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }

    // ============================= //

    [Header("Set-Up")]
    [SerializeField] private Image blackScreen;
    [SerializeField] private Image continueArrow;
    [SerializeField] private List<Page> cutscene = new List<Page>();
    [SerializeField] private float imageFadeInRate = 0.6f;
    [SerializeField] private float textFadeInRate = 1.2f;
    [SerializeField] private float fadeOutRate = 16f;
    [SerializeField] private float nextPanel = 0.75f;
    [SerializeField] private float nextPage = 0.5f;

    [Header("Audio")]
    [SerializeField] private AudioSource CutsceneMusic;
    [SerializeField] private AudioSource CutsceneOutro;

    [Header("Scene")]
    [SerializeField] private string NextScene;

    // ==========[Private]========== //
    private bool togglePress = false;

    // ============================= //

    void Start()
    {
        // Set all the Image (if didn't) into Invisible
        foreach (Page c in cutscene)
            foreach (Panel p in c.panel)
            {
                p.panelImg.color = new Color(1, 1, 1, 0);

                // If the Panel does not have a dialogue box or text
                if (p.panelTxtBg != null)
                {
                    p.panelTxtBg.color = new Color(1, 1, 1, 0);
                    p.panelTxt.color = new Color(1, 1, 1, 0);
                }
            }

        // Make sure the Black Screen alpha is 0
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.b, blackScreen.color.g, 1);
        continueArrow.enabled = false;

        // Play the SoundTrack
        if (CutsceneMusic != null)
        {
            AudioSource audio = CutsceneMusic;
            audio.Play();
        }

        StartCutscene();
    }

    private void StartCutscene()
    {
        // Begin the Cutscene Fade In and Out
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator ExitingCutscene()
    {
        for(float alpha = 0f; alpha < 1f; alpha += imageFadeInRate * Time.deltaTime)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.b, blackScreen.color.g, alpha);
            yield return null;
        }

        // Transition Scene when the Music Finishes Playing
        yield return new WaitUntil(() => CutsceneOutro.isPlaying == false);
        SceneManager.LoadScene(NextScene);
    }

    IEnumerator FadeInAndOut()
    {
        // Enter by fading in from a black screen
        for (float alpha = 1f; alpha >= 0f; alpha -= imageFadeInRate * Time.deltaTime)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.b, blackScreen.color.g, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);

        foreach (Page c in cutscene)
        {
            togglePress = false;

            // Fade In Panel by Panel
            foreach (Panel p in c.panel)
            {
                // First Fade In the Panel's Img and Txt Bg
                for (float alpha = 0f; alpha <= 1f; alpha += imageFadeInRate * Time.deltaTime)
                {
                    p.panelImg.color = new Color(1, 1, 1, alpha);

                    // Option: if the panel doesn't have a Dialogue Box
                    if (p.panelTxtBg != null)
                        p.panelTxtBg.color = new Color(1, 1, 1, alpha);

                    // If Space is press, Immediately Reveal Them
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        togglePress = !togglePress; // true
                        p.panelImg.color = new Color(1, 1, 1, 1);
                        if (p.panelTxtBg != null)
                        {
                            p.panelTxtBg.color = new Color(1, 1, 1, 1);
                            p.panelTxt.color = new Color(1, 1, 1, 1);
                        }
                            
                        break;
                    }
                    yield return null;
                }

                // If the player hasn't press Space
                if (!togglePress)
                {
                    // Next Fade In the Panel's Text
                    if (p.panelTxt != null)
                        for (float alpha = 0f; alpha <= 1f; alpha += textFadeInRate * Time.deltaTime)
                        {
                            p.panelTxt.color = new Color(1, 1, 1, alpha);

                            // If Space is press, Immediately Reveal Them
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                togglePress = !togglePress; // true
                                p.panelTxt.color = new Color(1, 1, 1, 1);
                                break;
                            }
                            yield return null;
                        }
                }
                yield return new WaitForSeconds(nextPanel);
            }

            // Wait for player to press space
            // While waiting, a continue button will appear
            continueArrow.enabled = true;
            continueArrow.GetComponent<FadeInAndOut>().toggle = true;
            StartCoroutine(continueArrow.GetComponent<FadeInAndOut>().FadeInFadeOut());
            while (!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }
            continueArrow.GetComponent<FadeInAndOut>().toggle = false;
            continueArrow.GetComponent<FadeInAndOut>().Reset(continueArrow);
            continueArrow.enabled = false;

            // If the Page is the last page, play the outro music
            if (cutscene.IndexOf(c) == cutscene.Count - 1)
            {
                // Fade Out Intro Audio
                StartCoroutine(FadeAudioSource.StartFade(CutsceneMusic, 0.5f, 0f));

                // Start Playing the Outro
                CutsceneOutro.Play();
            }

            // Turn all Panel off when the cutscene is shown
            // And player press the Space Bar
            for (float alpha = 1f; alpha >= 0f; alpha -= fadeOutRate * Time.deltaTime)
            {
                foreach (Panel p in c.panel)
                {
                    p.panelImg.color = new Color(1, 1, 1, alpha);
                    if (p.panelTxtBg != null && p.panelTxt != null)
                    {
                        p.panelTxtBg.color = new Color(1, 1, 1, alpha);
                        p.panelTxt.color = new Color(1, 1, 1, alpha);
                    }
                       
                    yield return null;
                }
            }
            yield return new WaitForSeconds(nextPage);

            if (cutscene.IndexOf(c) == cutscene.Count - 1)
                StartCoroutine(ExitingCutscene());

            yield return null;
        }
        yield return null;
    }

}
