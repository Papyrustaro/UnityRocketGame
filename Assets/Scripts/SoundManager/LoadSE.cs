using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSE : MonoBehaviour
{
    [SerializeField] private AudioClip back;
    [SerializeField] private AudioClip decision;
    [SerializeField] private AudioClip explosionEnemy;
    [SerializeField] private AudioClip explosionMissile;
    [SerializeField] private AudioClip explosionObstacle;
    [SerializeField] private AudioClip explosionPlayer;
    [SerializeField] private AudioClip failed;
    [SerializeField] private AudioClip getItem;
    [SerializeField] private AudioClip pause;
    [SerializeField] private AudioClip peopleSurprise;
    [SerializeField] private AudioClip returnMoveDirection;
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip shotBeam;
    [SerializeField] private AudioClip shot0;
    [SerializeField] private AudioClip shot1;
    [SerializeField] private AudioClip speedDown;
    [SerializeField] private AudioClip speedUp;
    [SerializeField] private AudioClip stopMovement;
    [SerializeField] private AudioClip success;


    private void Awake()
    {
        SEManager.back = this.back;
        SEManager.decision = this.decision;
        SEManager.explosionEnemy = this.explosionEnemy;
        SEManager.explosionMissile = this.explosionMissile;
        SEManager.explosionObstacle = this.explosionObstacle;
        SEManager.explosionPlayer = this.explosionPlayer;
        SEManager.failed = this.failed;
        SEManager.getItem = this.getItem;
        SEManager.pause = this.pause;
        SEManager.peopleSurprise = this.peopleSurprise;
        SEManager.returnMoveDirection = this.returnMoveDirection;
        SEManager.select = this.select;
        SEManager.shotBeam = this.shotBeam;
        SEManager.shot0 = this.shot0;
        SEManager.shot1 = this.shot1;
        SEManager.speedDown = this.speedDown;
        SEManager.speedUp = this.speedUp;
        SEManager.stopMovement = this.stopMovement;
        SEManager.success = this.success;
    }
}
