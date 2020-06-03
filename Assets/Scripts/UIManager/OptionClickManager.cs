using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionClickManager : MonoBehaviour
{
    public static OptionClickManager Instance { get; set; }
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject optionIcon;

    public GameObject OptionIcon => this.optionIcon;
    
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
    }

    public void ClickOptionIcon()
    {
        if (this.optionPanel.activeSelf)
        {
            this.optionPanel.SetActive(false);
            SEManager.PlaySE(SEManager.back);
            StageManager.Instance.MoveAllMoving();
        }
        else
        {
            //StageManager.Instance.IsPausing = true;
            StageManager.Instance.StopAllMoving();
            this.optionPanel.SetActive(true);
            this.resumeButton.Select();
            SEManager.PlaySE(SEManager.pause);
        }
        
    }


}
