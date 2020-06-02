using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーロケットの動き制御。角度を変更すると進行方向も変わる。噴射で力学的加速。最低速度・最高速度有
/// </summary>
public class PRM_PhysicsWithSpeedRangeAndMoveDirectionWithRotation : PlayerRocketMovement
{
    [SerializeField] private float moveForce = 1f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float minSpeed = 3f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float decelerationRate = 0.98f;
    private bool isMoveForce; //加速しているか
    private float rotationValue = 0f; //回転量記憶用

    private float moveToSidesLength = 1f; //横へ瞬時移動距離

    private void Awake()
    {
        this.AwakeFunc();
    }

    private void Start()
    {
        this.M_Rigidbody2D.velocity = new Vector2(0f, 1f);
    }

    private void Update()
    {
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
        //velocityの向きを回転に合わせて変える
        this.M_Rigidbody2D.velocity = Quaternion.Euler(0, 0, this.rotationValue) * this.M_Rigidbody2D.velocity;
        this.rotationValue = 0f;

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
            this.rotationValue += this.rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //右回転
            this.transform.Rotate(new Vector3(0, 0, this.rotationSpeed * Time.deltaTime * -1));
            this.rotationValue -= this.rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //加速
            this.isMoveForce = true;
            this.InjectionFire.SetActive(true);
        }
        /*if (Input.GetKey(KeyCode.R))
        {
            //進行方向の右側に移動
            Vector2 v = new Vector2(0f, this.moveToSidesLength);
            v = Quaternion.Euler(0, 0, this.transform.rotation.z + -90) * v;
            v = new Vector2(v.x + this.transform.position.x, v.y + this.transform.position.y);
            this.transform.position = Vector2.MoveTowards(this.transform.position, v, this.moveToSidesLength);
        }*/
        if (Input.GetKey(KeyCode.E))
        {
            //進行方向の左側に移動
        }
    }
}
