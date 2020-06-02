using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerRocket : MonoBehaviour
{
    [SerializeField] private Sprite explosion_small;
    [SerializeField] private Sprite explosion_big;
    [SerializeField] private float shotInterval = 0.1f;
    private PlayerRocketMovement m_rocketController;
    private SpriteRenderer m_spriteRenderer;
    private WeaponGenerator weaponGenerator;
    private bool haveWeapon = true;
    private bool canShot = true;

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
        if (StageManager.Instance.IsStop) return;
        if (this.haveWeapon && this.canShot && Input.GetKeyDown(KeyCode.J))
        {
            SEManager.PlaySE(SEManager.shot0);
            this.weaponGenerator.GenerateWeapon();
            this.canShot = false;
            StartCoroutine(DelayMethod(this.shotInterval, () =>
            {
                this.canShot = true;
            }));
        }
    }

    public void DestroyPlayerRocket()
    {
        this.IsDied = true;
        this.m_rocketController.InjectionFire.SetActive(false);
        StageManager.Instance.StopAllMoving();
        SEManager.PlaySE(SEManager.explosionPlayer);
        this.m_spriteRenderer.sprite = this.explosion_small;
        StartCoroutine(DelayMethodRealTime(0.3f, () =>
        {
            this.m_spriteRenderer.sprite = this.explosion_big;
        }));
        StartCoroutine(DelayMethodRealTime(1f, () =>
        {
            StageManager.Instance.GameOver();
        }));
    }

    IEnumerator DelayMethodRealTime(float waitTime, Action action)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        action();
    }

    IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
