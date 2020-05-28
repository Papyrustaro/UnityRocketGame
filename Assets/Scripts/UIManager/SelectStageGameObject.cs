using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageGameObject : MonoBehaviour
{
    [SerializeField] private int stageIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SelectStageManager.Instance.OpenAndClosePanel(this.stageIndex, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SelectStageManager.Instance.OpenAndClosePanel(this.stageIndex, false);
        }
    }
}
