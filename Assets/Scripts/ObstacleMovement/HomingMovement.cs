using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    //[SerializeField] private Transform playerPrefab;
    [SerializeField] private Vector2 forwardDirection = new Vector2(0f, 1f);
    private Vector3 forwardDirection3D;

    private Transform playerPrefab;
    private void Awake()
    {
        this.forwardDirection3D = new Vector3(this.forwardDirection.x, this.forwardDirection.y, 0f).normalized;
    }

    private void Start()
    {
        this.playerPrefab = StageManager.Instance.PlayerPrefab.transform;
    }
    private void Update()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, this.playerPrefab.position, this.moveSpeed * Time.deltaTime);

        Vector3 diff = new Vector3(this.playerPrefab.position.x - this.transform.position.x, this.playerPrefab.position.y - this.transform.position.y, 0f);
        //this.transform.rotation = Quaternion.FromToRotation(Vector3.up, diff.normalized);
        this.transform.rotation = Quaternion.FromToRotation(this.forwardDirection3D, diff.normalized);
    }


}
