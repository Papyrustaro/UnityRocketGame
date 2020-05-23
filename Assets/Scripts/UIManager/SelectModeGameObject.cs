using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectModeGameObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private E_PlayMode moveToPlayMode;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SelectModeManager.Instance.PlayerSelectingMode = this.moveToPlayMode;
            this.spriteRenderer.color = new Color32(255, 143, 143, 255);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SelectModeManager.Instance.PlayerSelectingMode = E_PlayMode.Other;
            this.spriteRenderer.color = new Color32(255, 255, 255, 255);
        }
    }
}
