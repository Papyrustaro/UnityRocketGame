using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 向きも変わらないやつ
/// </summary>
public class PRM_ConstantMove : PlayerRocketMovement
{
    [SerializeField] private float moveSpeed = 0.01f;
    [SerializeField] private float upperPositionLimit = 2f;
    private Vector2 moveDirection;

    private void Awake()
    {
        this.AwakeFunc();
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
        this.moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        
        if (this.moveDirection != Vector2.zero)
        {
            if (this.transform.position.y >= this.upperPositionLimit && this.moveDirection.y > 0) this.M_Rigidbody2D.MovePosition(new Vector2(this.transform.position.x + this.moveDirection.x * this.moveSpeed, this.upperPositionLimit));
            else this.M_Rigidbody2D.MovePosition(new Vector2(this.transform.position.x + this.moveDirection.x * this.moveSpeed, this.transform.position.y + this.moveDirection.y * this.moveSpeed));
        }
    }
}
