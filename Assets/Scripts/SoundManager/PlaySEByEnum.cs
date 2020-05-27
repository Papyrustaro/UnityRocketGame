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
            case E_SE.explosionMissile:
                SEManager.PlaySE(SEManager.explosionMissile);
                break;
            case E_SE.select:
                SEManager.PlaySE(SEManager.select);
                break;
            case E_SE.shot0:
                SEManager.PlaySE(SEManager.shot0);
                break;
            case E_SE.shot1:
                SEManager.PlaySE(SEManager.shot1);
                break;
            case E_SE.back:
                SEManager.PlaySE(SEManager.back);
                break;
            case E_SE.decision:
                SEManager.PlaySE(SEManager.decision);
                break;
            case E_SE.explosionEnemy:
                SEManager.PlaySE(SEManager.explosionEnemy);
                break;
            case E_SE.explosionObstacle:
                SEManager.PlaySE(SEManager.explosionObstacle);
                break;
            case E_SE.explosionPlayer:
                SEManager.PlaySE(SEManager.explosionPlayer);
                break;
            case E_SE.failed:
                SEManager.PlaySE(SEManager.failed);
                break;
            case E_SE.getItem:
                SEManager.PlaySE(SEManager.getItem);
                break;
            case E_SE.pause:
                SEManager.PlaySE(SEManager.pause);
                break;
            case E_SE.peopleSurprise:
                SEManager.PlaySE(SEManager.peopleSurprise);
                break;
            case E_SE.returnMoveDirection:
                SEManager.PlaySE(SEManager.returnMoveDirection);
                break;
            case E_SE.shotBeam:
                SEManager.PlaySE(SEManager.shotBeam);
                break;
            case E_SE.speedDown:
                SEManager.PlaySE(SEManager.speedDown);
                break;
            case E_SE.speedUp:
                SEManager.PlaySE(SEManager.speedUp);
                break;
            case E_SE.stopMovement:
                SEManager.PlaySE(SEManager.stopMovement);
                break;
            case E_SE.success:
                SEManager.PlaySE(SEManager.success);
                break;
        }
    }

    public static void PlaySE(E_SE playSE)
    {
        switch (playSE)
        {
            case E_SE.explosionMissile:
                SEManager.PlaySE(SEManager.explosionMissile);
                break;
            case E_SE.select:
                SEManager.PlaySE(SEManager.select);
                break;
            case E_SE.shot0:
                SEManager.PlaySE(SEManager.shot0);
                break;
            case E_SE.shot1:
                SEManager.PlaySE(SEManager.shot1);
                break;
            case E_SE.back:
                SEManager.PlaySE(SEManager.back);
                break;
            case E_SE.decision:
                SEManager.PlaySE(SEManager.decision);
                break;
            case E_SE.explosionEnemy:
                SEManager.PlaySE(SEManager.explosionEnemy);
                break;
            case E_SE.explosionObstacle:
                SEManager.PlaySE(SEManager.explosionObstacle);
                break;
            case E_SE.explosionPlayer:
                SEManager.PlaySE(SEManager.explosionPlayer);
                break;
            case E_SE.failed:
                SEManager.PlaySE(SEManager.failed);
                break;
            case E_SE.getItem:
                SEManager.PlaySE(SEManager.getItem);
                break;
            case E_SE.pause:
                SEManager.PlaySE(SEManager.pause);
                break;
            case E_SE.peopleSurprise:
                SEManager.PlaySE(SEManager.peopleSurprise);
                break;
            case E_SE.returnMoveDirection:
                SEManager.PlaySE(SEManager.returnMoveDirection);
                break;
            case E_SE.shotBeam:
                SEManager.PlaySE(SEManager.shotBeam);
                break;
            case E_SE.speedDown:
                SEManager.PlaySE(SEManager.speedDown);
                break;
            case E_SE.speedUp:
                SEManager.PlaySE(SEManager.speedUp);
                break;
            case E_SE.stopMovement:
                SEManager.PlaySE(SEManager.stopMovement);
                break;
            case E_SE.success:
                SEManager.PlaySE(SEManager.success);
                break;
        }
    }
}

public enum E_SE
{
    back,
    decision,
    explosionEnemy,
    explosionMissile,
    explosionObstacle,
    explosionPlayer,
    failed,
    getItem,
    pause,
    peopleSurprise,
    returnMoveDirection,
    select,
    shot0,
    shot1,
    shotBeam,
    speedDown,
    speedUp,
    stopMovement, 
    success,

}
