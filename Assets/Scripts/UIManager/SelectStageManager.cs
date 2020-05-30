using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStageManager : MonoBehaviour
{
    [SerializeField] private E_PlayType playType;
    [SerializeField] private GameObject[] stageSelectPanels;
    public static SelectStageManager Instance { get; set; }

    public int CurrentSelectIndex { get; set; } = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(this.CurrentSelectIndex >= 0)
            {
                //初期化処理
                InitBeforeGoStage();
                SEManager.PlaySE(SEManager.decision);
                switch (this.playType)
                {
                    case E_PlayType.Mission:
                        //index+1かな
                        if(this.CurrentSelectIndex == 9)
                        {
                            BGMManager.PlayBGM(BGMManager.bossBGM1);
                        }
                        else
                        {
                            BGMManager.PlayBGM(BGMManager.missionBGM);
                        }
                        SceneManager.LoadScene("Mission" + (this.CurrentSelectIndex + 1).ToString());

                        break;
                    case E_PlayType.ScoreAttack:
                        SceneManager.LoadScene("ScoreAttack" + (this.CurrentSelectIndex + 1).ToString());
                        break;
                    case E_PlayType.TimeAttack:
                        SceneManager.LoadScene("TimeAttack" + (this.CurrentSelectIndex + 1).ToString());
                        break;
                }
            }
        }
    }

    private void InitBeforeGoStage()
    {
        Instance = null;
    }

    public void OpenAndClosePanel(int stageIndex, bool wantOpen)
    {
        if (wantOpen) this.CurrentSelectIndex = stageIndex;
        else this.CurrentSelectIndex = -1;

        this.stageSelectPanels[stageIndex].SetActive(wantOpen);
    }
}
