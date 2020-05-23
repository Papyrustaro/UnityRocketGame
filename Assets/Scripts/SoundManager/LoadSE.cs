using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSE : MonoBehaviour
{
    [SerializeField] private AudioClip shotBullet;
    [SerializeField] private AudioClip explosion;

    private void Awake()
    {
        SEManager.shotBullet = this.shotBullet;
        SEManager.explosion = this.explosion;
    }
}
