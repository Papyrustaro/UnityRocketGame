using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThereAndBackMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector2 moveDirectionAndLength;
    [SerializeField] private float waitTurnTime = 1f;
    private Vector3 centerPosition;
    private Vector2 moveDirection;
    private int directionFlag = 1;

    private float countTime = 0f;
    private bool isMoving = true;

    private void Awake()
    {
        this.centerPosition = this.transform.position;
        this.moveDirection = this.moveDirectionAndLength.normalized;
    }

    private void Update()
    {
        if (this.isMoving)
        {
            MoveThereAndBack();
        }
        else
        {
            this.countTime += Time.deltaTime;
            if(this.countTime >= this.waitTurnTime)
            {
                this.isMoving = true;
                this.countTime = 0f;
            }
        }
    }

    private void MoveThereAndBack()
    {
        if((this.moveDirection.x * this.directionFlag > 0f && this.transform.position.x > this.centerPosition.x + Mathf.Abs(this.moveDirectionAndLength.x)) ||
            (this.moveDirection.x * this.directionFlag < 0f && this.transform.position.x < this.centerPosition.x - Mathf.Abs(this.moveDirectionAndLength.x)) ||
            (this.moveDirection.y * this.directionFlag > 0f && this.transform.position.y > this.centerPosition.y + Mathf.Abs(this.moveDirectionAndLength.y)) ||
            (this.moveDirection.y * this.directionFlag < 0f && this.transform.position.y < this.centerPosition.y - Mathf.Abs(this.moveDirectionAndLength.y)))
        {
            this.directionFlag *= -1;
            this.isMoving = false;
            return;
        }

        this.transform.position = new Vector3(this.transform.position.x + this.moveDirection.x * this.directionFlag * this.moveSpeed * Time.deltaTime, this.transform.position.y + this.moveDirection.y * this.directionFlag * this.moveSpeed * Time.deltaTime, 0f);
    }
}
