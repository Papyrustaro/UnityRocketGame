using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameObjectInTitle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TitleManager.Instance.PlayerInStartObject = true;
            this.spriteRenderer.color = new Color32(255, 143, 143, 255);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TitleManager.Instance.PlayerInStartObject = false;
            this.spriteRenderer.color = new Color32(255, 255, 255, 255);
        }
    }
}
