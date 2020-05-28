using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject missionPanel;
    [SerializeField] private GameObject scoreAttackPanel;
    [SerializeField] private GameObject timeAttackPanel;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject manualPanel;

    public E_MenuType CurrentSelectType { get; set; } = E_MenuType.Other;

    public static MenuUIManager Instance { get; set; }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (this.CurrentSelectType)
            {
                case E_MenuType.Mission:
                    Debug.Log("mission");
                    break;
                case E_MenuType.ScoreAttack:
                    break;
                case E_MenuType.TimeAttack:
                    break;
                case E_MenuType.Manual:
                    break;
                case E_MenuType.Option:
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
}