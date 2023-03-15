using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance {get; private set;}
    public int Value;
    public Dictionary<string,string> AbilityIcons = new Dictionary<string, string>();
    public List<string> playerAbilitiesCopy = new List<string> {"SchrodingerBox","ClawsOff",null};
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            AbilityIcons.Add("ClawsOff","clawsOffTemp");
            AbilityIcons.Add("SchrodingerBox","HUD_schrodinger_box");
            AbilityIcons.Add("ExplosiveFishAbility","HUD_fish_barrel");
            AbilityIcons.Add("NavalMine","HUD_naval_mine");
            AbilityIcons.Add("empty", "HUD_no_ability");

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

}