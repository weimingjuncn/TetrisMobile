using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPause = true;//游戏是否暂停
    private Shape currentShape = null;
    private Ctrl ctrl;
    public Shape[] shapes;
    public Color[] colors;
    Transform blockHolder;

    int escapeTimes = 0;//退出输入计时

    private void Awake()
    {
        ctrl = GetComponent<Ctrl>();
        blockHolder = transform.Find("BlockHolder");
    }
    IEnumerator resetTimes()
    {
        yield return new WaitForSeconds(1);
        escapeTimes = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //这个地方可以写“再按一次退出”的提示
            Debug.Log("再按一次退出");
            ShowAndroidToastMessage("再按一次退出");
            escapeTimes++;
            Debug.Log(escapeTimes);

            StartCoroutine("resetTimes");
            if (escapeTimes > 1)
            {
                Debug.Log("退出");
                Application.Quit();
                escapeTimes = 0;
            }
        }
        if (isPause) return;
        if (currentShape == null)
        {
            SpawnShape();
        }
    }
    public void StartGame()
    {
        isPause = false;
        if (currentShape != null)
            currentShape.Resume();
    }
    public void PauseGame()
    {
        isPause = true;
        if (currentShape != null)
            currentShape.Pause();
    }
    void SpawnShape()
    {
        int index = Random.Range(0, shapes.Length);
        int indexColor = Random.Range(0, colors.Length);
        currentShape = GameObject.Instantiate(shapes[index]);
        currentShape.transform.parent = blockHolder;
        currentShape.Init(colors[indexColor], ctrl, this);
    }
    /// <summary>
    /// 方块落下来了
    /// </summary>
    public void FallDown()
    {
        currentShape = null;
        if (ctrl.model.isDataUpdate)
        {
            ctrl.view.UpdateGameUI(ctrl.model.Score,ctrl.model.HighestScore);
        }
        foreach(Transform t in blockHolder)
        {
            if (t.childCount <= 1)
            {
                Destroy(t.gameObject);
            }
        }
        if (ctrl.model.IsGameOver())
        {
            PauseGame();
            ctrl.view.ShowGameOverUI(ctrl.model.Score);
        }
    }
    public void ClearShape()
    {
        if(currentShape != null)
        {
            Destroy(currentShape.gameObject);
            currentShape = null;
        }
    }
    /// <summary>
    /// 弹出信息提示
    /// </summary>
    /// <param name="message">要弹出的信息</param>
    public void ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
