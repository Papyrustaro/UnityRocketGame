using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageManager : MonoBehaviour
{
    private StageOverViewsManager stageOverViewsManager;
    public static SelectStageManager Instance { get; set; }

    public int SelectingStageIndex { get; set; } = -1;

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
        this.stageOverViewsManager = GetComponent<StageOverViewsManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && this.SelectingStageIndex >= 0)
        {
            this.stageOverViewsManager.ShowStageOverView(this.SelectingStageIndex);
        }
    }

    public void BackFromShowOverView()
    {

        SEManager.PlaySE(SEManager.back);
        Time.timeScale = 1f;
        this.stageOverViewsManager.OverViews[this.SelectingStageIndex].SetActive(false);
        this.stageOverViewsManager.StageOverViewsPanel.SetActive(false);
        this.SelectingStageIndex = -1;
        
    }
}
