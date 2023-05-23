using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string nextScene;
    //added PlayButton Sound effect
    [SerializeField] private AudioSource PlaySoundEffect;

    //added UIHover Sound effect
    [SerializeField] private AudioSource HoverSoundEffect;

    public void PlayGame()
    {
        //Play sound effect
        PlaySoundEffect.Play();
        SceneManager.LoadScene(nextScene);
        
    }

    //hover ui
    public void HoverSound()
    {
        //play ui hover sound
        HoverSoundEffect.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
