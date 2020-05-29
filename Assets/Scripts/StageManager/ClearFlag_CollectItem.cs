using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFlag_CollectItem : MonoBehaviour
{
    [SerializeField] private int itemCount = 3;

    public static ClearFlag_CollectItem Instance { get; set; }

    public int RemainItemCount { get; set; }

    private void Awake()
    {
        if(ClearFlag_CollectItem.Instance == null)
        {
            ClearFlag_CollectItem.Instance = this;
        }
        else
        {
            throw new System.Exception();
        }
        this.RemainItemCount = this.itemCount;
    }

    public void GetMustCollectItem()
    {
        this.RemainItemCount--;
        StageUIManager.Instance.UpdateFlagText();
        if(this.RemainItemCount <= 0)
        {
            StageManager.Instance.StageClear();
        }
    }
}
