using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cbmcp;
    [SerializeField] private float ShakeIntensity;
    [SerializeField] private float ShakeTime;

    private void Awake()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>().GetComponent<CinemachineVirtualCamera>();
        _cbmcp = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0;
    }

    public void ChangeCamera()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>().GetComponent<CinemachineVirtualCamera>();
        _cbmcp = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0;
    }

    public void ShakeCamera()
    {
        _cbmcp = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = ShakeIntensity;
        StartCoroutine(StopShake());
    }

    private IEnumerator StopShake()
    {
        yield return new WaitForSeconds(ShakeTime);
        _cbmcp = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0;
    }
    
}
