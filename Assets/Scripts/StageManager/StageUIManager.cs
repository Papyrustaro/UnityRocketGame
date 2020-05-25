using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageUIManager : MonoBehaviour
{
    private GameObject pausePanel;
    private Button resumeButton;

    public static StageUIManager Instance { get; set; }

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

        this.pausePanel = this.transform.Find("PausePanel").gameObject;
        this.resumeButton = this.pausePanel.transform.Find("ResumeButton").GetComponent<Button>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            PauseAndResume();
        }
    }

    /// <summary>
    /// ゲーム進行中ならポーズ、ポーズ中ならゲーム再開
    /// </summary>
    public void PauseAndResume()
    {
        if (this.pausePanel.activeSelf)
        {
            this.pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
            this.pausePanel.SetActive(true);
            this.resumeButton.Select();
        }
    }

    public void LoadStageSelect()
    {
        StageManager.Instance.InitStageInstance();
        SceneManager.LoadScene("SelectMode");
        Time.timeScale = 1f;
    }
}
