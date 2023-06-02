using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Glow : MonoBehaviour
{
    private SpriteRenderer target;
    private float MAX;
    private float MIN;
    [SerializeField] private bool toggle;

    [Header("Brightness Level Range (0-100)")]
    [SerializeField] private int min;
    [SerializeField] private int max;
    [SerializeField] private float glowRate;

    // Start is called before the first frame update
    void Start()
    {
        toggle = false;
        target = this.GetComponent<SpriteRenderer>();
        if (target != null) toggle = true;

        if (max > 100) max = 100;
        if (min < 0) min = 0;

        target.color = new Color(min, min, min, 1);
        MAX = max / 100f;
        MIN = min / 100f;

        if (toggle) StartCoroutine(GlowBright());
    }

    IEnumerator GlowBright()
    {
        // Debug.Log("Start Brightening");
        for (float i = MIN; i <= MAX; i += glowRate * Time.deltaTime)
        {
            target.color = new Color(i, i, i, 1);
            yield return null;
        }
        target.color = new Color(MAX, MAX, MAX, 1);
        StartCoroutine(GlowDim());
        yield return null;
    }

    IEnumerator GlowDim()
    {
        // Debug.Log("Start Dimming");
        for (float i = MAX; i >= MIN; i -= glowRate * Time.deltaTime)
        {
            target.color = new Color(i, i, i, 1);
            yield return null;
        }
        target.color = new Color(MIN, MIN, MIN, 1);
        StartCoroutine(GlowBright());
        yield return null;
    }
}
