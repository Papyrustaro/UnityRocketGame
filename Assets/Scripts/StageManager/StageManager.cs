using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SetRanking();
        StageUIManager.Instance.ShowRankingWhenStageClear();
        StopAllMoving();
    }

    public void SetRanking()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if(PlayType == E_PlayType.TimeAttack)
        {
            if(TimeManager.Instance.CountTimeType == E_CountTimeType.CountDown)
            {
                throw new System.Exception();
            }
            else
            {
                float time = TimeManager.Instance.CountTime + Time.deltaTime;
                float[] rankingTime = new float[11];
                for (int i = 1; i <= 10; i++)
                {
                    rankingTime[i] = PlayerPrefs.GetFloat(sceneName + "Time" + i);
                }

                for (int i = 1; i <= 10; i++)
                {
                    if (rankingTime[i] == 0f || rankingTime[i] > time)
                    {
                        for (int j = 10; j >= i; j--)
                        {
                            if (rankingTime[j] == 0f) continue;
                            PlayerPrefs.SetString(sceneName + "PlayerName" + (j + 1), PlayerPrefs.GetString(sceneName + "PlayerName" + j));
                            PlayerPrefs.SetFloat(sceneName + "Time" + (j + 1), rankingTime[j]);
                        }
                        PlayerPrefs.SetString(sceneName + "PlayerName" + i, StaticData.playerName);
                        PlayerPrefs.SetFloat(sceneName + "Time" + i, time);
                        break;
                    }
                }
            }
        }else if(PlayType == E_PlayType.ScoreAttack)
        {
            int score = ScoreManager.Instance.HaveScore;
            int[] rankingScore = new int[11];
            for(int i = 1; i <= 10; i++)
            {
                rankingScore[i] = PlayerPrefs.GetInt(sceneName + "Score" + i);
            }

            for(int i = 1; i <= 10; i++)
            {
                if(rankingScore[i] == 0 || rankingScore[i] < score)
                {
                    for(int j = 10; j >= i; j--)
                    {
                        if (rankingScore[j] == 0) continue;
                        PlayerPrefs.SetString(sceneName + "PlayerName" + (j + 1), PlayerPrefs.GetString(sceneName + "PlayerName" + j));
                        PlayerPrefs.SetInt(sceneName + "Score" + (j + 1), rankingScore[j]);
                    }
                    PlayerPrefs.SetString(sceneName + "PlayerName" + i, StaticData.playerName);
                    PlayerPrefs.SetInt(sceneName + "Score" + i, score);
                    break;
                }
            }
        }
        PlayerPrefs.Save();
    }

    public void GameOver()
    {
        SetRanking();
        StageUIManager.Instance.GameOver();
        StopAllMoving();
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
    Other
}

public enum E_PlayType
{
    Mission,
    ScoreAttack,
    TimeAttack,
    Other
}