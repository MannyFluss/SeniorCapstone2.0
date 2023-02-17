using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUIScriptManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    [Header("Abilities")]
    [SerializeField]
    GameObject[] Abilities;



    public void setAbilityCoolDown(int _index, float _coolDown)
    {
        
        Abilities[_index].GetComponent<Image>().color = new Color(0.0f,0.0f,0.0f);
        //LeanTween.color(Abilities[_index],new Color(0f,0f,0f),0f);
        LeanTween.color(Abilities[_index].GetComponent<Image>().rectTransform,new Color(1f,1f,1f),_coolDown);

    }
    // Update is called once per frame

}
