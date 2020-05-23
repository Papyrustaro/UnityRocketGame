using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectModeManager : MonoBehaviour
{
    public static SelectModeManager Instance { get; private set; }

    public E_PlayMode PlayerSelectingMode { get; set; } = E_PlayMode.Other;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (this.PlayerSelectingMode)
            {
                case E_PlayMode.Other:
                    break;
                case E_PlayMode.MoveScreen_DefeatEnemy0:
                    SceneManager.LoadScene("MoveScreenSample");
                    break;
                case E_PlayMode.OneScreen_Survival0:
                    SceneManager.LoadScene("OS_Sample0");
                    break;
                case E_PlayMode.OneScreen_CollectItem0:
                    SceneManager.LoadScene("OC_Sample0");
                    break;
            }
        }
    }
}

public enum E_PlayMode
{
    OneScreen_Survival0,
    MoveScreen_DefeatEnemy0,
    Other,
    OneScreen_CollectItem0,
}
