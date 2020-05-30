using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBiggingMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float expandRadisSpeed = 0.1f;
    private Vector2 centerPosition;
    private float radius = 0f;

    private void Awake()
    {
        this.centerPosition = this.transform.position;
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;
        this.radius += this.expandRadisSpeed * Time.deltaTime;
        float x = this.radius * Mathf.Sin(Time.time * this.moveSpeed);
        float y = this.radius * Mathf.Cos(Time.time * this.moveSpeed);

        this.transform.position = new Vector3(x + this.centerPosition.x, y + this.centerPosition.y, 0f);
    }
}
