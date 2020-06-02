using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance { get; private set; }

    public bool PlayerInStartObject { get; set; } = false;

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

    private void Start()
    {
        StageManager.Instance.MoveAllMoving();
    }

    public void SetPlayerName(string inputName)
    {
        StaticData.playerName = inputName;
        SceneManager.LoadScene("SelectMode");
    }

    void Update()
    {
        if (this.PlayerInStartObject && Input.GetKeyDown(KeyCode.Return))
        {
            //選択画面に飛ぶ
            SceneManager.LoadScene("SelectMode");
        }
    }
}

