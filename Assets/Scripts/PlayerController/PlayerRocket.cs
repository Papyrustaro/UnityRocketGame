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

    public static Transform PlayerTransform { private set; get; }

    
    public bool IsDied { get; private set; } = false;
    
    private void Awake()
    {
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
        PlayerRocket.PlayerTransform = this.transform;
    }

    private void Start()
    {
        //StageManager.Instance.PlayerPrefab = this.gameObject;
    }

    private void Update()
    {
        if (this.haveWeapon && Input.GetKeyDown(KeyCode.J))
        {
            SEManager.PlaySE(SEManager.shotBullet);
            this.weaponGenerator.GenerateWeapon();
        }
    }

    public void DestroyPlayerRocket()
    {
        SEManager.PlaySE(SEManager.explosion);
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
