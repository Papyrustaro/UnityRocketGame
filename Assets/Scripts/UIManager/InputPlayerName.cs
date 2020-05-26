using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputPlayerName : MonoBehaviour
{
    private InputField inputField;

    private void Awake()
    {
        this.inputField = GetComponent<InputField>();
    }

    public void SetPlayerName()
    {
        StaticData.playerName = this.inputField.text;
        SceneManager.LoadScene("SelectMode");
    }
}
