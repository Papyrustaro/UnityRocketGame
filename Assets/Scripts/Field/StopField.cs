using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopField : MonoBehaviour
{
    private bool onPlayer = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D p_rb = collision.GetComponent<Rigidbody2D>();
            p_rb.velocity = Vector2.zero;
            p_rb.angularVelocity = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("a");
        }
    }
}
