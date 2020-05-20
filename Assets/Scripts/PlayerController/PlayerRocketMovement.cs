using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerRocketMovement : MonoBehaviour
{
    public Rigidbody2D M_Rigidbody2D { private set; get; }
    public GameObject InjectionFire { private set; get; }
    public PlayerRocket M_PlayerRocket { private set; get; }

    public void AwakeFunc()
    {
        this.M_Rigidbody2D = GetComponent<Rigidbody2D>();
        this.InjectionFire = this.transform.Find("InjectionFire").gameObject;
        this.M_PlayerRocket = GetComponent<PlayerRocket>();
    }
}
