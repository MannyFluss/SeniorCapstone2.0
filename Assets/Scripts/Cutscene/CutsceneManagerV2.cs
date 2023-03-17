using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManagerV2 : MonoBehaviour
{
    //private Dictionary<Image, string> cutscene = new Dictionary<Image, string>();
    [Serializable]
    public class cutsceneNarrativePair
    {
        public Sprite cutsceneImage;
        public string cutsceneNarrative;
    }

    public List<cutsceneNarrativePair> cutsceneList = new List<cutsceneNarrativePair>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
