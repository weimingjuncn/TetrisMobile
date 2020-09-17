using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    Transform pivot;
    Ctrl ctrl;
    GameManager gameManager;
    bool isPause = false;
    bool isSpeedUp = false;
    float timer = 0;
    float stepTime = 0.8f;
    int multiple = 15;

    private void Awake()
    {
        pivot = transform.Find("Pivot");
    }
    private void Update()
    {
        if (isPause) return;
        timer += Time.deltaTime;
        if (timer >stepTime)
        {
            timer = 0;
            Fall();
        }
        InputControl();
        InputByButton.ResetButtonState();
    }

    private void Fall()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
        if (ctrl.model.IsValidMapPosition(this.transform) == false)
        {
            pos.y += 1;
            transform.position = pos;
            isPause = true;
            
            bool isLineClear = ctrl.model.PlaceShape(this.transform);
            if (isLineClear) ctrl.audioManager.PlayLineClear();
            gameManager.FallDown();
            return;
        }
        ctrl.audioManager.PlayDrop();
    }

    private void InputControl()
    {
        float h = 0;
        if(Input.GetKeyDown(KeyCode.LeftArrow)
            || InputByButton.isLeft)
        {
            h = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)
            || InputByButton.isRight)
        {
            h = 1;
        }
        if (h != 0)
        {
            Vector3 pos = transform.position;
            pos.x += h;
            transform.position = pos;
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                pos.x -= h;
                transform.position = pos;
            }
            else
            {
                ctrl.audioManager.PlayControl();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)
            ||InputByButton.isRotate)
        {
            transform.RotateAround(pivot.position, Vector3.forward, -90);
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90);
            }
            else
            {
                ctrl.audioManager.PlayControl();
            }
        }
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    isSpeedUp = true;
        //    stepTime /= multiple;
        //}
        if (Input.GetKeyDown(KeyCode.DownArrow)
            || InputByButton.isDown)
        {
            Vector3 pos = transform.position;
            pos.y -= 1;
            transform.position = pos;
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                pos.y += 1;
                transform.position = pos;
            }
            else
            {
                ctrl.audioManager.PlayControl();
            }
        }
    }

    public void Init(Color color, Ctrl ctrl, GameManager gameManager)
    {
        foreach (Transform t in transform)
        {
            if(t.tag == "Block")
            {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }
        this.ctrl = ctrl;
        this.gameManager = gameManager;
    }
    public void Pause()
    {
        isPause = true;
    }
    public void Resume()
    {
        isPause = false;
    }
}
