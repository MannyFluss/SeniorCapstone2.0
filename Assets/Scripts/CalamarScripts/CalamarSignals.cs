using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CalamarSignals : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;

    bool reduceNoise = false;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(reduceNoise)
        {
            noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, 0, 0.1f);
            noise.m_FrequencyGain = Mathf.Lerp(noise.m_FrequencyGain, 0, 0.1f);
        }
        if(noise.m_AmplitudeGain == 0)
        {
            reduceNoise = false;
        }
    }

    public void ShakeCamera()
    {
        reduceNoise = false;
        CinemachineBasicMultiChannelPerlin noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 1;
        noise.m_FrequencyGain = 1;
    }

    public void BigShake()
    {
        reduceNoise = false;
        CinemachineBasicMultiChannelPerlin noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 4;
        noise.m_FrequencyGain = 4;
    }

    public void NoShake()
    {
        reduceNoise = true;
    }
}
