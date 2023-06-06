using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLevel : MonoBehaviour
{
    public static GlobalLevel Instance;
    [ReadOnlyInspector] public float PlayerHealth;
    [ReadOnlyInspector] public bool CalamarDefeated;
    [ReadOnlyInspector] public bool DrKrabDefeated;

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
