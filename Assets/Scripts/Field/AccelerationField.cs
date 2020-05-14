using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationField : MonoBehaviour
{
    [SerializeField] private float moveForce = 3f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, this.moveForce));
        }
    }
}
