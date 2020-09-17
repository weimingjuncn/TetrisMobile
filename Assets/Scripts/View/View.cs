using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class View : MonoBehaviour
{
    Ctrl ctrl;
    RectTransform logoName;
    RectTransform menuUI;
    RectTransform gameUI;
    RectTransform ctrlShapeUI;
    GameObject restartButton;
    GameObject gameOverUI;
    GameObject settingUI;
    GameObject rankUI;

    Text score;
    Text highestScore;
    Text gameOverScore;
    GameObject mute;

    Text rankScore;
    Text rankHighestScore;
    Text rankTimes;
    void Awake()
    {
        ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Ctrl>();
        logoName = transform.Find("Canvas/Logo") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        gameUI = transform.Find("Canvas/GameUI") as RectTransform;
        ctrlShapeUI = transform.Find("Canvas/CtrlShapeUI") as RectTransform;

        restartButton = transform.Find("Canvas/MenuUI/RestartButton").gameObject;
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        settingUI = transform.Find("Canvas/SettingUI").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;
        

        score = transform.Find("Canvas/GameUI/ScoreLabel/Text").GetComponent<Text>();
        highestScore = transform.Find("Canvas/GameUI/HighestScoreLabel/Text").GetComponent<Text>();
        gameOverScore = transform.Find("Canvas/GameOverUI/Text").GetComponent<Text>();

        mute = transform.Find("Canvas/SettingUI/AudioButton/Mute").gameObject;

        rankScore = transform.Find("Canvas/RankUI/ScoreLabel/Text").GetComponent<Text>();
        rankHighestScore = transform.Find("Canvas/RankUI/HighestScoreLabel/Text").GetComponent<Text>();
        rankTimes = transform.Find("Canvas/RankUI/TimesLabel/Text").GetComponent<Text>();
    }

    public void ShowMenu()
    {
        logoName.gameObject.SetActive(true);
        logoName.DOAnchorPosY(830.9f, 0.5f);
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(180, 0.5f);
    }
    public void HideMenu()
    {
        logoName.DOAnchorPosY(1509.1f, 0.5f)
            .OnComplete(delegate { logoName.gameObject.SetActive(false); });
        menuUI.DOAnchorPosY(-177.8f, 0.5f)
            .OnComplete(delegate { menuUI.gameObject.SetActive(false); });
    }
    public void UpdateGameUI(int score, int highestScore)
    {
        this.score.text = score.ToString();
        this.highestScore.text = highestScore.ToString();
    }
    public void ShowGameUI(int score=0, int highestScore=0)
    {
        this.score.text = score.ToString();
        this.highestScore.text = highestScore.ToString();
        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(0, 0.5f);
    }

    public void HideGameUI()
    {
        gameUI.DOAnchorPosY(863.7f, 0.5f)
            .OnComplete(delegate { gameUI.gameObject.SetActive(false); });
    }
    public void ShowCtrlShapeUI()
    {
        ctrlShapeUI.gameObject.SetActive(true);
        ctrlShapeUI.DOAnchorPosY(-957.2f, 0.5f);
    }
    public void HideCtrlShapeUI()
    {
        ctrlShapeUI.DOAnchorPosY(-1219, 0.5f)
            .OnComplete(delegate { ctrlShapeUI.gameObject.SetActive(false); });
    }
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }
    public void ShowGameOverUI(int score = 0)
    {
        gameOverUI.SetActive(true);
        gameOverScore.text = score.ToString();
    }
    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }
    public void OnHomeButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnSettingButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(true);
    }
    public void SetMuleActive(bool isActive)
    {
        mute.SetActive(isActive);
    }
    public void OnSettingUIClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(false);
    }
    //public void OnRankButtonClick()
    //{
    //    ctrl.audioManager.PlayCursor();
    //    rankUI.SetActive(true);
    //}
    public void ShowRankUI(int score,int highestScore,int times)
    {
        this.rankScore.text = score.ToString();
        this.rankHighestScore.text = highestScore.ToString();
        this.rankTimes.text = times.ToString();
        rankUI.SetActive(true);
    }
    public void OnRankUIClick()
    {
        rankUI.SetActive(false);
    }
}
