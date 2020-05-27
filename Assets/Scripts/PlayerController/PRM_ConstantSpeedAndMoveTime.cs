using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// プレイヤーロケットの動き制御。角度は独立して変更可能。噴射で一定時間移動(非力学的)
/// </summary>
public class PRM_ConstantSpeedAndMoveTime : PlayerRocketMovement
{
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float rotationSpeed = 300f;
    [SerializeField] private float oneMovementTime = 0.5f;
    private bool isMoving; //動いているか
    private Vector2 moveDirection;

    private void Awake()
    {
        this.AwakeFunc();
    }

    private void Update()
    {
        if (StageManager.Instance.IsStop) return;
        if (!this.isMoving && !this.M_PlayerRocket.IsDied)
        {
            this.RocketMoveUpdate();
        }

        /*if (Input.GetKeyDown(KeyCode.Backspace))
        {
            this.M_Rigidbody2D.velocity = Vector2.zero;
            this.M_Rigidbody2D.angularVelocity = 0f;
        }*/
    }

    private void FixedUpdate()
    {
        if (this.isMoving)
        {
            this.M_Rigidbody2D.MovePosition(new Vector2(this.transform.position.x + this.moveDirection.x * this.moveSpeed, this.transform.position.y + this.moveDirection.y * this.moveSpeed));
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
            float z = (this.transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
            this.moveDirection = new Vector2(Mathf.Cos(z), Mathf.Sin(z)).normalized;
            this.isMoving = true;
            this.InjectionFire.SetActive(true);

            StartCoroutine(DelayMethod(this.oneMovementTime, () =>
            {
                this.isMoving = false;
                this.InjectionFire.SetActive(false);
            }));
        }
    }

    IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
