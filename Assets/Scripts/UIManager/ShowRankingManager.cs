using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System;

public class ShowRankingManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> scrollViews = new List<GameObject>();
    [SerializeField] private List<Text> playerNamesTexts = new List<Text>();
    [SerializeField] private List<Text> clearTimesTexts = new List<Text>();
    [SerializeField] private int missionCount = 11;
    [SerializeField] private Text rankingTitleText;
    [SerializeField] private Button nextRankingIndexButton;
    private int showingMissionIndex = 0;

    public Text RankingTitleText => this.rankingTitleText;
    public int ShowingMissionIndex => this.showingMissionIndex;
    public Button NextRankingIndexButton => this.nextRankingIndexButton;


    public void SetRanking()
    {
        for(int i = 0; i < missionCount; i++)
        {
            if (StaticData.highRankResults.ContainsKey("Mission" + (i + 1).ToString()))
            {
                this.playerNamesTexts[i].text = StaticData.highRankResults["Mission" + (i + 1).ToString()].PlayerNameText;
                this.clearTimesTexts[i].text = StaticData.highRankResults["Mission" + (i + 1).ToString()].ResultTimeText;
            }
            else
            {
                StartCoroutine(SetHighRankingTexts(i));
            }
        }
    }

    public IEnumerator SetHighRankingTexts(int missionIndex)
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Mission" + (missionIndex + 1).ToString());
        List<NCMBObject> result = null;
        NCMBException error = null;

        if (missionIndex != 3) query.OrderByAscending("ClearTime"); //昇順
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
            this.SetHighRankingTexts(result, missionIndex);
        }
    }

    public void SetHighRankingTexts(List<NCMBObject> highRanks, int missionIndex)
    {
        string playerName = "";
        string resultTime = "";
        List<ResultDataNameAndTime> results = new List<ResultDataNameAndTime>();
        for (int i = 0; i < highRanks.Count; i++)
        {
            playerName += (i + 1).ToString() + ". " + highRanks[i]["PlayerName"].ToString() + "\n";
            resultTime += highRanks[i]["ClearTime"].ToString() + "\n";
        }
        this.playerNamesTexts[missionIndex].text = playerName;
        this.clearTimesTexts[missionIndex].text = resultTime;
        StaticData.highRankResults.Add("Mission" + (missionIndex + 1).ToString(), new ResultDataNameAndTime(playerName, resultTime));
    }

    public void PressNextMissionRankingButton()
    {
        SEManager.PlaySE(SEManager.select);
        this.scrollViews[this.showingMissionIndex].SetActive(false);
        this.showingMissionIndex++;
        if (this.showingMissionIndex > this.missionCount - 1) this.showingMissionIndex = 0;
        this.scrollViews[this.showingMissionIndex].SetActive(true);
        this.RankingTitleText.text = "ランキング(Mission" + (this.showingMissionIndex + 1).ToString() + ")";
    }

    public void PressBackMissionRankingButton()
    {
        SEManager.PlaySE(SEManager.select);
        this.scrollViews[this.showingMissionIndex].SetActive(false);
        this.showingMissionIndex--;
        if (this.showingMissionIndex < 0) this.showingMissionIndex = this.missionCount - 1;
        this.scrollViews[this.showingMissionIndex].SetActive(true);
        this.RankingTitleText.text = "ランキング(Mission" + (this.showingMissionIndex + 1).ToString() + ")";
    }

    public void Resume()
    {
        StageManager.Instance.IsStop = false;
        SEManager.PlaySE(SEManager.back);
        this.gameObject.SetActive(false);
    }
}
