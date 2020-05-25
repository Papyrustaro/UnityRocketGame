using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int haveScore = 0;
    private Text scoreText;
    public static ScoreManager Instance { get; set; }

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
        this.scoreText = GetComponent<Text>();
    }


    public void AddScore(int addValue)
    {
        this.haveScore += addValue;
        this.scoreText.text = "Score: " + this.haveScore.ToString();
    }
}
