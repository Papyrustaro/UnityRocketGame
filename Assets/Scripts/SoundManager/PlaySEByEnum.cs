using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySEByEnum : MonoBehaviour
{
    [SerializeField] private E_SE playSE;

    public void PlaySE()
    {
        switch (this.playSE)
        {
            case E_SE.ミサイル爆発音:
                SEManager.PlaySE(SEManager.missileExplosion);
                break;
        }
    }

    public static void PlaySE(E_SE playSE)
    {
        switch (playSE)
        {
            case E_SE.ミサイル爆発音:
                SEManager.PlaySE(SEManager.missileExplosion);
                break;
        }
    }
}

public enum E_SE
{
    ミサイル爆発音,

}
