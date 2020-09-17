using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    /// <summary>
    /// 放大
    /// </summary>
    public void ZoomIn()
    {
        mainCamera.DOOrthoSize(14f, 0.5f);
    }
    /// <summary>
    /// 缩小
    /// </summary>
    public void ZoomOut()
    {
        mainCamera.DOOrthoSize(17f, 0.5f);
    }
}
