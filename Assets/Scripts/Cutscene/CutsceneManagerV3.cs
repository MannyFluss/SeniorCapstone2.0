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
    public class Cutscene
    {
        public string cutsceneName;
        public List<Panel> panel = new List<Panel>();
    }

    // ============================= //

    [Header("Set-Up")]
    public List<Cutscene> cutscene = new List<Cutscene>();
    public float fadeInRate = 0.6f;
    public float fadeOutRate = 1f;

    [Header("Audio")]
    [SerializeField] private AudioClip CutsceneMusic;
    [SerializeField] private AudioSource CutsceneOutro;

    // ==========[Private]========== //
    private bool togglePress = false;

    // ============================= //

    void Start()
    {
        // Set all the Image (if didn't) into Invisible
        foreach (Cutscene c in cutscene)
            foreach (Panel p in c.panel)
            {
                p.panelImg.color = new Color(1, 1, 1, 0);
                p.panelTxtBg.color = new Color(1, 1, 1, 0);
                p.panelTxt.color = new Color(1, 1, 1, 0);
            }

        // Play the SoundTrack
        if (CutsceneMusic != null)
        {
            AudioSource audio = this.GetComponent<AudioSource>();
            audio.clip = CutsceneMusic;
            audio.Play();
        }

        StartCoroutine(FadeInAndOut());
    }

    private void StartCutscene()
    {
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        foreach (Cutscene c in cutscene)
        {
            togglePress = false;

            // Fade In Panel by Panel
            foreach (Panel p in c.panel)
            {
                // First Fade In the Panel's Img and Txt Bg
                for (float alpha = 0f; alpha <= 1f; alpha += fadeRate * Time.deltaTime)
                {
                    p.panelImg.color = new Color(1, 1, 1, alpha);
                    p.panelTxtBg.color = new Color(1, 1, 1, alpha);

                    // If Space is press, Immediately Reveal Them
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        togglePress = !togglePress; // true
                        p.panelTxt.color = new Color(1, 1, 1, 1);
                        p.panelImg.color = new Color(1, 1, 1, 1);
                        p.panelTxtBg.color = new Color(1, 1, 1, 1);
                        break;
                    }
                    yield return null;
                }

                // If the player hasn't press Space
                if (!togglePress)
                {
                    // Next Fade In the Panel's Text
                    for (float alpha = 0f; alpha <= 1f; alpha += fadeRate * Time.deltaTime)
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
                yield return null;
            }

            // Wait for player to press space
            while (!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }

            // Turn all Panel off when the cutscene is shown
            // And player press the Space Bar
            foreach (Panel p in c.panel)
            {
                p.panelTxt.color = new Color(1, 1, 1, 0);
                p.panelImg.color = new Color(1, 1, 1, 0);
                p.panelTxtBg.color = new Color(1, 1, 1, 0);
            }
            yield return null;
        }
        yield return null;
    }

}
