using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Text scoreText;
    public static ScoreManager Instance { get; set; }

    public int HaveScore { get; private set; } = 0;

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
        this.HaveScore += addValue;
        this.scoreText.text = "Score: " + this.HaveScore.ToString();
    }
}
