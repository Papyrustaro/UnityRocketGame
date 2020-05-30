using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text;

public class StageUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private Text rankingPlayerNameText;
    [SerializeField] private Text rankingScoreOrTimeText;
    [SerializeField] private GameObject smogPanel;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject timeText;
    [SerializeField] private Text playerResultText;
    [SerializeField] private GameObject manualPanel;
    [SerializeField] private Text flagCountText;
    [SerializeField] private Text recentPlayerNameText;
    [SerializeField] private Text recentDateText;
    [SerializeField] private GameObject rankingScrollView;
    [SerializeField] private GameObject recentScrollView;
    [SerializeField] private Text changeScrollViewButtonText;
    [SerializeField] private Text scrollViewTitleText;

    private GameObject stageClearText;
    private GameObject gameOverText;
    private ManualUIManager manualUIManager;

    public GameObject PausePanel => this.pausePanel;
    public Button ResumeButton => this.resumeButton;
    public static StageUIManager Instance { get; set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception();
        }

        this.stageClearText = this.smogPanel.transform.Find("StageClearText").gameObject;
        this.gameOverText = this.smogPanel.transform.Find("GameOverText").gameObject;
        this.manualUIManager = this.manualPanel.GetComponent<ManualUIManager>();
    }

    private void Start()
    {
        this.UpdateFlagText();
    }

    public void Update()
    {
        if (Time.timeScale == 0f) return;
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            PauseAndResume();
        }
    }

    public void UpdateFlagText()
    {
        this.flagCountText.text = "";
        switch (StageManager.Instance.ClearFlagType)
        {
            case E_ClearFlagType.CollectItem:
                this.flagCountText.text = "残り: " + ClearFlag_CollectItem.Instance.RemainItemCount;
                break;
            case E_ClearFlagType.BreakTarget:
                this.flagCountText.text = "残り: " + ClearFlag_BreakTarget.Instance.RemainTargetCount;
                break;
        }
    }

    /// <summary>
    /// ゲーム進行中ならポーズ、ポーズ中ならゲーム再開
    /// </summary>
    public void PauseAndResume()
    {
        if (this.pausePanel.activeSelf)
        {
            this.pausePanel.SetActive(false);
            SEManager.PlaySE(SEManager.back);
            Time.timeScale = 1f;
        }
        else
        {
            StageManager.Instance.StopAllMoving();
            this.pausePanel.SetActive(true);
            this.resumeButton.Select();
            SEManager.PlaySE(SEManager.pause);
        }
    }

    public void ViewManual()
    {
        this.pausePanel.SetActive(false);
        this.manualPanel.SetActive(true);
        this.manualUIManager.FirstSelectButton();
    }


    public void MoveSceneToStageSelect()
    {
        E_PlayType playType = StageManager.Instance.PlayType;
        StageManager.Instance.InitStageInstance();
        switch (playType)
        {
            case E_PlayType.Mission:
                SceneManager.LoadScene("SelectMissionStage");
                break;
            case E_PlayType.ScoreAttack:
                SceneManager.LoadScene("SelectScoreAttackStage");
                break;
            case E_PlayType.TimeAttack:
                SceneManager.LoadScene("SelectTimeAttackStage");
                break;
        }
        Time.timeScale = 1f;
    }

    public void ContinueSameStage()
    {
        StageManager.Instance.InitStageInstance();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void SetRankingText()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string playerNameText = "";
        string timeText = "";
        for (int i = 1; i <= 10; i++)
        {
            if (!PlayerPrefs.HasKey(sceneName + "RankingPlayerName" + i)) break;
            playerNameText += i + ". " + PlayerPrefs.GetString(sceneName + "RankingPlayerName" + i) + "\n";
            timeText += PlayerPrefs.GetFloat(sceneName + "RankingTime" + i).ToString() + "\n";
        }
        this.rankingPlayerNameText.text = playerNameText;
        this.rankingScoreOrTimeText.text = timeText;

        this.playerResultText.text = StaticData.playerName + ": " + TimeManager.Instance.CountTime;


        string recentPlayerNameText = "";
        string dateText = "";
        for (int i = 1; i <= 10; i++)
        {
            if (!PlayerPrefs.HasKey(sceneName + "RecentPlayerName" + i)) break;
            recentPlayerNameText += i + ". " + PlayerPrefs.GetString(sceneName + "RecentPlayerName" + i) + "\n";
            dateText += PlayerPrefs.GetString(sceneName + "RecentDate" + i) + "\n";
        }
        this.recentPlayerNameText.text = recentPlayerNameText;
        this.recentDateText.text = dateText;


    }

    public void ChangeScrollView()
    {
        SEManager.PlaySE(SEManager.decision);
        bool rankingScrollViewActive = this.rankingScrollView.activeSelf;
        if (rankingScrollViewActive)
        {
            this.rankingScrollView.SetActive(false);
            this.recentScrollView.SetActive(true);
            this.changeScrollViewButtonText.text = "ランキングを表示";
            this.scrollViewTitleText.text = "最近遊んだプレイヤー";
        }
        else
        {
            this.rankingScrollView.SetActive(true);
            this.recentScrollView.SetActive(false);
            this.changeScrollViewButtonText.text = "最近遊んだ人を表示";
            this.scrollViewTitleText.text = "ランキング";
        }
        //this.rankingScrollView.SetActive(!this.rankingScrollView.activeSelf);
        //this.recentScrollView.SetActive(!this.recentScrollView.activeSelf);
    }
    public void GameOver()
    {
        StageManager.Instance.StopAllMoving();
        SetRankingText();
        StartCoroutine(DelayMethodRealTime(0.5f, () =>
        {
            SEManager.PlaySE(SEManager.failed);
            this.scoreText.SetActive(false);
            this.timeText.SetActive(false);
            this.flagCountText.transform.gameObject.SetActive(false);
            this.gameOverText.SetActive(true);
            this.smogPanel.SetActive(true);
        }));
        StartCoroutine(DelayMethodRealTime(1f, () =>
        {
            this.gameOverText.SetActive(false);
        }));
        StartCoroutine(DelayMethodRealTime(1.5f, () =>
        {
            SEManager.PlaySE(SEManager.getItem);
            this.rankingPanel.SetActive(true);
            this.continueButton.Select();
        }));
    }

    public void ShowRankingWhenStageClear()
    {
        StageManager.Instance.StopAllMoving();
        //ランキングテキストの処理
        SetRankingText();

        StartCoroutine(DelayMethodRealTime(0.5f, () =>
        {
            SEManager.PlaySE(SEManager.success);
            this.scoreText.SetActive(false);
            this.timeText.SetActive(false);
            this.flagCountText.transform.gameObject.SetActive(false);
            this.stageClearText.SetActive(true);
            this.smogPanel.SetActive(true);
        }));
        StartCoroutine(DelayMethodRealTime(1f, () =>
        {
            this.stageClearText.SetActive(false);
        }));
        StartCoroutine(DelayMethodRealTime(1.5f, () =>
        {
            SEManager.PlaySE(SEManager.getItem);
            this.rankingPanel.SetActive(true);
            this.continueButton.Select();
        }));
    }

    IEnumerator DelayMethodRealTime(float waitTime, Action action)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        action();
    }
}
