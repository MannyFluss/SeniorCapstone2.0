using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NameArrayAttribute : PropertyAttribute
{
    public readonly List<string> names = new List<string>();
    
    public NameArrayAttribute(string name, int len) { 
        for (int i = 1; i <= len; i++)
            this.names.Add(name + ' ' + i);
    }
}
