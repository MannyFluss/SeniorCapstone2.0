using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //added PlayButton Sound effect
    [SerializeField] private AudioSource PlaySoundEffect;

    //added UIHover Sound effect
    [SerializeField] private AudioSource HoverSoundEffect;

    public void PlayGame()
    {
        //Play sound effect
        PlaySoundEffect.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
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
