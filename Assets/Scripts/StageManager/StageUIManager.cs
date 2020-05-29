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
        if(StageManager.Instance.PlayType == E_PlayType.TimeAttack)
        {
            string playerNameText = "";
            string timeText = "";
            for (int i = 1; i <= 10; i++)
            {
                if (!PlayerPrefs.HasKey(sceneName + "PlayerName" + i)) break;
                playerNameText += i + ". " + PlayerPrefs.GetString(sceneName + "PlayerName" + i) + "\n";
                timeText += PlayerPrefs.GetFloat(sceneName + "Time" + i).ToString() + "\n";
            }
            this.rankingPlayerNameText.text = playerNameText;
            this.rankingScoreOrTimeText.text = timeText;

            this.playerResultText.text = StaticData.playerName + ": " + TimeManager.Instance.CountTime;
        }
        else if(StageManager.Instance.PlayType == E_PlayType.ScoreAttack)
        {
            string playerNameText = "";
            string scoreText = "";
            for (int i = 1; i <= 10; i++)
            {
                if (!PlayerPrefs.HasKey(sceneName + "PlayerName" + i)) break;
                playerNameText += i + ". " + PlayerPrefs.GetString(sceneName + "PlayerName" + i) + "\n";
                scoreText += PlayerPrefs.GetFloat(sceneName + "Score" + i).ToString() + "\n";
            }
            this.rankingPlayerNameText.text = playerNameText;
            this.rankingScoreOrTimeText.text = scoreText;

            this.playerResultText.text = StaticData.playerName + ": " + ScoreManager.Instance.HaveScore;
        }


    }

    public void GameOver()
    {
        StageManager.Instance.StopAllMoving();
        StartCoroutine(DelayMethodRealTime(0.5f, () =>
        {
            SEManager.PlaySE(SEManager.failed);
            this.scoreText.SetActive(false);
            this.timeText.SetActive(false);
            this.flagCountText.transform.gameObject.SetActive(false);
            this.gameOverText.SetActive(true);
            this.smogPanel.SetActive(true);
        }));
        StartCoroutine(DelayMethodRealTime(1.5f, () =>
        {
            this.gameOverText.SetActive(false);
        }));
        StartCoroutine(DelayMethodRealTime(2.5f, () =>
        {
            SEManager.PlaySE(SEManager.getItem);
            this.rankingPanel.SetActive(true);
            if (StageManager.Instance.PlayType == E_PlayType.Mission)
            {
                this.rankingPanel.transform.Find("RankingScrollView").gameObject.SetActive(false);
                this.rankingPanel.transform.Find("RankingTitleText").gameObject.SetActive(false);
                this.rankingPanel.transform.Find("PlayerResultText").gameObject.SetActive(false);
            }
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
        StartCoroutine(DelayMethodRealTime(1.5f, () =>
        {
            this.stageClearText.SetActive(false);
        }));
        StartCoroutine(DelayMethodRealTime(2.5f, () =>
        {
            SEManager.PlaySE(SEManager.getItem);
            this.rankingPanel.SetActive(true);
            if (StageManager.Instance.PlayType == E_PlayType.Mission)
            {
                this.rankingPanel.transform.Find("RankingScrollView").gameObject.SetActive(false);
                this.rankingPanel.transform.Find("RankingTitleText").gameObject.SetActive(false);
                this.rankingPanel.transform.Find("PlayerResultText").gameObject.SetActive(false);
            }
            this.continueButton.Select();
        }));
    }

    IEnumerator DelayMethodRealTime(float waitTime, Action action)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        action();
    }
}
