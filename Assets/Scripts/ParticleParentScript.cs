using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleParentScript : MonoBehaviour
{

    [SerializeField]
    bool playOnStart = false;
    ParticleSystem myParticleSystem;
    [SerializeField]
    ParticleSystem[] additionalParticles;
    [SerializeField]
    float timeUntilDestroy = 0.0f;
    void Start()
    {

        myParticleSystem = GetComponent<ParticleSystem>();
  
        if (playOnStart == true)
        {
            play();
        }
        if (timeUntilDestroy > 0.0f)
        {
            Destroy(gameObject, timeUntilDestroy);
        }
    }
    public void play()
    {
        startParticlesInChildren();
    }
    public void startParticlesInChildren()
    {
        myParticleSystem.Stop();
        myParticleSystem.Play(true);
        //play each particles in the additionalParticles array
        for (int i = 0; i < additionalParticles.Length; i++)
        {
            print("play");
            additionalParticles[i].Stop();
            additionalParticles[i].Play(true);
        }
    }
}