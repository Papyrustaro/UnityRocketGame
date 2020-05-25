using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// プレイヤーgameObjectと衝突したとき、scoreを足して、自身は消えるgameObject
/// </summary>
public class AddScoreWhenContactPlayer : MonoBehaviour
{
    [SerializeField] private int addScoreValue = 100;
    [SerializeField] private UnityEvent contactPlayerEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(this.addScoreValue);
            this.contactPlayerEvent.Invoke();
            Destroy(this.gameObject);
        }
    }
}
