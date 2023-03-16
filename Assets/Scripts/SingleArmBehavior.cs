using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleArmBehavior : MonoBehaviour
{
    public bool Frenzy;
    private bool frenzyInProgress = false;
    public bool WipeOut;
    private bool wipeOutInProgress = false;

    [SerializeField]
    Transform player;

    //Wipeout
    int side;

    //Frenzy
    [SerializeField]
    GameObject indicator;

    GameObject indicatorMade;

    private bool follow = false;

    private bool slam = false;
    private bool sweep = false;
    private bool moveBack = false;

    Quaternion rotateTo;
    Quaternion rotateFrom;

    Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Frenzy && !frenzyInProgress)
        {
            StartCoroutine(TentacleFrenzy());
            frenzyInProgress = true;
        }
        if (WipeOut && !wipeOutInProgress)
        {
            StartCoroutine(WipeOutTime());
            wipeOutInProgress = true;
        }
        FrenzyHandler();
        WipeOutHandler();
    }

    private void FrenzyHandler()
    {
        if(!Frenzy)
        {
            return;
        }
        if(follow)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), 0.1f);
        }
        if (indicatorMade != null)
        {
            indicatorMade.transform.localScale = Vector3.Lerp(indicatorMade.transform.localScale, new Vector3(13f, 1f, 1f), 0.03f);
        }
        if(moveBack)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, rotateFrom, 0.05f);
        }
        else if(slam)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, rotateTo, 0.3f);
        }
        
    }

    private void WipeOutHandler()
    {
        if(!WipeOut)
        {
            return;
        }
        if (slam)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, rotateTo, 0.2f);
        }
        if (sweep)
        {
            //Here is where the end position of the arm gets set                    \/\/\/\/
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(-16, transform.localPosition.y, transform.localPosition.z), 0.033f); 
            
        }
        if (moveBack)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, 0.01f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, rotateFrom, 0.075f);
        }
    }

    IEnumerator TentacleFrenzy()
    {
        follow = true;
        yield return new WaitForSeconds(3.4f);
        follow = false;
        indicatorMade = Instantiate(indicator, transform.position, Quaternion.Euler(0, 90, 0));
        yield return new WaitForSeconds(2.5f);
        slam = true;
        rotateFrom = transform.rotation;
        rotateTo = transform.rotation * Quaternion.Euler(new Vector3(90, 0, 0));
        yield return new WaitForSeconds(1f);
        Destroy(indicatorMade);
        yield return new WaitForSeconds(1f);
        slam = false;
        yield return new WaitForSeconds(1f);
        moveBack = true;
        yield return new WaitForSeconds(6f);
        moveBack = false;
        frenzyInProgress = false;
    }
    IEnumerator WipeOutTime()
    {
        side = Random.Range(0, 2);
        originalPosition = transform.position;
        moveBack = false;
        yield return new WaitForSeconds(1f);
        rotateFrom = transform.rotation;
        rotateTo = transform.rotation * Quaternion.Euler(new Vector3(90, 0, 0));
        slam = true;
        yield return new WaitForSeconds(1f);
        sweep = true;
        yield return new WaitForSeconds(3f);
        moveBack = true;
        sweep = false;
        slam = false;
        yield return new WaitForSeconds(5.8f);
        wipeOutInProgress = false;
    }
}
