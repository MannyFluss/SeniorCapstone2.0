using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.ConstrainedExecution;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class CutsceneManagerV2 : MonoBehaviour
{

    //added a FadeAudioSource Function
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

    //added IntroOutroMusic
    [SerializeField] private AudioSource IntroOutro;


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
    public Image SUB_BG;

    // [Header("Editing Variables")]
    private float imageFadeRate = 0.85f;
    private float textFadeRate = 1.8f;
    private float pauseTimer = 0.05f;
    private float transitionTimer = 1.8f;
    
 
    void Start()
    {
        GRAPHICS.color = new Color(1, 1, 1, 0);
        SUBTITLE.color = new Color(1, 1, 1, 0);
        SUB_BG.color = new Color(1, 1, 1, 0);
        if (cutsceneList.Count > 0)
        {
            // Play the Audio Source when the cutscene starts, if there is a file attached
            if (CutsceneMusic != null)
            {
                AudioSource audio = this.GetComponent<AudioSource>();
                audio.clip = CutsceneMusic;
                audio.Play();
            }

            StartCutscene();
        }
    }

    private void StartCutscene()
    {
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        yield return new WaitForSeconds(transitionTimer);
        for (int ctr = 0; ctr < cutsceneList.Count; ctr++)
        {
            GRAPHICS.sprite = cutsceneList[ctr].cutsceneImage;
            SUBTITLE.text = cutsceneList[ctr].cutsceneNarrative;


            //Debug.Log(SUBTITLE.maxHeight);

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
                SUB_BG.color = new Color(0, 0, 0, alpha);
                yield return null;
            }

            // Pause until Player click
            while (!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
                //throw fade in here
            }

            // Subtitle Fade Out
            for (float alpha = 1f; alpha > 0f; alpha -= textFadeRate * Time.deltaTime)
            {
                SUBTITLE.color = new Color(1, 1, 1, alpha);
                SUB_BG.color = new Color(0, 0, 0, alpha);
                yield return null;
            }

            

            // Pause before Image Disappear
            yield return new WaitForSeconds(pauseTimer);

            //check for cutscene 3 and then start audio fade
            if (ctr == 2)
            {
                //fade intro audio
                StartCoroutine(FadeAudioSource.StartFade(GetComponent<AudioSource>(), 0.5f, 0f));
                //GetComponent<AudioSource>().Stop();
                IntroOutro.Play();
            }

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

    private bool DetectMouseClick()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            return true;
        }
        return false;
    }
}
