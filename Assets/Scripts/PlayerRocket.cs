﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerRocket : MonoBehaviour
{
    [SerializeField] private Sprite explosion_small;
    [SerializeField] private Sprite explosion_big;
    private PlayerRocketController m_rocketController;
    private SpriteRenderer m_spriteRenderer;

    public bool IsDied { get; private set; } = false;
    
    private void Awake()
    {
        this.m_spriteRenderer = GetComponent<SpriteRenderer>();
        this.m_rocketController = GetComponent<PlayerRocketController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            this.IsDied = true;
            this.m_rocketController.InjectionFire.SetActive(false);
            Time.timeScale = 0f;
            Debug.Log("墜落");
            this.m_spriteRenderer.sprite = this.explosion_small;
            StartCoroutine(DelayMethodRealTime(0.3f, () =>
            {
                this.m_spriteRenderer.sprite = this.explosion_big;
            }));
            StartCoroutine(DelayMethodRealTime(1.5f, () =>
            {
                SceneManager.LoadScene("GameOver");
            }));
        }
    }

    IEnumerator DelayMethodRealTime(float waitTime, Action action)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        action();
    }
}
