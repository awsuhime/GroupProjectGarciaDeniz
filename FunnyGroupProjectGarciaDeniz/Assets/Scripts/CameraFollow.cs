using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.U2D;
using System.Reflection;

public class CameraFollow : MonoBehaviour
{
    CinemachineBrain CB;
    object Internal;
    FieldInfo OrthoInfo;
    void Start()
    {
        CB = GetComponent<CinemachineBrain>();
        Internal = typeof(PixelPerfectCamera).GetField("m_internal", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(GetComponent<PixelPerfectCamera>());
        OrthoInfo = Internal.GetType().GetField("orthoSize", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var cam = CB.ActiveVirtualCamera as CinemachineVirtualCamera;
        if (cam)
        {
            cam.m_Lens.OrthographicSize = (float)OrthoInfo.GetValue(Internal);
        }
       
    }
}
