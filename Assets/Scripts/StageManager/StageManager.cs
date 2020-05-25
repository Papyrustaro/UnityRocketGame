using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private E_ClearFlagType clearFlagType;
    [SerializeField] private E_PlayType playType;

    public static StageManager Instance { get; set; }

    //public GameObject PlayerPrefab { get; set; }
    public E_ClearFlagType ClearFlagType => this.clearFlagType;

    public E_PlayType PlayType => this.playType;

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

        //Debug.Log(SceneManager.GetActiveScene().name);
    }

    public void StageClear()
    {
        //InitStageInstance();
        //SceneManager.LoadScene("GameClear");
        SetRanking();
        StageUIManager.Instance.ShowRankingWhenStageClear();
        Time.timeScale = 0f;
    }

    public void SetRanking()
    {
        float time = TimeManager.Instance.CountTime;
        string sceneName = SceneManager.GetActiveScene().name;
        float[] rankingTime = new float[10];
        for(int i = 1; i <= 10; i++)
        {
            rankingTime[i - 1] = PlayerPrefs.GetFloat(sceneName + "Time" + i);
        }

        for(int i = 1; i <= 10; i++)
        {
            if(rankingTime[i - 1] == 0f || rankingTime[i - 1] > time)
            {
                for(int j = 10; j > i; j--)
                {
                    if (rankingTime[j - 1] == 0f) continue;
                    PlayerPrefs.SetString(sceneName + "PlayerName" + j, PlayerPrefs.GetString(sceneName + "PlayerName" + (j - 1)));
                    PlayerPrefs.SetFloat(sceneName + "Time" + j, rankingTime[j - 1]);
                }
                PlayerPrefs.SetString(sceneName + "PlayerName" + i, StaticData.playerName);
                PlayerPrefs.SetFloat(sceneName + "Time" + i, time);
                break;
            }
        }
        PlayerPrefs.Save();
    }

    public void GameOver()
    {
        InitStageInstance();
        SceneManager.LoadScene("GameOver");
        Time.timeScale = 1f;
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
    MoveToGoal
}

public enum E_PlayType
{
    Mission,
    ScoreAttack,
    TimeAttack
}