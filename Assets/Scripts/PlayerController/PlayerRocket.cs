using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerRocket : MonoBehaviour
{
    [SerializeField] private Sprite explosion_small;
    [SerializeField] private Sprite explosion_big;
    private PlayerRocketMovement m_rocketController;
    private SpriteRenderer m_spriteRenderer;
    private WeaponGenerator weaponGenerator;
    private bool haveWeapon = true;

    
    public bool IsDied { get; private set; } = false;
    
    private void Awake()
    {
        StageManager.Instance.PlayerPrefab = this.gameObject;
        this.m_spriteRenderer = GetComponent<SpriteRenderer>();
        this.m_rocketController = GetComponent<PlayerRocketMovement>();
        try
        {
            this.weaponGenerator = this.transform.Find("Weapon").GetComponent<WeaponGenerator>();
        }
        catch (NullReferenceException)
        {
            this.haveWeapon = false;
        }
    }

    private void Update()
    {
        if (this.haveWeapon && Input.GetKeyDown(KeyCode.J))
        {
            this.weaponGenerator.GenerateWeapon();
        }
    }

    public void DestroyPlayerRocket()
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
            StageManager.Instance.GameOver();
        }));
    }

    IEnumerator DelayMethodRealTime(float waitTime, Action action)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        action();
    }
}
