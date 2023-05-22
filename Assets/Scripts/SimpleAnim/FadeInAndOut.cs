using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAndOut : MonoBehaviour
{
    private Image target;

    [Header("Set-Up")]
    [SerializeField] private float fadeInRate = 1.6f;
    [SerializeField] private float fadeOutRate = 1.6f;
    [SerializeField] public bool toggle = false;

    // Start is called before the first frame update
    void Start()
    {
        target = this.GetComponent<Image>();
        toggle = false;
        Reset(target);
    }

    public IEnumerator FadeInFadeOut()
    {
        for (float alpha = 0f; alpha <= 1f; alpha += fadeInRate * Time.deltaTime)
        {
            target.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        for (float alpha = 1f; alpha >= 0f; alpha -= fadeOutRate * Time.deltaTime)
        {
            target.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        if (toggle) StartCoroutine(FadeInFadeOut());
        yield return null;
    }

    public void Reset(Image img)
    {
        img.color = new Color(1, 1, 1, 1);
    }
}
