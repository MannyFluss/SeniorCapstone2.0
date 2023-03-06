using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance {get; private set;}
    public int Value;
    public Dictionary<string,string> AbilityIcons = new Dictionary<string, string>();
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            //AbilityIcons = new Dictionary<string, Sprite>();
            AbilityIcons.Add("ClawsOff","Assets/Resources/HUD/HUD_empty heart.png");
            AbilityIcons.Add("SchrodingerBox","Assets/Resources/HUD/HUD schrodinger box.png");
            AbilityIcons.Add("ExplosiveFishAbility","Assets/Resources/HUD/HUD fish barrel.png");
            AbilityIcons.Add("empty", "Assets/Resources/HUD/HUD_empty heart.png");

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public Sprite getIconTexture(string _input)
    {
        if (AbilityIcons.ContainsKey(_input)==false)
        {
            return Resources.Load<Sprite>(AbilityIcons["empty"]);
        }
        return Resources.Load<Sprite>(AbilityIcons[_input]);

    }
    public Sprite test()
    {
        return Resources.Load<Sprite>("Assets/Resources/ability_1.png");
    }

}