using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLevel : MonoBehaviour
{
    public static GlobalLevel Instance;
    [ReadOnlyInspector] public bool CalamarDefeated = false;
    [ReadOnlyInspector] public bool DrKrabDefeated = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
