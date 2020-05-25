using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveScoreGameObject : MonoBehaviour
{
    [SerializeField] private int haveScoreValue;

    public void AddScore()
    {
        ScoreManager.Instance.AddScore(this.haveScoreValue);
    }
}
