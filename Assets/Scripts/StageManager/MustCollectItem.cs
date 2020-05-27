using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MustCollectItem : MonoBehaviour
{
    [SerializeField] private UnityEvent getMustCollectItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (StageManager.Instance.ClearFlagType == E_ClearFlagType.CollectItem && collision.CompareTag("Player"))
        {
            SEManager.PlaySE(SEManager.getItem);
            this.getMustCollectItem.Invoke();
            ClearFlag_CollectItem.Instance.GetMustCollectItem();
            Destroy(this.gameObject);
        }
    }
}
