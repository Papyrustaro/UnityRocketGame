using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageGameObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private int stageIndex;

    private float countTime = 0f;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SelectStageManager.Instance.SelectingStageIndex = this.stageIndex;
            this.spriteRenderer.color = new Color32(255, 143, 143, 255);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.countTime += Time.deltaTime;
            if(this.countTime > 1f)
            {
                SelectStageManager.Instance.SelectingStageIndex = this.stageIndex;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SelectStageManager.Instance.SelectingStageIndex = -1;
            this.spriteRenderer.color = new Color32(255, 255, 255, 255);
            this.countTime = 0f;
        }
    }
}
