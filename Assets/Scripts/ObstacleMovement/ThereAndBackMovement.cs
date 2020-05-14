using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThereAndBackMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector2 moveDirectionAndLength;
    //private float xLengthFromPosition;
    //private float yLengthFromPosition;
    private Vector3 centerPosition;
    private Vector2 moveDirection;
    private int directionFlag = 1;

    private void Awake()
    {
        this.centerPosition = this.transform.position;
        //this.moveDirection = new Vector2(xLengthFromPosition, yLengthFromPosition).normalized;
        this.moveDirection = this.moveDirectionAndLength.normalized;
    }

    private void Update()
    {
        MoveThereAndBack();
    }

    private void MoveThereAndBack()
    {
        if((this.moveDirection.x * this.directionFlag > 0f && this.transform.position.x > this.centerPosition.x + Mathf.Abs(this.moveDirectionAndLength.x)) ||
            (this.moveDirection.x * this.directionFlag < 0f && this.transform.position.x < this.centerPosition.x - Mathf.Abs(this.moveDirectionAndLength.x)) ||
            (this.moveDirection.y * this.directionFlag > 0f && this.transform.position.y > this.centerPosition.y + Mathf.Abs(this.moveDirectionAndLength.y)) ||
            (this.moveDirection.y * this.directionFlag < 0f && this.transform.position.y < this.centerPosition.y - Mathf.Abs(this.moveDirectionAndLength.y)))
        {
            this.directionFlag *= -1;
        }

        this.transform.position = new Vector3(this.transform.position.x + this.moveDirection.x * this.directionFlag * this.moveSpeed * Time.deltaTime, this.transform.position.y + this.moveDirection.y * this.directionFlag * this.moveSpeed * Time.deltaTime, 0f);
        /*if (this.flag)
        {
            if (this.transform.position.x > this.centerPosition.x + Mathf.Abs(this.moveDirectionAndLength.x) || this.transform.position.y > this.centerPosition.y + Mathf.Abs(this.moveDirectionAndLength.y))
            {
                this.flag = false;
            }
            this.transform.position = new Vector3(this.transform.position.x + this.moveDirection.x * this.moveSpeed * Time.deltaTime, this.transform.position.y + this.moveDirection.y * this.moveSpeed * Time.deltaTime, 0f);
        }
        else
        {
            if(this.transform.position.x < this.centerPosition.x - Mathf.Abs(this.moveDirectionAndLength.x) || this.transform.position.y < this.centerPosition.y - Mathf.Abs(this.moveDirectionAndLength.y))
            {
                this.flag = true;
            }
            this.transform.position = new Vector3(this.transform.position.x + this.moveDirection.x * this.moveSpeed * Time.deltaTime * -1, this.transform.position.y + this.moveDirection.y * this.moveSpeed * Time.deltaTime * -1, 0f);
        }*/
    }
}
