using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMovementByForce : MonoBehaviour
{
    [SerializeField] private float moveForce = 1f;
    //[SerializeField] private Transform playerPrefab;
    [SerializeField] private Vector2 forwardDirection = new Vector2(0f, 1f);
    [SerializeField] private GameObject explosionInDestroy;
    private Rigidbody2D m_rigidbody2D;
    private Vector3 forwardDirection3D;
    private Vector3 diff;

    private Transform playerPrefab;

    private void Awake()
    {
        this.m_rigidbody2D = GetComponent<Rigidbody2D>();
        this.forwardDirection3D = new Vector3(this.forwardDirection.x, this.forwardDirection.y, 0f).normalized;
    }

    private void Start()
    {
        this.playerPrefab = StageManager.playerPrefab.transform;
    }
    private void Update()
    {
        this.diff = new Vector3(this.playerPrefab.position.x - this.transform.position.x, this.playerPrefab.position.y - this.transform.position.y, 0f).normalized;
        this.transform.rotation = Quaternion.FromToRotation(this.forwardDirection3D, this.diff);
    }

    private void FixedUpdate()
    {
        this.m_rigidbody2D.AddForce(this.diff);
    }

    public void DestroyMissile()
    {
        Instantiate(this.explosionInDestroy, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
