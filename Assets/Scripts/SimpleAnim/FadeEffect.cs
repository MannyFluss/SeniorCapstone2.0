using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [Header("Effects Options")]
    public bool FadeIn = false;
    public bool FadeOut = false;

    // [Header("Target Object Image")]

    void Start()
    {
        // Check if the Object attached is disabled; if not, disabled it

    }

    IEnumerator FadeInEffect()
    {
        yield return null;
    }

    IEnumerator FadeOutEffect()
    {
        yield return null;
    }
}
