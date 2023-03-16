using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public class Cutscene
    {
        public Image stillImage;
        public string narrative;

        public Cutscene(Image still, string narr)
        {
            stillImage = still;
            narrative = narr;  
        }
        public Cutscene() { stillImage = null;narrative = null; }

    }
    public Cutscene cutscene1 = new Cutscene();
    public List<Cutscene> listOfCutscene = new List<Cutscene>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
