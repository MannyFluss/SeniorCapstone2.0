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
        public GameObject panelObj;
        private Image panelImg;
        private Image panelTxtBox;

        // If Panel has Multiple Text Box
        private List<TMP_Text> panelTxt = new List<TMP_Text>();

        public void SetVariable()
        {
            
            // First Store the Panel Image
            panelImg = panelObj.transform.GetChild(0).GetComponent<Image>();

            // If the Panel Text, through the child and Store them Properly
            if (panelObj.transform.childCount > 2)  
            {
                panelTxtBox = panelObj.transform.GetChild(1).GetComponent<Image>();

                TMP_Text[] tmp = panelObj.GetComponentsInChildren<TMP_Text>();
                for (int i = 0; i < tmp.Length; i++) panelTxt.Add(tmp[i]);
            }
            
        }
        public Image GetImg() { return panelImg; }
        public Image GetTxtBox() 
        {
            return panelTxtBox;
        }
        public TMP_Text GetTxt(int i) 
        {
            if (i < panelTxt.Count) return panelTxt[i];
            else return null;
        }
        public int GetTxtLen()
        {
            return panelTxt.Count;
        }
    }

    [System.Serializable]
    public class Page
    {
        public string PageName;
        public bool playPage = true;
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
    [SerializeField] private float imageFadeInRate = 0.8f;
    [SerializeField] private float textFadeInRate = 1.2f;
    [SerializeField] private float fadeOutRate = 16f;
    [SerializeField] private float nextPanel = 0.75f;
    [SerializeField] private float panel2Text = 0.50f;
    [SerializeField] private float nextText = 0.75f;
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
                p.SetVariable();
                p.GetImg().color = new Color(1, 1, 1, 0);

                // If the Panel does have a dialogue box or text
                // Set them all to invis, via color
                if (p.GetTxtBox() != null)
                    p.GetTxtBox().color = new Color(1, 1, 1, 0);
                for (int i = 0; i < p.GetTxtLen(); i++)
                    p.GetTxt(i).color = new Color(1, 1, 1, 0);
             
            }

        // Make sure the Black Screen alpha is 1
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

    private void RevealAll(Panel p)
    {
        togglePress = true;
        p.GetImg().color = new Color(1, 1, 1, 1);
        if (p.GetTxtBox() != null) p.GetTxtBox().color = new Color(1, 1, 1, 1);
        for (int i = 0; i < p.GetTxtLen(); i++)
            p.GetTxt(i).color = new Color(1, 1, 1, 1);
    }

    IEnumerator FadeInAndOut()
    {
        // Enter by fading in from a black screen
        for (float alpha = 1f; alpha >= 0f; alpha -= imageFadeInRate * Time.deltaTime)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.b, blackScreen.color.g, alpha);
            yield return null;
        }

        // Start Showing page by page
        foreach (Page c in cutscene)
        {
            if (!c.playPage) continue;
            
            togglePress = false;

            // Fade In Panel by Panel
            foreach (Panel p in c.panel)
            {
                togglePress = false;

                yield return new WaitForSeconds(nextPanel);
                // First Fade In the Panel's Img and Txt Bg
                for (float alpha = 0f; alpha <= 1f; alpha += imageFadeInRate * Time.deltaTime)
                {
                    p.GetImg().color = new Color(1, 1, 1, alpha);

                    // If Space is press, Immediately Reveal Them
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        RevealAll(p);
                        break;
                    }
                    yield return null;
                }

                yield return new WaitForSeconds(panel2Text);
                // If the player hasn't press Space to skip Panel
                if (!togglePress)
                {
                    bool txtbox = false;
                    // Next, Fade In the Panel's Text
                    if (p.GetTxtLen() != 0)
                        for (int i = 0; i < p.GetTxtLen(); i++)
                        {
                            if (!togglePress)
                            {
                                for (float alpha = 0f; alpha <= 1f; alpha += textFadeInRate * Time.deltaTime)
                                {
                                    // Option: if the panel doesn't have a Dialogue Box
                                    if (p.GetTxtBox() != null && !txtbox) p.GetTxtBox().color = new Color(1, 1, 1, alpha);

                                    p.GetTxt(i).color = new Color(1, 1, 1, alpha);
                                    yield return null;
                                    // If Space is press, Immediately Reveal Them
                                    if (Input.GetKeyDown(KeyCode.Space))
                                    {
                                        RevealAll(p);
                                        break;
                                    }
                                    if (togglePress) break;
                                }
                                yield return new WaitForSeconds(nextText);
                            }
                            txtbox = true;
                        }
                }
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
                    p.GetImg().color = new Color(1, 1, 1, alpha);
                    if (p.GetTxtBox() != null) p.GetTxtBox().color = new Color(1, 1, 1, alpha);
                    for (int i = 0; i < p.GetTxtLen(); i++)
                        p.GetTxt(i).color = new Color(1, 1, 1, alpha);
                    yield return null;
                }
            }

            // Make sure all obj alpha is 0
            foreach (Panel p in c.panel)
            {
                p.GetImg().color = new Color(1, 1, 1, 0);
                if (p.GetTxtBox() != null) p.GetTxtBox().color = new Color(1, 1, 1, 0);
                for (int i = 0; i < p.GetTxtLen(); i++)
                    p.GetTxt(i).color = new Color(1, 1, 1, 0);
            }

            yield return new WaitForSeconds(nextPage);
        }
        StartCoroutine(ExitingCutscene());
        yield return null;
    }

}
