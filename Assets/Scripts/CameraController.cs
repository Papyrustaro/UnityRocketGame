using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerRocketPosition;

    private void LateUpdate()
    {
        this.transform.position = new Vector3(this.playerRocketPosition.position.x, this.playerRocketPosition.position.y, this.transform.position.z);
    }
}
