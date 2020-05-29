using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 向きも変わらないやつ
/// </summary>
public class PRM_ConstantMove : PlayerRocketMovement
{
    [SerializeField] private float moveSpeed = 0.01f;
    private Vector2 moveDirection;

    private void Awake()
    {
        this.AwakeFunc();
    }
    private void Update()
    {
        this.moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        if(this.moveDirection != Vector2.zero)
        {
            this.M_Rigidbody2D.MovePosition(new Vector2(this.transform.position.x + this.moveDirection.x * this.moveSpeed, this.transform.position.y + this.moveDirection.y * this.moveSpeed));
        }
    }
}
