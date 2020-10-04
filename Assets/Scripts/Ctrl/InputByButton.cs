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
    public static bool isPress = false;//是否处于长按状态

    private float Delay = 0.5f;//延迟相当于按下持续时间
    private float LastDownTime;//

    private void Update()
    {
        if (LastDownTime > 0 && Time.time - LastDownTime > Delay)
        {
            isPress = true;
            //Debug.Log(isPress);
        }
        else
        {
            isPress = false;
            //Debug.Log(isPress);
        }
    }
    public void OnLeftButtonClick()
    {
        isLeft = true;
    }
    public void OnDownButtonClick()
    {
        isDown = true;
    }
    public void OnDownButtonDown()//按下按钮
    {
        LastDownTime = Time.time;
    }
    public void OnDownButtonUp()//松开按钮
    {
        LastDownTime = 0;
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
