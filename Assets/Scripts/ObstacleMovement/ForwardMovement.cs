using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector2 forwardDirection = new Vector2(0f, 1f);
    [SerializeField] private Vector2 moveDirection = new Vector2(0f, 1f);
    [SerializeField] private bool boundWall = false;
    private Vector3 forwardDirection3D;

    private Rigidbody2D m_rigidbody2D;

    private void Awake()
    {
        this.forwardDirection3D = new Vector3(this.forwardDirection.x, this.forwardDirection.y, 0f).normalized;
        this.moveDirection = this.moveDirection.normalized;

        this.m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.transform.rotation = Quaternion.FromToRotation(this.forwardDirection3D, this.moveDirection);
        this.m_rigidbody2D.AddForce(this.moveDirection * this.moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.boundWall && collision.transform.CompareTag("Wall"))
        {
            Vector2 diff = new Vector2();
            diff = this.transform.position;
            StartCoroutine(DelayMethod(0.021f, () =>
            {
                diff = new Vector2(this.transform.position.x - diff.x, this.transform.position.y - diff.y).normalized;
                this.moveDirection = diff;
                this.transform.rotation = Quaternion.FromToRotation(this.forwardDirection3D, this.moveDirection);
            }));
        }
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;

        this.transform.rotation = Quaternion.FromToRotation(this.forwardDirection3D, this.moveDirection);
        this.m_rigidbody2D.velocity = Vector2.zero;
        this.m_rigidbody2D.AddForce(this.moveDirection * this.moveSpeed);
    }

    IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
