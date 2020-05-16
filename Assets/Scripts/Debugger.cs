using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Debugger : MonoBehaviour
{
    public void HaveDamagedFunc()
    {
        Debug.Log("ダメージを受けた" + Time.time);
    }

    public void DestroyFunc()
    {
        Debug.Log("死んだ!" + Time.time);
    }

    public void ContactEnemy()
    {
        Debug.Log("敵と衝突した");
    }
}
