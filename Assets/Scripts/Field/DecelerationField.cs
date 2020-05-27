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
            SEManager.PlaySE(SEManager.speedDown);
            collision.transform.parent.GetComponent<Rigidbody2D>().velocity *= this.lowerRate;
        }
    }
}
