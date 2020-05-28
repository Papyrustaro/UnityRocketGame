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
        Time.timeScale = 1f;
        SceneManager.LoadScene(this.gameObject.name);
    }

    public void PressBack()
    {
        
    }
}
