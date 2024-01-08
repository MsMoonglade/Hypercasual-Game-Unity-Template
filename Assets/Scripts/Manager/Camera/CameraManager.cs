using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera offCamera;
    
    //setted in End Game Behaviour
    [HideInInspector]
    public CinemachineVirtualCamera endGameCamera;

    private float cameraShakeTime;

    private void Start()
    {
        SetMainCameraPriority();
    }

    private void Update()
    {
        //if shaking remove shake time
        if (cameraShakeTime > 0)
        {
            cameraShakeTime -= Time.deltaTime;

            if (cameraShakeTime <= 0)
            {
                mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            }
        }
    }

    public void SetCameraTarget(CinemachineVirtualCamera i_camera , Transform i_newTarget)
    {
        i_camera.Follow = i_newTarget;
    }

    public void ShakeGameCamera(float i_time, float i_amount)
    {
        mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = i_amount;
        cameraShakeTime = i_time;
    }

    public void SetMainCameraPriority()
    {
        mainCamera.Priority = 1;
        offCamera.Priority = 0;
    }

    public void SetOffCameraPriority()
    {
        offCamera.Priority = 1;
        mainCamera.Priority = 0;
    }

    public void SetEndGameCameraPriority()
    {
        offCamera.Priority = 0;
        mainCamera.Priority = 0;
        endGameCamera.Priority = 1;
    }

    public void NoCameraPriority()
    {
        offCamera.Priority = 0;
        mainCamera.Priority = 0;    
    }
}