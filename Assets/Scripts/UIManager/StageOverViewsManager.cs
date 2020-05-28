using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOverViewsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] overViews;
    [SerializeField] private GameObject stageOverViewsPanel;

    public GameObject[] OverViews => this.overViews;
    public GameObject StageOverViewsPanel => this.stageOverViewsPanel;

    public void ShowStageOverView(int overViewIndex)
    {
        Time.timeScale = 0f;
        this.stageOverViewsPanel.SetActive(true);
        this.overViews[overViewIndex].SetActive(true);
        SEManager.PlaySE(SEManager.decision);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowStageOverView(0);
        }
    }
}
