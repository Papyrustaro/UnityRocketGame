using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AddTimeWhenContactPlayer : MonoBehaviour
{
    [SerializeField] private float addTimeValue = 5f;
    [SerializeField] private UnityEvent contactPlayerEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TimeManager.Instance.AddTime(this.addTimeValue);
            this.contactPlayerEvent.Invoke();
            Destroy(this.gameObject);
        }
    }
}
