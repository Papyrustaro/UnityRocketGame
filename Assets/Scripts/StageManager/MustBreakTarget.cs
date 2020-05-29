using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MustBreakTarget : MonoBehaviour
{
    [SerializeField] private UnityEvent breakTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (StageManager.Instance.ClearFlagType == E_ClearFlagType.BreakTarget && collision.CompareTag("AttackToEnemy"))
        {
            SEManager.PlaySE(SEManager.getItem);
            this.breakTarget.Invoke();
            ClearFlag_BreakTarget.Instance.BreakTarget();
            Destroy(this.gameObject);
        }
    }
}
