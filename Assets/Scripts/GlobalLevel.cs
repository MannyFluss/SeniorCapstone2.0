using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLevel : MonoBehaviour
{
    public static GlobalLevel Instance;
    [ReadOnlyInspector] public bool CalamarDefeated;
    [ReadOnlyInspector] public bool DrKrabDefeated;
    [ReadOnlyInspector] public float PlayerHealth = 9;

    private void Awake()
    {
        CalamarDefeated = true;
        DrKrabDefeated = true;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
