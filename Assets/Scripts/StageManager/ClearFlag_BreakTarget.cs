using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFlag_BreakTarget : MonoBehaviour
{
    [SerializeField] private int targetCount = 3;

    public static ClearFlag_BreakTarget Instance { get; set; }

    public int RemainTargetCount { get; set; }

    private void Awake()
    {
        if (ClearFlag_BreakTarget.Instance == null)
        {
            ClearFlag_BreakTarget.Instance = this;
        }
        else
        {
            throw new System.Exception();
        }
        this.RemainTargetCount = this.targetCount;
    }

    public void BreakTarget()
    {
        this.RemainTargetCount--;
        StageUIManager.Instance.UpdateFlagText();
        if (this.RemainTargetCount <= 0)
        {
            StageManager.Instance.StageClear();
        }
    }
}
