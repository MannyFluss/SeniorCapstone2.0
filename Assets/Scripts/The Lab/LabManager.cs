using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabManager : MonoBehaviour
{
    [Header("Audio/Soundtrack")]
    [SerializeField] private AudioSource LabMusic;

    [Header("Portals")]
    [SerializeField] private GameObject CalamarDoor;
    [SerializeField] private GameObject DrKrabDoor;

    // Check the Global Level variables to see which door are open.
    void Start()
    {
        GlobalLevel.Instance.PlayerHealth = 9f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().health = GlobalLevel.Instance.PlayerHealth;
        // Play Music
        PlayLabMusic();

        // If Calamar has not been defeated, Dr Krab's door will remain closed
        CheckCalamarDefeated();
    }

    private void PlayLabMusic()
    {
        if (LabMusic != null)
        {
            LabMusic.loop = true;
            AudioSource audio = LabMusic;
            audio.Play();
        }
    }

    private void CheckCalamarDefeated()
    {
        GameObject DKportal = DrKrabDoor.transform.Find("Portal").gameObject;
        GameObject DKsymbol = DrKrabDoor.transform.Find("Dr Krab Symbol").gameObject;
        GameObject DKdoor = DrKrabDoor.transform.Find("Dr Krab Closed").gameObject;
        GameObject DKlight = DrKrabDoor.transform.Find("Dr Krab SL").gameObject;

        if (!GlobalLevel.Instance.CalamarDefeated)
        {
            // Turn Off the effects on the portal
            DKportal.GetComponent<RotateAnimation>().enabled = false;
            DKportal.GetComponent<Glow>().enabled = false;

            // Hide the Dr Krab Icon
            DKsymbol.GetComponent<SpriteRenderer>().enabled = false;

            // Show the Closed Door
            DKdoor.GetComponent<SpriteRenderer>().enabled = true;
            DKdoor.GetComponent<BoxCollider>().enabled = true;

            // Turn off the Light
            DKlight.GetComponent<Light>().enabled = false;

            Debug.Log("Calamar hasn't been defeated");
        }
        else
        {
            // Turn On the effects on the portal
            DKportal.GetComponent<RotateAnimation>().enabled = true;
            DKportal.GetComponent<Glow>().enabled = true;

            // Show the Dr Krab Icon
            DKsymbol.GetComponent<SpriteRenderer>().enabled = true;

            // Hide the Closed Door
            DKdoor.GetComponent<SpriteRenderer>().enabled = false;
            DKdoor.GetComponent<BoxCollider>().enabled = false;

            // Turn on the Light
            DKlight.GetComponent<Light>().enabled = true;

            Debug.Log("Calamar HAS been defeated");
        }
    }
}
