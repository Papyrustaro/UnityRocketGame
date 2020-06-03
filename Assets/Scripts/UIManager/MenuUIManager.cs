using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private GameObject scoreAttackPanel;
    [SerializeField] private GameObject timeAttackPanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject manualPanel;
    [SerializeField] private GameObject manual;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private GameObject ranking;

    private ShowRankingManager showRankingManager;


    /// <summary>
    /// ランキングを一度でも表示したかどうか
    /// </summary>
    private bool showedRanking = false;

    public E_MenuType CurrentSelectType { get; set; } = E_MenuType.Other;

    public static MenuUIManager Instance { get; set; }

    public float CountTime { get; set; } = 0f;

    public GameObject ManualPanel => this.manualPanel;
    public GameObject OptionPanel => this.optionPanel;

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
        this.showRankingManager = this.ranking.GetComponent<ShowRankingManager>();
    }

    private void Update()
    {
        if(this.CountTime < 1f) this.CountTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (this.CurrentSelectType)
            {
                case E_MenuType.Mission:
                    SEManager.PlaySE(SEManager.decision);
                    SceneManager.LoadScene("SelectMissionStage");
                    break;
                case E_MenuType.ScoreAttack:
                    SEManager.PlaySE(SEManager.decision);
                    SceneManager.LoadScene("SelectScoreAttackStage");
                    break;
                case E_MenuType.TimeAttack:
                    SEManager.PlaySE(SEManager.decision);
                    SceneManager.LoadScene("SelectTimeAttackStage");
                    break;
                case E_MenuType.Manual:
                    if(CountTime > 0.3f)
                    {
                        OptionClickManager.Instance.OptionIcon.SetActive(false);
                        SEManager.PlaySE(SEManager.decision);
                        this.manualPanel.SetActive(false);
                        this.manual.SetActive(true);
                        StageManager.Instance.StopAllMoving();
                        this.CountTime = 0f;
                    }
                    break;
                case E_MenuType.Option:
                    if (CountTime > 0.3f)
                    {
                        OptionClickManager.Instance.OptionIcon.SetActive(false);
                        SEManager.PlaySE(SEManager.decision);
                        this.optionPanel.SetActive(false);
                        this.option.SetActive(true);
                        StageManager.Instance.StopAllMoving();
                        this.CountTime = 0f;
                    }
                    break;
                case E_MenuType.Ranking:
                    if(CountTime > 0.3f)
                    {
                        OptionClickManager.Instance.OptionIcon.SetActive(false);
                        SEManager.PlaySE(SEManager.decision);
                        this.rankingPanel.SetActive(false);
                        this.ranking.SetActive(true);
                        this.showRankingManager.RankingTitleText.text = "ランキング(Mission" + (this.showRankingManager.ShowingMissionIndex + 1).ToString() + ")";
                        this.showRankingManager.NextRankingIndexButton.Select();
                        if (!this.showedRanking)
                        {
                            this.showRankingManager.SetRanking();
                        }
                        StageManager.Instance.StopAllMoving();
                        this.CountTime = 0f;
                    }
                    break;
            }
        }
    }

    public void OpenAndClosePanel(E_MenuType menuType, bool wantOpen)
    {
        if (wantOpen) this.CurrentSelectType = menuType;
        else this.CurrentSelectType = E_MenuType.Other;

        switch (menuType)
        {
            case E_MenuType.Manual:
                this.manualPanel.SetActive(wantOpen);
                break;
            case E_MenuType.Mission:
                this.missionPanel.SetActive(wantOpen);
                break;
            case E_MenuType.Option:
                this.optionPanel.SetActive(wantOpen);
                break;
            case E_MenuType.Ranking:
                this.rankingPanel.SetActive(wantOpen);
                break;
            case E_MenuType.ScoreAttack:
                this.scoreAttackPanel.SetActive(wantOpen);
                break;
            case E_MenuType.TimeAttack:
                this.timeAttackPanel.SetActive(wantOpen);
                break;
        }
    }
}

public enum E_MenuType
{
    Mission,
    TimeAttack,
    ScoreAttack,
    Option,
    Manual,
    Other,
    Ranking,
}