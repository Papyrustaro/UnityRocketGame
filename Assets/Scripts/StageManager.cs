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

    public void StageClear()
    {
        //ステージクリア処理
        StageManager.Instance = null;
        ClearFlag_DefeatEnemy.Instance = null;
        ClearFlag_CollectItem.Instance = null;
        SceneManager.LoadScene("GameClear");
    }

    public void GameOver()
    {
        //gameOver処理
        StageManager.Instance = null;
        ClearFlag_DefeatEnemy.Instance = null;
        ClearFlag_CollectItem.Instance = null;
        SceneManager.LoadScene("GameOver");
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