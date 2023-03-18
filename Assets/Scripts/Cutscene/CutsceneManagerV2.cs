using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.ConstrainedExecution;
using UnityEngine.SceneManagement;

public class CutsceneManagerV2 : MonoBehaviour
{
    //private Dictionary<Image, string> cutscene = new Dictionary<Image, string>();
    [Serializable]
    public class cutsceneNarrativePair
    {
        public Sprite cutsceneImage;
        public string cutsceneNarrative;
        public float cutsceneTimer;
    }

    public AudioClip CutsceneMusic;

    [Header("Set-Up")]
    public List<cutsceneNarrativePair> cutsceneList = new List<cutsceneNarrativePair>();
    public string nextSceneName;

    [Header("Operating GameObject")]
    public Image GRAPHICS;
    public TMP_Text SUBTITLE;

    [Header("Editing Variables")]
    private float imageFadeRate = 0.85f;
    private float textFadeRate = 1.5f;
    private float pauseTimer = 0.1f;
    private float transitionTimer = 1.8f;

    void Start()
    {
        GRAPHICS.color = new Color(1, 1, 1, 0);
        SUBTITLE.color = new Color(1, 1, 1, 0);
        if (cutsceneList.Count > 0)
        {
            // Play the Audio Source when the cutscene starts
            AudioSource audio = this.GetComponent<AudioSource>();
            audio.clip = CutsceneMusic;
            audio.Play();

            StartCoroutine(FadeInAndOut());
        }
    }

    IEnumerator FadeInAndOut()
    {
        yield return new WaitForSeconds(transitionTimer);
        for (int ctr = 0; ctr < cutsceneList.Count; ctr++)
        {
            GRAPHICS.sprite = cutsceneList[ctr].cutsceneImage;
            SUBTITLE.text = cutsceneList[ctr].cutsceneNarrative;

            // Image Fade In
            for (float alpha = 0f; alpha <= 1f; alpha += imageFadeRate * Time.deltaTime)
            {
                GRAPHICS.color = new Color(1, 1, 1, alpha);
                yield return null;
            }

            // Pause before Text appear
            yield return new WaitForSeconds(pauseTimer);

            // Subtitle Fade In
            for (float alpha = 0f; alpha <= 1f; alpha += textFadeRate * Time.deltaTime)
            {
                SUBTITLE.color = new Color(1, 1, 1, alpha);
                yield return null;
            }

            // Pause to show image
            yield return new WaitForSeconds(cutsceneList[ctr].cutsceneTimer);

            // Subtitle Fade Out
            for (float alpha = 1f; alpha > 0f; alpha -= textFadeRate * Time.deltaTime)
            {
                SUBTITLE.color = new Color(1, 1, 1, alpha);
                yield return null;
            }

            // Pause before Image Disappear
            yield return new WaitForSeconds(pauseTimer);

            // Image Fade Out
            for (float alpha = 1f; alpha > 0f; alpha -= imageFadeRate * Time.deltaTime)
            {
                GRAPHICS.color = new Color(1, 1, 1, alpha);
                yield return null;
            }

            // Pause with black screen
            yield return new WaitForSeconds(transitionTimer);
        }
        SceneManager.LoadScene(nextSceneName);
        yield return null;
    }
}
