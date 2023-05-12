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
            AbilityIcons.Add("ClawsOff","newIcons/ABILITY_shockwave");
            AbilityIcons.Add("SchrodingerBox","newIcons/ABILITY_schrondingers box");
            AbilityIcons.Add("ExplosiveFishAbility","newIcons/ABILITY_fish barrel");
            AbilityIcons.Add("NavalMine","newIcons/ABILITY_naval mine");
            AbilityIcons.Add("KnivesOut","newIcons/ABILITY_knives out");
            AbilityIcons.Add("HeartyFix","newIcons/ABILITY_hearty fix");
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