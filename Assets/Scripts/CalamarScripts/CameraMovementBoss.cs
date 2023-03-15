using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovementBoss : MonoBehaviour
{
    CinemachineVirtualCamera vcam;

    [SerializeField]
    GameObject player;

    [SerializeField]
    float moveValue;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        vcam.transform.position = Vector3.Lerp(vcam.transform.position, new Vector3(player.transform.position.x / moveValue, vcam.transform.position.y, vcam.transform.position.z), 0.1f);
    }
}
