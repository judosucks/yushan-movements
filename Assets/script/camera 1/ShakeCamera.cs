using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    private float startingIntendity;
    private float shakeTimerTotal;
    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    public void Shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        startingIntendity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }
    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;


            //time over
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntendity, 0f, shakeTimer / shakeTimerTotal);
        }
    }


}
