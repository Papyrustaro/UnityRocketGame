using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自身のオブジェクトに向かって、周囲のオブジェクトを吸い寄せる
/// </summary>
public class InhaleController : MonoBehaviour
{
    [SerializeField] private bool ignoreObstacles = true; //プレイヤー以外は吸い寄せないかどうか
    [SerializeField] private float inhaleForce = 1f;
    [SerializeField] private GameObject playerPrefab;
    private PlayerRocketController playerController;

    //Vector2 v;

    private void Awake()
    {
        this.playerController = this.playerPrefab.GetComponent<PlayerRocketController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 v = new Vector2(this.transform.position.x - this.playerPrefab.transform.position.x, this.transform.position.y - this.playerPrefab.transform.position.y);
            this.playerController.M_RigidBody.AddForce(v.normalized * this.inhaleForce);
            //this.playerPrefab.transform.position = new Vector3(this.playerPrefab.transform.position.x - v.normalized.x * Time.deltaTime * this.inhaleForce, this.playerPrefab.transform.position.y - v.normalized.y * Time.deltaTime * this.inhaleForce, 0f);
            //v = new Vector2(this.transform.position.x - this.playerPrefab.transform.position.x, this.transform.position.y - this.playerPrefab.transform.position.y);
        }
    }

    private void FixedUpdate()
    {
        /*
        if(v.magnitude != 0)
        {
            this.playerController.M_RigidBody.AddForce(v.normalized * this.inhaleForce);
            v = new Vector2(0f, 0f);
        }*/
    }
}
