using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundOnWall : MonoBehaviour
{
    private Vector2 moveDirection = new Vector2(1f, 0f);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Vector2 n = collision.transform.up;
            float h = Mathf.Abs(Vector2.Dot(this.moveDirection, n));
            this.moveDirection += 2 * n * h;
        }
    }
}
