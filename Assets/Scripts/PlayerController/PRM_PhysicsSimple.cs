using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーロケットの動き制御。向いている方向に力学的に加速。回転は独立。最低速度・最高速度は無し
/// </summary>
public class PRM_PhysicsSimple : PlayerRocketMovement
{
    [SerializeField] private float moveForce = 3f;
    [SerializeField] private float rotationSpeed = 3f;
    private bool isMoveForce; //加速しているか

    private void Awake()
    {
        this.AwakeFunc();
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;
        if (!this.M_PlayerRocket.IsDied)
        {
            this.RocketMoveUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (this.isMoveForce)
        {
            this.M_Rigidbody2D.AddRelativeForce(new Vector2(0f, this.moveForce));
            this.isMoveForce = false;
            this.InjectionFire.SetActive(false);
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
        if (Input.GetKey(KeyCode.Space))
        {
            //加速
            this.isMoveForce = true;
            this.InjectionFire.SetActive(true);
        }
    }

    
}
