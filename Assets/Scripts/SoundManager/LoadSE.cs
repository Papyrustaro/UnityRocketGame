using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSE : MonoBehaviour
{
    [SerializeField] private AudioClip shotBullet;
    [SerializeField] private AudioClip missileExplosion;


    private void Awake()
    {
        SEManager.shotBullet = this.shotBullet;
        SEManager.missileExplosion = this.missileExplosion;
    }
}
