using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃によってダメージを与えるgameObject。tagによって管理(attackToPlayer/attackToEnemy/attackToAll)
/// </summary>
public class DamageAttackObject : MonoBehaviour
{
    [SerializeField] private int damageValue = 1;

    public int DamageValue => this.damageValue;
}
