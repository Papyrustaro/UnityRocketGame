using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] E_ClearFlagType clearFlagType;

    public static StageManager Instance { get; set; }

    //public GameObject PlayerPrefab { get; set; }
    public E_ClearFlagType ClearFlagType => this.clearFlagType;

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
    }

    public void StageClear()
    {
        InitStageInstance();
        SceneManager.LoadScene("GameClear");
        Time.timeScale = 1f;
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