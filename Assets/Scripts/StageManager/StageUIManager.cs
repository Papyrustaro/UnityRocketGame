using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text;
using UnityEngine.Networking;
using NCMB;

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
    [SerializeField] private GameObject tweetResultButton;

    private GameObject stageClearText;
    private GameObject gameOverText;
    private ManualUIManager manualUIManager;

    private int thisTimePlayerRank = -1; //今回のプレイヤーのタイム順位

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
        if (!StageManager.Instance.IsPausing && StageManager.Instance.IsStop) return;
        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.LeftControl))
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
            StageManager.Instance.IsPausing = false;
            OptionClickManager.Instance.OptionIcon.SetActive(true);
            this.pausePanel.SetActive(false);
            SEManager.PlaySE(SEManager.back);
            StageManager.Instance.MoveAllMoving();
        }
        else
        {
            StageManager.Instance.IsPausing = true;
            StageManager.Instance.StopAllMoving();
            OptionClickManager.Instance.OptionIcon.SetActive(false);
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
        //StageManager.Instance.InitStageInstance();
        switch (playType)
        {
            case E_PlayType.Mission:
                BGMManager.PlayBGM(BGMManager.menuBGM);
                SceneManager.LoadScene("SelectMissionStage");
                break;
            case E_PlayType.ScoreAttack:
                SceneManager.LoadScene("SelectScoreAttackStage");
                break;
            case E_PlayType.TimeAttack:
                SceneManager.LoadScene("SelectTimeAttackStage");
                break;
        }
        StageManager.Instance.MoveAllMoving();
    }

    public void ContinueSameStage()
    {
        //StageManager.Instance.InitStageInstance();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StageManager.Instance.MoveAllMoving();
    }

    public IEnumerator SetHighRankingTextFromClearResult()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(SceneManager.GetActiveScene().name);
        List<NCMBObject> result = null;
        NCMBException error = null;

        if(TimeManager.Instance.CountTimeType == E_CountTimeType.CountUp) query.OrderByAscending("ClearTime"); //昇順
        else query.OrderByDescending("ClearTime"); //降順
        query.Limit = 10;

        query.FindAsync((List<NCMBObject> _result, NCMBException _error) =>
        {
            result = _result;
            error = _error;
        });

        //resultもしくはerrorが入るまで待機
        yield return new WaitWhile(() => result == null && error == null);

        //後続処理
        if(error == null)
        {
            this.SetHighRankingTextFromClearResult(result);
        }
        else
        {
            Debug.Log(error);
        }
    }

    public IEnumerator SetRecentClearTextFromClearResult()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(SceneManager.GetActiveScene().name);
        List<NCMBObject> result = null;
        NCMBException error = null;

        query.OrderByDescending("createDate");
        query.Limit = 9;

        query.FindAsync((List<NCMBObject> _result, NCMBException _error) =>
        {
            result = _result;
            error = _error;
        });

        //resultもしくはerrorが入るまで待機
        yield return new WaitWhile(() => result == null && error == null);

        //後続処理
        if (error == null)
        {
            this.SetRecentClearTextFromClearResult(result);
        }
        else
        {
            Debug.Log(error);
        }
    }

    public void SetRecentClearTextFromClearResult(List<NCMBObject> recentResults)
    {
        string playerName = "1. " + StaticData.playerName + "\n";
        string clearDate = DateTime.Now.ToString("yyyy/MM/dd") + "\n";

        for(int i = 0; i < recentResults.Count; i++)
        {
            playerName += (i + 2).ToString() + ". " + recentResults[i]["PlayerName"].ToString() + "\n";
            DateTime cDate = DateTime.Parse(recentResults[i].CreateDate.ToString());
            clearDate += cDate.ToString("yyyy/MM/dd") + "\n";
        }

        this.recentPlayerNameText.text = playerName;
        this.recentDateText.text = clearDate;

        if (StaticData.recentResults.ContainsKey(SceneManager.GetActiveScene().name))
        {
            StaticData.recentResults[SceneManager.GetActiveScene().name] = new ResultDataNameAndDate(playerName, clearDate);
        }
        else
        {
            StaticData.recentResults.Add(SceneManager.GetActiveScene().name, new ResultDataNameAndDate(playerName, clearDate));
        }
    }

    public IEnumerator SetHighRankingTextFromFailedResult()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(SceneManager.GetActiveScene().name);
        List<NCMBObject> result = null;
        NCMBException error = null;

        if (TimeManager.Instance.CountTimeType == E_CountTimeType.CountUp) query.OrderByAscending("ClearTime"); //昇順
        else query.OrderByDescending("ClearTime"); //降順
        query.Limit = 10;

        query.FindAsync((List<NCMBObject> _result, NCMBException _error) =>
        {
            result = _result;
            error = _error;
        });

        //resultもしくはerrorが入るまで待機
        yield return new WaitWhile(() => result == null && error == null);

        //後続処理
        if (error == null)
        {
            this.SetHighRankingTextFromFailedResult(result);
        }
    }

    public IEnumerator SetRecentClearTextFromFailedResult()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(SceneManager.GetActiveScene().name);
        List<NCMBObject> result = null;
        NCMBException error = null;

        query.OrderByDescending("createDate");
        query.Limit = 10;

        query.FindAsync((List<NCMBObject> _result, NCMBException _error) =>
        {
            result = _result;
            error = _error;
        });

        //resultもしくはerrorが入るまで待機
        yield return new WaitWhile(() => result == null && error == null);

        //後続処理
        if (error == null)
        {
            this.SetRecentClearTextFromFailedResult(result);
        }
    }

    public void SetRecentClearTextFromFailedResult(List<NCMBObject> recentResults)
    {
        string playerName = "";
        string clearDate = "";
        List<ResultDataNameAndDate> results = new List<ResultDataNameAndDate>();

        for (int i = 0; i < recentResults.Count; i++)
        {
            playerName += (i + 1).ToString() + ". " + recentResults[i]["PlayerName"].ToString() + "\n";
            DateTime cDate = DateTime.Parse(recentResults[i].CreateDate.ToString());
            clearDate += cDate.ToString("yyyy/MM/dd") + "\n";
        }

        this.recentPlayerNameText.text = playerName;
        this.recentDateText.text = clearDate;
        StaticData.recentResults.Add(SceneManager.GetActiveScene().name, new ResultDataNameAndDate(playerName, clearDate));
    }

    public void SetHighRankingTextFromFailedResult(List<NCMBObject> highRanks)
    {
        string playerName = "";
        string resultTime = "";
        List<ResultDataNameAndTime> results = new List<ResultDataNameAndTime>();
        for(int i = 0; i < highRanks.Count; i++)
        {
            playerName += (i + 1).ToString() + ". " + highRanks[i]["PlayerName"].ToString() + "\n";
            resultTime += highRanks[i]["ClearTime"].ToString() + "\n";
        }
        this.rankingPlayerNameText.text = playerName;
        this.rankingScoreOrTimeText.text = resultTime;
        StaticData.highRankResults.Add(SceneManager.GetActiveScene().name, new ResultDataNameAndTime(playerName, resultTime));
    }

    public void SetHighRankingTextFromClearResult(List<NCMBObject> highRanks)
    {
        bool rankined = false;
        string playerName = "";
        string resultTime = "";
        int highRanksCount = highRanks.Count;

        List<float> resultTimes = new List<float>();
        int thisTimeIndex = highRanksCount; //今回クリアしたプレイヤーの順位
        for(int i = 0; i < highRanksCount; i++)
        {
            if(TimeManager.Instance.CountTimeType == E_CountTimeType.CountUp)
            {
                if (float.Parse(highRanks[i]["ClearTime"].ToString()) > TimeManager.Instance.CountTime)
                {
                    thisTimeIndex = i;
                    break;
                }
            }
            else
            {
                if(float.Parse(highRanks[i]["ClearTime"].ToString()) < TimeManager.Instance.CountTime)
                {
                    thisTimeIndex = i;
                    break;
                }
            }
        }
        if (highRanksCount < 10) highRanksCount++;

        for(int i = 0; i < highRanksCount; i++)
        {
            if(i == thisTimeIndex) //playerの順位
            {
                playerName += (i + 1).ToString() + ". " + StaticData.playerName + "\n";
                resultTime += TimeManager.Instance.CountTime + "\n";
                this.thisTimePlayerRank = i + 1;
                rankined = true;
            }
            else if(rankined) //playerがランクインしたあとの
            {
                playerName += (i + 1).ToString() + ". " + highRanks[i-1]["PlayerName"].ToString() + "\n";
                resultTime += float.Parse((highRanks[i-1]["ClearTime"]).ToString()) + "\n";
            }
            else //playerがランクインする前
            {
                playerName += (i + 1).ToString() + ". " + highRanks[i]["PlayerName"].ToString() + "\n";
                resultTime += float.Parse((highRanks[i]["ClearTime"]).ToString()) + "\n";
            }
        }

        this.rankingPlayerNameText.text = playerName;
        this.rankingScoreOrTimeText.text = resultTime;
        if (StaticData.highRankResults.ContainsKey(SceneManager.GetActiveScene().name))
        {
            StaticData.highRankResults[SceneManager.GetActiveScene().name] = new ResultDataNameAndTime(playerName, resultTime);
        }
        else
        {
            StaticData.highRankResults.Add(SceneManager.GetActiveScene().name, new ResultDataNameAndTime(playerName, resultTime));
        }
    }
    /*public void SetRankingTextByPlayerPrefs()
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
    }*/

    public void SetResultAndShowUsedStaticData()
    {
        this.rankingPlayerNameText.text = StaticData.highRankResults[SceneManager.GetActiveScene().name].PlayerNameText;
        this.rankingScoreOrTimeText.text = StaticData.highRankResults[SceneManager.GetActiveScene().name].ResultTimeText;
        this.recentPlayerNameText.text = StaticData.recentResults[SceneManager.GetActiveScene().name].PlayerNameText;
        this.recentDateText.text = StaticData.recentResults[SceneManager.GetActiveScene().name].ClearDateText;

        this.playerResultText.text = StaticData.playerName + ": " + TimeManager.Instance.CountTime;
        StageManager.Instance.StopAllMoving();
        //StartCoroutine(DelayMethodRealTime(0.3f, () =>
        //{
        SEManager.PlaySE(SEManager.failed);
        this.scoreText.SetActive(false);
        this.timeText.SetActive(false);
        this.flagCountText.transform.gameObject.SetActive(false);
        this.gameOverText.SetActive(true);
        this.smogPanel.SetActive(true);
        //}));
        StartCoroutine(DelayMethodRealTime(0.3f, () =>
        {
            this.gameOverText.SetActive(false);
        }));
        StartCoroutine(DelayMethodRealTime(0.5f, () =>
        {
            SEManager.PlaySE(SEManager.getItem);
            this.rankingPanel.SetActive(true);
            this.tweetResultButton.SetActive(false);
            this.continueButton.Select();
        }));
    }

    public void TweetResult()
    {
        string message = SceneManager.GetActiveScene().name + "を" + StageManager.Instance.ResultTime + "でクリア!!" + " #UnderRocket #unityroom";
        Application.OpenURL("http://twitter.com/intent/tweet?text=" + UnityWebRequest.EscapeURL(message));
    }

    public void Tweeting()
    {
        string tweetText = "";
        if (this.thisTimePlayerRank != -1) tweetText = "【現在" + this.thisTimePlayerRank.ToString() + "位】";
        if(TimeManager.Instance.CountTimeType == E_CountTimeType.CountUp)
        {
            tweetText += SceneManager.GetActiveScene().name + "を" + StageManager.Instance.ResultTime.ToString() + "秒でクリア!!";
        }
        else
        {
            tweetText += SceneManager.GetActiveScene().name + "を" + StageManager.Instance.ResultTime.ToString() + "秒残しでクリア!!";
        }

        string url = "https://twitter.com/intent/tweet?"
            + "text=" + tweetText
            + "&url=" + "https://unityroom.com/games/underrocket"
            + "&hashtags=" + "UnderRocket,unityroom";

#if UNITY_EDITOR
        Application.OpenURL(url);
#elif UNITY_WEBGL
            // WebGLの場合は、ゲームプレイ画面と同じウィンドウでツイート画面が開かないよう、処理を変える
            Application.ExternalEval(string.Format("window.open('{0}','_blank')", url));
#else
            Application.OpenURL(url);
#endif
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
            this.scrollViewTitleText.text = "最近クリアした人";
        }
        else
        {
            this.rankingScrollView.SetActive(true);
            this.recentScrollView.SetActive(false);
            this.changeScrollViewButtonText.text = "最近クリアした人を表示";
            this.scrollViewTitleText.text = "ランキング";
        }
        //this.rankingScrollView.SetActive(!this.rankingScrollView.activeSelf);
        //this.recentScrollView.SetActive(!this.recentScrollView.activeSelf);
    }
    public IEnumerator SetAndShowRankingWhenFailed()
    {
        this.playerResultText.text = StaticData.playerName + ": " + TimeManager.Instance.CountTime;
        StageManager.Instance.StopAllMoving();
        //SetRankingTextByPlayerPrefs();
        //StartCoroutine(DelayMethodRealTime(0.5f, () =>
        //{
            SEManager.PlaySE(SEManager.failed);
            this.scoreText.SetActive(false);
            this.timeText.SetActive(false);
            this.flagCountText.transform.gameObject.SetActive(false);
            this.gameOverText.SetActive(true);
            this.smogPanel.SetActive(true);
        //}));
        StartCoroutine(DelayMethodRealTime(0.5f, () =>
        {
            this.gameOverText.SetActive(false);
        }));

        StartCoroutine(SetHighRankingTextFromFailedResult());
        yield return StartCoroutine(SetRecentClearTextFromFailedResult());

        SEManager.PlaySE(SEManager.getItem);
        this.rankingPanel.SetActive(true);
        this.tweetResultButton.SetActive(false);
        this.continueButton.Select();
    }

    public IEnumerator SetAndShowRankingWhenClear()
    {
        this.playerResultText.text = StaticData.playerName + ": " + TimeManager.Instance.CountTime;
        StageManager.Instance.StopAllMoving();

        //StartCoroutine(DelayMethodRealTime(0.5f, () =>
        //{
            SEManager.PlaySE(SEManager.success);
            this.scoreText.SetActive(false);
            this.timeText.SetActive(false);
            this.flagCountText.transform.gameObject.SetActive(false);
            this.stageClearText.SetActive(true);
            this.smogPanel.SetActive(true);
        //}));
        StartCoroutine(DelayMethodRealTime(0.5f, () =>
        {
            this.stageClearText.SetActive(false);
        }));

        StartCoroutine(SetHighRankingTextFromClearResult());
        yield return StartCoroutine(SetRecentClearTextFromClearResult());

        SEManager.PlaySE(SEManager.getItem);
        this.rankingPanel.SetActive(true);
        this.continueButton.Select();
        StageManager.Instance.SavePlayerResult();
    }

    IEnumerator DelayMethodRealTime(float waitTime, Action action)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        action();
    }
}
