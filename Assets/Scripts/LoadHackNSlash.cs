using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHackNSlash : MonoBehaviour
{
    public string SceneName;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other) {
        // Load the new scene.
        SceneManager.LoadScene(SceneName);
    }
}
