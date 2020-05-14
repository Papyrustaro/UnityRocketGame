using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecelerationField : MonoBehaviour
{
    [SerializeField] private float lowerRate = 0.2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().velocity *= this.lowerRate;
        }
    }
}
