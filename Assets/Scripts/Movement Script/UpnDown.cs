using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpnDown : MonoBehaviour
{
    public Canvas canvas;
    public GameObject seaBodyTarget;
    public GameObject wavesTarget;
    private float range = 1700f;

    private Vector3 resetPos1;
    private Vector3 resetPos2;
    private Vector3 targetPos1;
    private Vector3 targetPos2;
    public float end1 = 360f, end2 = 420f;

    public float moveRate;
    public float waittime = .2f;

    private bool complete = false;

    // Start is called before the first frame update
    void Awake()
    {   
        float canvasScale = Screen.width / canvas.GetComponent<CanvasScaler>().referenceResolution.x;

        moveRate = canvasScale * moveRate;
        waittime = canvasScale * waittime;

        resetPos1 = seaBodyTarget.transform.position;
        targetPos1 = new Vector3(resetPos1.x, end1 * canvasScale, resetPos1.z);
        resetPos2 = wavesTarget.transform.position;
        targetPos2 = new Vector3(resetPos2.x, end2 * canvasScale, resetPos2.z);

        
    }

    // Update is called once per frame
    public void PlayWaveAnim()
    {
        StartCoroutine(SeaBodyUp());
        StartCoroutine(WavesUp());
        StartCoroutine(CallFunction());
    }

    IEnumerator SeaBodyUp()
    {
        for (float i = resetPos1.y; i <= targetPos1.y; i += Time.deltaTime * moveRate) 
        {
            seaBodyTarget.transform.position = new Vector3(seaBodyTarget.transform.position.x, i, seaBodyTarget.transform.position.z);
            yield return null;
        }

        yield return new WaitForSeconds(.1f);
    }

    IEnumerator SeaBodyDown()
    {
        for (float i = targetPos1.y; i >= resetPos1.y; i -= Time.deltaTime * moveRate)
        {
            seaBodyTarget.transform.position = new Vector3(seaBodyTarget.transform.position.x, i, seaBodyTarget.transform.position.z);
            yield return null;
        }
    }

    IEnumerator WavesUp()
    {
        for (float i = resetPos2.y; i <= targetPos2.y; i += Time.deltaTime * moveRate)
        {
            wavesTarget.transform.position = new Vector3(wavesTarget.transform.position.x, i, wavesTarget.transform.position.z);
            yield return null;
        }
    }
    IEnumerator WavesDown()
    {
        for (float i = targetPos2.y; i >= resetPos2.y; i -= Time.deltaTime * moveRate)
        {
            wavesTarget.transform.position = new Vector3(wavesTarget.transform.position.x, i, wavesTarget.transform.position.z);
            yield return null;
        }
    }

    IEnumerator CallFunction()
    {
        yield return new WaitForSeconds(waittime);
        this.GetComponent<MenuManager>().CreditToggle();
        yield return null;
        StartCoroutine(SeaBodyDown());
        StartCoroutine(WavesDown());
        yield return null;
    }
}
