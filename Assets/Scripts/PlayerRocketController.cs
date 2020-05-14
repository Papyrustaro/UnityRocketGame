﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの入力に応じてロケットを動かすクラス
/// </summary>
public class PlayerRocketController : MonoBehaviour
{
    [SerializeField] private float moveForce = 3f;
    [SerializeField] private float rotationSpeed = 3f;
    private Rigidbody2D m_rigidbody;
    private bool isMoveForce; //加速しているか
    private PlayerRocket m_rocket;

    public Rigidbody2D M_RigidBody => this.m_rigidbody;

    public GameObject InjectionFire { get; private set; }

    private void Awake()
    {
        this.m_rigidbody = GetComponent<Rigidbody2D>();
        this.m_rocket = GetComponent<PlayerRocket>();
        this.InjectionFire = this.transform.Find("InjectionFire").gameObject;
    }

    private void Update()
    {
        if (!this.m_rocket.IsDied)
        {
            this.RocketMoveUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            this.m_rigidbody.velocity = Vector2.zero;
            this.m_rigidbody.angularVelocity = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (this.isMoveForce)
        {
            this.m_rigidbody.AddRelativeForce(new Vector2(0f, this.moveForce));
            this.isMoveForce = false;
            this.InjectionFire.SetActive(false);
        }
    }

    /// <summary>
    /// 動きを停止させる(速度、加速度、角速度0)
    /// </summary>
    public void StopMovement()
    {
        this.m_rigidbody.velocity = Vector2.zero;
        this.m_rigidbody.angularVelocity = 0f;
    }


    private void RocketMoveUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //左回転
            this.transform.Rotate(new Vector3(0, 0, this.rotationSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            //右回転
            this.transform.Rotate(new Vector3(0, 0, this.rotationSpeed * Time.deltaTime * -1));
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //加速
            this.isMoveForce = true;
            this.InjectionFire.SetActive(true);
        }
    }

    
}
