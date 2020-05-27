using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationField : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SEManager.PlaySE(SEManager.returnMoveDirection);
            collision.transform.parent.Rotate(0f, 0f, 180f);
        }
    }
}
