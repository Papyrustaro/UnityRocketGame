using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// HPをもち、攻撃によって破壊される可能性のあるgameObjectのTriggerEnter処理
/// </summary>
public class ColliderFunctionOfHaveHpObject : MonoBehaviour
{
    [SerializeField] private bool ignoreObstacle = false;
    [SerializeField] private int hp = 2;
    [SerializeField] private UnityEvent haveDamaged; //攻撃を受けた際の処理(damage計算以外: SE再生など)
    [SerializeField] private UnityEvent destroyMe; //自身が破壊されるときの処理
    [SerializeField] private UnityEvent contactEnemy; //敵の場合はプレイヤーに当たったとき

    /// <summary>
    /// 敵or敵の攻撃でtrue、プレイヤーorプレイヤーの攻撃でfalse
    /// </summary>
    public E_FlyingObjectType FlyingObjectType{ get; private set; }

    public int Hp => this.hp;

    private void Awake()
    {
        switch (this.gameObject.tag)
        {
            case "Player":
                this.FlyingObjectType = E_FlyingObjectType.Player;
                break;
            case "Enemy":
                this.FlyingObjectType = E_FlyingObjectType.Enemy;
                break;
            case "Obstacle":
                this.FlyingObjectType = E_FlyingObjectType.Obstacle;
                this.ignoreObstacle = true;
                break;
            case "AttackToPlayer":
                this.FlyingObjectType = E_FlyingObjectType.AttackToPlayer;
                break;
            case "AttackToEnemy":
                this.FlyingObjectType = E_FlyingObjectType.AttackToEnemy;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.ignoreObstacle && collision.CompareTag("Obstacle"))
        {
            this.destroyMe.Invoke();
            return;
        }
        if (collision.CompareTag("AttackToAll") || this.FlyingObjectType == E_FlyingObjectType.Obstacle)
        {
            this.hp -= collision.GetComponent<DamageAttackObject>().DamageValue;
            this.haveDamaged.Invoke();
            if (this.hp <= 0) this.destroyMe.Invoke();
        }
        else if (this.FlyingObjectType == E_FlyingObjectType.Enemy || this.FlyingObjectType == E_FlyingObjectType.AttackToPlayer)
        {
            if (collision.CompareTag("AttackToEnemy"))
            {
                this.hp -= collision.GetComponent<DamageAttackObject>().DamageValue;
                this.haveDamaged.Invoke();
                if (this.hp <= 0) {
                    if(this.FlyingObjectType == E_FlyingObjectType.Enemy && StageManager.Instance.ClearFlagType == E_ClearFlagType.DefeatEnemy)
                    {
                        ClearFlag_DefeatEnemy.Instance.CheckDefeatedEnemy(this.transform.root.gameObject);
                    }
                    this.destroyMe.Invoke(); 
                }
            }
            else if (collision.CompareTag("Player"))
            {
                //プレイヤーにあたったとき
                this.contactEnemy.Invoke();
            }
        }
        else if(this.FlyingObjectType == E_FlyingObjectType.Player || this.FlyingObjectType == E_FlyingObjectType.AttackToEnemy)
        {
            if (collision.CompareTag("AttackToPlayer"))
            {
                this.hp -= collision.GetComponent<DamageAttackObject>().DamageValue;
                this.haveDamaged.Invoke();
                if (this.hp <= 0) this.destroyMe.Invoke();
            }
            else if (collision.CompareTag("Enemy"))
            {
                //敵に当たったとき
                this.contactEnemy.Invoke();
            }
        }
    }

    public void DestroyMyGameObject()
    {
        Destroy(this.gameObject);
    }
}

public enum E_FlyingObjectType
{
    Player,
    Enemy,
    Obstacle,
    AttackToPlayer,
    AttackToEnemy,
}