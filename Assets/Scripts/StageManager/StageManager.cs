using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StageManager : MonoBehaviour
{
    [SerializeField] private E_ClearFlagType clearFlagType;
    [SerializeField] private E_PlayType playType;
    [SerializeField] private bool isStageScene = true;

    public static StageManager Instance { get; set; }

    public bool IsStageScene => this.isStageScene;

    //public GameObject PlayerPrefab { get; set; }
    public E_ClearFlagType ClearFlagType => this.clearFlagType;

    public E_PlayType PlayType => this.playType;

    public float ResultTime { get; set; } = 0f;

    /// <summary>
    /// ゲームが一時停止されているかどうか(pause、カウントダウン、クリア演出時等)
    /// </summary>
    public bool IsStop { get; set; } = false;

    private void Awake()
    {
        if(StageManager.Instance == null)
        {
            StageManager.Instance = this;
        }
        else
        {
            throw new System.Exception();
        }
    }

    private void Start()
    {
        StaticData.playingStageSceneName = SceneManager.GetActiveScene().name;
    }

    public void StopAllMoving()
    {
        Time.timeScale = 0f;
        this.IsStop = true;
    }

    public void StageClear()
    {
        //InitStageInstance();
        //SceneManager.LoadScene("GameClear");
        StopAllMoving();
        TimeManager.Instance.UpdateCountTime();
        //TimeManager.Instance.AddTime(Time.deltaTime);
        SetRanking();
        StageUIManager.Instance.ShowRankingWhenStageClear();
        //StopAllMoving();
    }

    public void SetRanking()
    {
        this.ResultTime = TimeManager.Instance.CountTime;
        string sceneName = SceneManager.GetActiveScene().name;
        if (TimeManager.Instance.CountTimeType == E_CountTimeType.CountUp)
        {
            //float time = TimeManager.Instance.CountTime + Time.deltaTime;
            float time = TimeManager.Instance.CountTime;
            float[] rankingTime = new float[11];
            for (int i = 1; i <= 10; i++)
            {
                rankingTime[i] = PlayerPrefs.GetFloat(sceneName + "RankingTime" + i);
            }

            for (int i = 1; i <= 10; i++)
            {
                if (rankingTime[i] == 0f || rankingTime[i] > time)
                {
                    for (int j = 10; j >= i; j--)
                    {
                        if (rankingTime[j] == 0f) continue;
                        PlayerPrefs.SetString(sceneName + "RankingPlayerName" + (j + 1), PlayerPrefs.GetString(sceneName + "RankingPlayerName" + j));
                        PlayerPrefs.SetFloat(sceneName + "RankingTime" + (j + 1), rankingTime[j]);
                    }
                    PlayerPrefs.SetString(sceneName + "RankingPlayerName" + i, StaticData.playerName);
                    PlayerPrefs.SetFloat(sceneName + "RankingTime" + i, time);
                    break;
                }
            }
        }
        else
        {
            //float time = TimeManager.Instance.CountTime - Time.deltaTime;
            float time = TimeManager.Instance.CountTime;
            float[] rankingTime = new float[11];
            for (int i = 1; i <= 10; i++)
            {
                rankingTime[i] = PlayerPrefs.GetFloat(sceneName + "RankingTime" + i);
            }

            for (int i = 1; i <= 10; i++)
            {
                if (rankingTime[i] == 0f || rankingTime[i] < time)
                {
                    for (int j = 10; j >= i; j--)
                    {
                        if (rankingTime[j] == 0f) continue;
                        PlayerPrefs.SetString(sceneName + "RankingPlayerName" + (j + 1), PlayerPrefs.GetString(sceneName + "RankingPlayerName" + j));
                        PlayerPrefs.SetFloat(sceneName + "RankingTime" + (j + 1), rankingTime[j]);
                    }
                    PlayerPrefs.SetString(sceneName + "RankingPlayerName" + i, StaticData.playerName);
                    PlayerPrefs.SetFloat(sceneName + "RankingTime" + i, time);
                    break;
                }
            }
        }
        DateTime dateTime = DateTime.Now;
        for (int i = 9; i >= 1; i--)
        {
            if (!PlayerPrefs.HasKey(sceneName + "RecentPlayerName" + i)) continue;
            PlayerPrefs.SetString(sceneName + "RecentPlayerName" + (i + 1), PlayerPrefs.GetString(sceneName + "RecentPlayerName" + i));
            PlayerPrefs.SetString(sceneName + "RecentDate" + (i + 1), PlayerPrefs.GetString(sceneName + "RecentDate" + i));
        }
        PlayerPrefs.SetString(sceneName + "RecentPlayerName" + 1, StaticData.playerName);
        PlayerPrefs.SetString(sceneName + "RecentDate" + 1, dateTime.Year.ToString() + "/" + dateTime.Month.ToString() + "/" + dateTime.Day.ToString());
        PlayerPrefs.Save();
    }

    public void GameOver()
    {
        //SetRanking();
        StopAllMoving();
        TimeManager.Instance.UpdateCountTime();
        //TimeManager.Instance.AddTime(Time.deltaTime);
        StageUIManager.Instance.GameOver();
        //StopAllMoving();
    }

    public void InitStageInstance()
    {
        StageManager.Instance = null;
        ClearFlag_DefeatEnemy.Instance = null;
        ClearFlag_CollectItem.Instance = null;
        ScoreManager.Instance = null;
        TimeManager.Instance = null;
        StageUIManager.Instance = null;
    }
}

/// <summary>
/// クリア条件の種類
/// </summary>
public enum E_ClearFlagType
{
    DefeatEnemy,
    CollectItem,
    Survival,
    MoveToGoal,
    BreakTarget,
    Other
}

public enum E_PlayType
{
    Mission,
    ScoreAttack,
    TimeAttack,
    Other
}