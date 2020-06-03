using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class OptionManager : MonoBehaviour
{
    [SerializeField] private GameObject optionView;
    [SerializeField] private GameObject staffView;

    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;

    [SerializeField] private Button backFromViewStaffButton;
    [SerializeField] private Button resumeButton;

    [SerializeField] private Slider rotationSpeedSlider;
    [SerializeField] private InputField rotationSpeedInputField;
    [SerializeField] private Text rotationSpeedPlaceholderText;

    private void OnEnable()
    {
        this.resumeButton.Select();
        SetRotationSpeedUIByStaticData();
    }
    public void ChangeBGMVolume()
    {
        BGMManager.audioSource.volume = this.bgmVolumeSlider.value;
    }

    public void ChangeSEVolume()
    {
        SEManager.audioSource.volume = this.seVolumeSlider.value;
    }

    public void PressResume()
    {
        SEManager.PlaySE(SEManager.back);
        StageManager.Instance.MoveAllMoving();
        if (!StageManager.Instance.IsStageScene)
        {
            if(MenuUIManager.Instance.CurrentSelectType == E_MenuType.Option) MenuUIManager.Instance.OptionPanel.SetActive(true);
            OptionClickManager.Instance.OptionIcon.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }

    public void PressGoTitle()
    {
        //初期化処理
        BGMManager.PlayBGM(BGMManager.menuBGM);
        SEManager.PlaySE(SEManager.decision);
        SceneManager.LoadScene("Title");
    }

    public void ViewStaff()
    {
        SEManager.PlaySE(SEManager.decision);
        this.optionView.SetActive(false);
        this.staffView.SetActive(true);
        this.backFromViewStaffButton.Select();
    }

    public void BackFromViewStaff()
    {
        SEManager.PlaySE(SEManager.back);
        this.staffView.SetActive(false);
        this.optionView.SetActive(true);
        this.resumeButton.Select();
    }

    public void ChangeRotationSpeedBySlider()
    {
        StaticData.rotationSpeed = this.rotationSpeedSlider.value;
        this.rotationSpeedPlaceholderText.text = ((int)(this.rotationSpeedSlider.value)).ToString();
    }

    public void SetRotationSpeedUIByStaticData()
    {
        this.rotationSpeedSlider.value = StaticData.rotationSpeed;
        this.rotationSpeedPlaceholderText.text = ((int)StaticData.rotationSpeed).ToString();
    }
    public void ChangeRotationSpeedByInputField()
    {
        int speed;
        try
        {
            speed = int.Parse(this.rotationSpeedInputField.text);
        }
        catch (Exception)
        {
            return;
        }
        Debug.Log(speed.ToString());
        if(speed >= 150 && speed <= 1000)
        {
            this.rotationSpeedSlider.value = speed;
            StaticData.rotationSpeed = speed;
        }
    }
}
