using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputPlayerName : MonoBehaviour
{
    private InputField inputField;
    [SerializeField] private GameObject goMenuAnnounceText;

    private void Awake()
    {
        this.inputField = GetComponent<InputField>();
    }

    private void Start()
    {
        this.inputField.Select();
    }

    public void SetPlayerName()
    {
        if(this.inputField.text == "")
        {
            SEManager.PlaySE(SEManager.speedDown);
        }
        else
        {
            SEManager.PlaySE(SEManager.decision);
            StaticData.playerName = this.inputField.text;
            SceneManager.LoadScene("SelectMissionStage");
        }
    }

    public void ChangeInputText()
    {
        if(this.inputField.text == "")
        {
            this.goMenuAnnounceText.SetActive(false);
        }
        else
        {
            this.goMenuAnnounceText.SetActive(true);
        }
    }
}
