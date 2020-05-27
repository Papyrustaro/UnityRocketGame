using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HomingOnAndOffMovementByForce : MonoBehaviour
{
    [SerializeField] private float moveForce = 1f;
    //[SerializeField] private Transform playerPrefab;
    [SerializeField] private Vector2 forwardDirection = new Vector2(0f, 1f);
    [SerializeField] private float oneStopTime = 3f;
    [SerializeField] private float decelerationRate = 0.95f;
    [SerializeField] private float stopLowerSpeed = 0.5f;
    [SerializeField] private bool boundWall = false;

    private Transform playerPrefab;
    private float countTime = 0f;
    private Vector3 forwardDirection3D;
    private Rigidbody2D m_rigidbody2D;
    private bool rotationFlag = true;
    private Vector2 moveDirection;

    private void Awake()
    {
        this.m_rigidbody2D = GetComponent<Rigidbody2D>();
        this.forwardDirection3D = new Vector3(this.forwardDirection.x, this.forwardDirection.y, 0f).normalized;
    }

    private void Start()
    {
        this.playerPrefab = PlayerRocket.PlayerTransform;
    }
    private void Update()
    {
        if (StageManager.Instance.IsStop) return;
        this.countTime += Time.deltaTime;
        if (this.countTime >= this.oneStopTime)
        {
            if (this.rotationFlag)
            {
                this.moveDirection = new Vector2(this.playerPrefab.position.x - this.transform.position.x, this.playerPrefab.position.y - this.transform.position.y).normalized;
                this.m_rigidbody2D.velocity = new Vector2(this.moveDirection.x * this.moveForce, this.moveDirection.y * this.moveForce);
                Vector3 diff = new Vector3(this.playerPrefab.position.x - this.transform.position.x, this.playerPrefab.position.y - this.transform.position.y, 0f);
                this.transform.rotation = Quaternion.FromToRotation(this.forwardDirection3D, diff.normalized);
                this.rotationFlag = false;
            }
            if(this.m_rigidbody2D.velocity.magnitude < this.stopLowerSpeed)
            {
                this.m_rigidbody2D.velocity = Vector2.zero;
                this.countTime = 0f;
                this.rotationFlag = true;
            }
        }
    }

    private void FixedUpdate()
    {
        this.m_rigidbody2D.velocity *= this.decelerationRate;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.boundWall && (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Obstacle")))
        {
            Vector2 diff = new Vector2();
            diff = this.transform.position;
            StartCoroutine(DelayMethod(0.021f, () =>
            {
                diff = new Vector2(this.transform.position.x - diff.x, this.transform.position.y - diff.y).normalized;
                this.transform.rotation = Quaternion.FromToRotation(this.forwardDirection3D, diff);
            }));
        }
    }

    IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
