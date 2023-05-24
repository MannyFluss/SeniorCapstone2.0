using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeeMain : MonoBehaviour
{
    public static VeeMain Instance;
    [ReadOnlyInspector] public bool NotFirstTime;
    [ReadOnlyInspector] public bool TalkToVee = false;
    [ReadOnlyInspector] public int PastLine;

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
