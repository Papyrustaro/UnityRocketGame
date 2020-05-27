using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UnityEventから呼ぶ用
/// </summary>
public class AddScore : MonoBehaviour
{
    [SerializeField] private int addScoreValue;

    public void AddScoreFunc()
    {
        ScoreManager.Instance.AddScore(this.addScoreValue);
    }
}
