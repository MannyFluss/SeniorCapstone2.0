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
            AbilityIcons.Add("ClawsOff","FinalAbilities/ABILITY_shockwave");
            AbilityIcons.Add("SchrodingerBox","FinalAbilities/ABILITY_schrondingers box");
            AbilityIcons.Add("ExplosiveFishAbility","FinalAbilities/ABILITY_fish barrel");
            AbilityIcons.Add("NavalMine","FinalAbilities/ABILITY_naval mine");
            AbilityIcons.Add("HeartyFix","FinalAbilities/ABILITY_hearty fix");
            AbilityIcons.Add("KnivesOut","FinalAbilities/ABILITY_knives out");
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
        print(_input);
        if (AbilityIcons.ContainsKey(_input)==false)
        {
            return Resources.Load<Sprite>(AbilityIcons["empty"]);
        }
        return Resources.Load<Sprite>(AbilityIcons[_input]);
    }

}