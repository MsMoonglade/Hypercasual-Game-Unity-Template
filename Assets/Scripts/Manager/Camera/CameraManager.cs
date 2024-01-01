using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    public CinemachineVirtualCamera gameCamera;
    public CinemachineVirtualCamera editCamera;
    public CinemachineVirtualCamera endGameCamera;

    private float cameraShakeTime;

    private void Update()
    {
        //if shaking remove shake time
        if (cameraShakeTime > 0)
        {
            cameraShakeTime -= Time.deltaTime;

            if (cameraShakeTime <= 0)
            {
                gameCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            }
        }
    }

    public void ShakeGameCamera(float i_time, float i_amount)
    {
        gameCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = i_amount;
        cameraShakeTime = i_time;
    }

    public void SetGameCameraPriority()
    {
        gameCamera.Priority = 1;
        editCamera.Priority = 0;
    }

    public void SetEditCameraPriority()
    {
        editCamera.Priority = 1;
        gameCamera.Priority = 0;
    }

    public void SetEndGameCameraPriority()
    {
        editCamera.Priority = 0;
        gameCamera.Priority = 0;
        endGameCamera.Priority = 1;
    }

    public void NoCameraPriority()
    {
        editCamera.Priority = 0;
        gameCamera.Priority = 0;    
    }
}