﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーロケットの動き制御。角度は独立して変更可能。噴射で力学的加速。最低速度・最高速度有、減速あり
/// </summary>
public class PRM_PhysicsWithSpeedRange : PlayerRocketMovement
{
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float rotationSpeed = -1f;
    [SerializeField] private float minSpeed = 3f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float decelerationRate = 0.98f;
    private bool isMoveForce; //加速しているか
    private bool useOriginalRotationSpeed = false; //Inspectorで設定されたrotationSpeedを利用しているかどうか
    private void Awake()
    {
        this.AwakeFunc();
    }

    private void Start()
    {
        this.M_Rigidbody2D.velocity = new Vector2(0f, 1f);
        if (this.rotationSpeed < 0f) this.rotationSpeed = StaticData.rotationSpeed;
        else this.useOriginalRotationSpeed = true;
    }

    private void Update()
    {
        if (!this.useOriginalRotationSpeed && this.rotationSpeed != StaticData.rotationSpeed) this.rotationSpeed = StaticData.rotationSpeed;
        if (!this.M_Rigidbody2D.simulated)
        {
            if (StageManager.Instance.IsStop) return;
            else this.M_Rigidbody2D.simulated = true;
        }
        else
        {
            if (StageManager.Instance.IsStop)
            {
                this.M_Rigidbody2D.simulated = false;
                return;
            }
        }
        if (!this.M_PlayerRocket.IsDied)
        {
            this.RocketMoveUpdate();
        }
    }

    private void FixedUpdate()
    {
        
        if (this.isMoveForce && this.M_Rigidbody2D.velocity.magnitude <= this.maxSpeed)
        {
            this.M_Rigidbody2D.AddRelativeForce(new Vector2(0f, this.moveForce));
            this.isMoveForce = false;
            this.InjectionFire.SetActive(false);
        }
        if(this.M_Rigidbody2D.velocity.magnitude >= this.minSpeed)
        {
            this.M_Rigidbody2D.velocity *= this.decelerationRate;
        }
    }

    /// <summary>
    /// 動きを停止させる(速度、加速度、角速度0)
    /// </summary>
    public void StopMovement()
    {
        this.M_Rigidbody2D.velocity = Vector2.zero;
        this.M_Rigidbody2D.angularVelocity = 0f;
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
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            //加速
            this.isMoveForce = true;
            this.InjectionFire.SetActive(true);
        }
    }
}
