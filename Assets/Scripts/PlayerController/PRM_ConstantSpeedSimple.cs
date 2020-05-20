using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーロケットの動き制御。角度は独立して変更可能。噴射で一定距離移動(非力学的)
/// </summary>
public class PRM_ConstantSpeedSimple : PlayerRocketMovement
{
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float rotationSpeed = 300f;
    private bool isMoveForce; //加速しているか


    private void Awake()
    {
        this.AwakeFunc();
    }

    private void Update()
    {
        if (!this.M_PlayerRocket.IsDied)
        {
            this.RocketMoveUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            this.M_Rigidbody2D.velocity = Vector2.zero;
            this.M_Rigidbody2D.angularVelocity = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (this.isMoveForce)
        {
            float z = (this.transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
            this.M_Rigidbody2D.MovePosition(new Vector2(this.transform.position.x + Mathf.Cos(z) * this.moveSpeed, this.transform.position.y + Mathf.Sin(z) * this.moveSpeed));
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
