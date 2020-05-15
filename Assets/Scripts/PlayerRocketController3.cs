using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketController3 : MonoBehaviour
{
    [SerializeField] private float moveForce = 3f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float minSpeed = 3f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float decelerationRate = 0.98f;
    private Rigidbody2D m_rigidbody;
    private bool isMoveForce; //加速しているか
    private PlayerRocket m_rocket;
    private float rotationValue = 0f; //回転量記憶用

    private float moveToSidesLength = 1f; //横へ瞬時移動距離

    public Rigidbody2D M_RigidBody => this.m_rigidbody;

    public GameObject InjectionFire { get; private set; }

    private void Awake()
    {
        this.m_rigidbody = GetComponent<Rigidbody2D>();
        this.m_rocket = GetComponent<PlayerRocket>();
        this.InjectionFire = this.transform.Find("InjectionFire").gameObject;
    }

    private void Start()
    {
        this.m_rigidbody.velocity = new Vector2(0f, 1f);
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
        //velocityの向きを回転に合わせて変える
        this.m_rigidbody.velocity = Quaternion.Euler(0, 0, this.rotationValue) * this.m_rigidbody.velocity;
        this.rotationValue = 0f;

        this.m_rigidbody.velocity *= this.decelerationRate;
        if (this.m_rigidbody.velocity.magnitude < this.minSpeed)
        {
            this.m_rigidbody.velocity *= this.minSpeed / this.m_rigidbody.velocity.magnitude;
        }
        if (this.m_rigidbody.velocity.magnitude > this.maxSpeed)
        {
            this.m_rigidbody.velocity *= this.maxSpeed / this.m_rigidbody.velocity.magnitude;
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
        if (Input.GetKey(KeyCode.R))
        {
            //進行方向の右側に移動
            Vector2 v = new Vector2(0f, this.moveToSidesLength);
            v = Quaternion.Euler(0, 0, this.transform.rotation.z + -90) * v;
            v = new Vector2(v.x + this.transform.position.x, v.y + this.transform.position.y);
            this.transform.position = Vector2.MoveTowards(this.transform.position, v, this.moveToSidesLength);
        }
        if (Input.GetKey(KeyCode.E))
        {
            //進行方向の左側に移動
        }
    }
}
