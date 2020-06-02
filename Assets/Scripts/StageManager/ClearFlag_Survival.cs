using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFlag_Survival : MonoBehaviour
{
    [SerializeField] private float survivalTime = 10f;
    private float countTime = 0f;

    private void Update()
    {
        if (StageManager.Instance.IsStop) return;
        this.countTime += Time.deltaTime;
        if(this.countTime >= this.survivalTime)
        {
            StageManager.Instance.StageClear();
            this.gameObject.SetActive(false);
        }
    }
}
