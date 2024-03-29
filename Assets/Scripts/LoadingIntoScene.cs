using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingIntoScene : MonoBehaviour
{
    [SerializeField] private Image BlackScreen;
    private float fadeRate = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        BlackScreen.enabled = true;
        BlackScreen.color = new Color(0, 0, 0, 1);

        StartCoroutine(EnterScene());
    }

    IEnumerator EnterScene()
    {
        yield return new WaitForSeconds(0.5f);
        for (float alpha = 1f; alpha >= 0; alpha -= fadeRate * Time.deltaTime)
        {
            BlackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        BlackScreen.enabled = false;
        yield return null;
    }
}
