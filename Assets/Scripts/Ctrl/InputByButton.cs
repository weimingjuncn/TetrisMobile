using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class InputByButton : MonoBehaviour
{
    public static bool isLeft = false;
    public static bool isRight = false;
    public static bool isDown = false;
    public static bool isRotate = false;

    public void OnLeftButtonClick()
    {
        isLeft = true;
    }

    public void OnDownButtonClick()
    {
        isDown = true;
    }

    public void OnRightButtonClick()
    {
        isRight = true;
    }

    public void OnRotateButtonClick()
    {
        isRotate = true;
    }

    public static void ResetButtonState()
    {
        isLeft = false;
        isRight = false;
        isDown = false;
        isRotate = false;
    }
}
