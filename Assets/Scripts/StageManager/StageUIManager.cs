using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StageUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private Text rankingNameAndValueText;
    [SerializeField] private GameObject smogPanel;
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject timeText;

    private GameObject stageClearText;
    private GameObject gameOverText;

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

        /*this.pausePanel = this.transform.Find("PausePanel").gameObject;
        this.resumeButton = this.pausePanel.transform.Find("ResumeButton").GetComponent<Button>();
        this.rankingPanel = this.transform.Find("RankingPanel").gameObject;
        this.rankingNameAndValueText = this.rankingPanel.transform.Find("RankingScrollView/Viewport/ContentOfNameAndValueText").GetComponent<Text>();*/

        this.stageClearText = this.smogPanel.transform.Find("StageClearText").gameObject;
        this.gameOverText = this.smogPanel.transform.Find("GameOverText").gameObject;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            PauseAndResume();
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
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
            this.pausePanel.SetActive(true);
            this.resumeButton.Select();
        }
    }

    public void MoveSceneToStageSelect()
    {
        StageManager.Instance.InitStageInstance();
        SceneManager.LoadScene("SelectMode");
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
        string s = "";
        string sceneName = SceneManager.GetActiveScene().name;
        for (int i = 1; i <= 10; i++)
        {
            if (!PlayerPrefs.HasKey(sceneName + "PlayerName" + i)) break;
            s += (i + ". " + PlayerPrefs.GetString(sceneName + "PlayerName" + i)).ToString().PadRight(25) + PlayerPrefs.GetFloat(sceneName + "Time" + i).ToString() + "\n";
        }
        this.rankingNameAndValueText.text = s;
    }

    public void ShowRankingWhenStageClear()
    {
        Time.timeScale = 0f;
        //ランキングテキストの処理
        SetRankingText();

        StartCoroutine(DelayMethodRealTime(0.5f, () =>
        {
            this.scoreText.SetActive(false);
            this.timeText.SetActive(false);
            this.stageClearText.SetActive(true);
            this.smogPanel.SetActive(true);
        }));
        StartCoroutine(DelayMethodRealTime(1.5f, () =>
        {
            this.stageClearText.SetActive(false);
        }));
        StartCoroutine(DelayMethodRealTime(2.5f, () =>
        {
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
