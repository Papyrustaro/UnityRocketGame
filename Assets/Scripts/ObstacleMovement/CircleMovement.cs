using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float circleRadius = 3f;
    private Vector2 centerPosition;

    private void Awake()
    {
        this.centerPosition = this.transform.position;
    }

    private void Update()
    {
        float x = this.circleRadius * Mathf.Sin(Time.time * this.moveSpeed);
        float y = this.circleRadius * Mathf.Cos(Time.time * this.moveSpeed);

        this.transform.position = new Vector3(x + this.centerPosition.x, y + this.centerPosition.y, 0f);
    }
}
