using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour
{
    private Text timeText;

    [field: SerializeField]
    [field: RenameField("CountTimeType")]
    public E_CountTimeType CountTimeType { get; private set; }

    [field: SerializeField]
    [field: RenameField("LimitSecond")]
    public float LimitSecond { get; private set; } = 100000;

    public float CountTime { get; private set; }

    public static TimeManager Instance { get; set; }

    private bool needCount = true;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception();
        }

        this.timeText = GetComponent<Text>();
    }
    private void Start()
    {
        if(this.CountTimeType == E_CountTimeType.CountUp)
        {
            this.CountTime = 0f;
        }
        else
        {
            this.CountTime = this.LimitSecond;
        }
    }
    
    public void UpdateCountTime()
    {
        if (this.CountTimeType == E_CountTimeType.CountUp)
        {
            this.CountTime += Time.deltaTime;
        }
        else
        {
            this.CountTime -= Time.deltaTime;
            if (this.CountTime <= 0f && this.needCount)
            {
                this.CountTime = 0f;
                this.needCount = false;
            }
        }

        SetTimeText();
    }
    private void Update()
    {
        if (StageManager.Instance.IsStop) return;
        if(this.CountTimeType == E_CountTimeType.CountUp)
        {
            this.CountTime += Time.deltaTime;
        }
        else
        {
            this.CountTime -= Time.deltaTime;
            if(this.CountTime <= 0f && this.needCount)
            {
                this.CountTime = 0f;
                StageManager.Instance.GameOver();
                this.needCount = false;
                return;
            }
        }

        SetTimeText();
    }

    private void SetTimeText()
    {
        // this.timeText.text = "Time: " + Math.Round(this.CountTime).ToString();
        this.timeText.text = "Time: " + this.CountTime.ToString();
    }

    public void AddTime(float addTimeValue)
    {
        if(this.CountTimeType == E_CountTimeType.CountDown)
        {
            this.CountTime += addTimeValue;
        }
        else
        {
            this.CountTime -= addTimeValue;
            if (this.CountTime < 0f) this.CountTime = 0f;
        }
        SetTimeText();
    }
}

public enum E_CountTimeType
{
    CountUp,
    CountDown
}