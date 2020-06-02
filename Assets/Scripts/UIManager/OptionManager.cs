using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    [SerializeField] private GameObject optionView;
    [SerializeField] private GameObject staffView;

    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;

    [SerializeField] private Button backFromViewStaffButton;
    [SerializeField] private Button resumeButton;

    private void OnEnable()
    {
        this.resumeButton.Select();
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
        MenuUIManager.Instance.OptionPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void PressGoTitle()
    {
        //初期化処理
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
}
