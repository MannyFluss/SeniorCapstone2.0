using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleParentScript : MonoBehaviour
{

    [SerializeField]
    bool playOnStart = false;
    [SerializeField]
    ParticleSystem myParticleSystem;
    void Start()
    {

//        var children = GetComponentsInChildren<ParticleSystem>(true);    
  
        if (playOnStart == true)
        {
            play();
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
        print("here");
        // var children = GetComponentsInChildren<ParticleSystem>();     
        // foreach (ParticleSystem p in children)
        // {
        //     p.Play();
        // }
    }
}
