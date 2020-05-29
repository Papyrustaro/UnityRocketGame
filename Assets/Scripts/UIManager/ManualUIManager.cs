using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] manual;
    [SerializeField] private Button nextButton;
    private int currentIndex = 0;
    private int manualLength;

    private void Awake()
    {
        this.manualLength = this.manual.Length;
        
    }

    private void OnEnable()
    {
        this.nextButton.Select();
    }

    public void FirstSelectButton()
    {
        this.nextButton.Select();
    }
    public void PressNextManual()
    {
        this.manual[this.currentIndex].SetActive(false);
        this.currentIndex = (++this.currentIndex) % this.manualLength;
        this.manual[this.currentIndex].SetActive(true);
        SEManager.PlaySE(SEManager.page);
    }

    public void PressBackManual()
    {
        this.manual[this.currentIndex].SetActive(false);
        this.currentIndex--;
        if (this.currentIndex < 0) this.currentIndex = this.manualLength - 1;
        this.manual[this.currentIndex].SetActive(true);
        SEManager.PlaySE(SEManager.page);
    }

    public void PressResume()
    {
        SEManager.PlaySE(SEManager.back);
        if (StageManager.Instance.IsStageScene)
        {
            StageUIManager.Instance.PausePanel.SetActive(true);
            StageUIManager.Instance.ResumeButton.Select();
        }
        else
        {
            Time.timeScale = 1f;
            MenuUIManager.Instance.ManualPanel.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }

}
