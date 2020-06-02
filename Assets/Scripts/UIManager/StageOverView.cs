using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// StageOverViewひとつひとつにアタッチする
/// </summary>
public class StageOverView : MonoBehaviour
{
    private void OnEnable()
    {
        this.transform.Find("GoButton").GetComponent<Button>().Select();
    }

    public void PressGoStage()
    {
        SEManager.PlaySE(SEManager.decision);
        StageManager.Instance.MoveAllMoving();
        if(this.gameObject.name == "Mission10" || this.gameObject.name == "Mission11")
        {
            BGMManager.PlayBGM(BGMManager.bossBGM0);
        }
        else
        {
            BGMManager.PlayBGM(BGMManager.missionBGM);
        }
        SceneManager.LoadScene(this.gameObject.name);
    }

    public void PressBack()
    {
        
    }
}
