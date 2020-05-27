using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HomingOnAndOffMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f;
    //[SerializeField] private Transform playerPrefab;
    [SerializeField] private Vector2 forwardDirection = new Vector2(0f, 1f);
    [SerializeField] private float oneMoveTime = 2f;
    [SerializeField] private float oneStopTime = 1f;
    [SerializeField] private bool boundWall = false;

    private Transform playerPrefab;
    private float countTime = 0f;
    private float oneRoutineTime;
    private Vector3 forwardDirection3D;
    private bool rotationFlag = true;
    private Vector2 moveDirection;

    private Rigidbody2D m_rigidbody2D;

    private void Awake()
    {
        this.oneRoutineTime = this.oneMoveTime + this.oneStopTime;
        this.forwardDirection3D = new Vector3(this.forwardDirection.x, this.forwardDirection.y, 0f).normalized;
        this.m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.playerPrefab = PlayerRocket.PlayerTransform;
    }
    private void Update()
    {
        if (StageManager.Instance.IsStop) return;
        this.countTime += Time.deltaTime;
        if(this.countTime >= this.oneStopTime)
        {
            if (this.rotationFlag)
            {
                this.moveDirection = new Vector2(this.playerPrefab.position.x - this.transform.position.x, this.playerPrefab.position.y - this.transform.position.y).normalized;
                Vector3 diff = new Vector3(this.playerPrefab.position.x - this.transform.position.x, this.playerPrefab.position.y - this.transform.position.y, 0f);
                this.transform.rotation = Quaternion.FromToRotation(this.forwardDirection3D, diff.normalized);
                this.m_rigidbody2D.AddForce(this.moveDirection * this.moveSpeed);
                this.rotationFlag = false;
            }

            if(this.countTime >= this.oneRoutineTime)
            {
                this.m_rigidbody2D.velocity = Vector2.zero;
                this.countTime = 0f;
                this.rotationFlag = true;
            }
        }
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
                if(this.m_rigidbody2D.velocity != Vector2.zero)
                {
                    this.m_rigidbody2D.velocity = Vector2.zero;
                    this.m_rigidbody2D.AddForce(diff * this.moveSpeed);
                }
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
