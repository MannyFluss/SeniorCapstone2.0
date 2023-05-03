using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.ConstrainedExecution;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CutsceneManagerV3 : MonoBehaviour
{
    [Serializable]
    public class cutscene
    {
        private Sprite cutsceneImage;
        private string cutsceneDialogue;
    }

    public AudioClip CutsceneOST;

    [Header("Set-Up")]
    public List<cutscene> cutsceneList = new List<cutscene>();
    public string nextSceneName;
}
