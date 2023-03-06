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
            //THIS TOOK WAY TOO LONG TO DO naming convention is beyond human comprehension
            AbilityIcons.Add("ClawsOff","HUD_empty_heart");
            AbilityIcons.Add("SchrodingerBox","HUD_schrodinger_box");
            AbilityIcons.Add("ExplosiveFishAbility","HUD_fish_barrel");
            AbilityIcons.Add("empty", "HUD_empty_heart");

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