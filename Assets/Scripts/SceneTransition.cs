using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void nextScene(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
