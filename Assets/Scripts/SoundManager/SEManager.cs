using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static AudioSource audioSource;

    public static AudioClip back;
    public static AudioClip decision;
    public static AudioClip explosionEnemy;
    public static AudioClip explosionMissile;
    public static AudioClip explosionObstacle;
    public static AudioClip explosionPlayer;
    public static AudioClip failed;
    public static AudioClip getItem;
    public static AudioClip pause;
    public static AudioClip peopleSurprise;
    public static AudioClip returnMoveDirection;
    public static AudioClip select;
    public static AudioClip shotBeam;
    public static AudioClip shot0;
    public static AudioClip shot1;
    public static AudioClip speedDown;
    public static AudioClip speedUp;
    public static AudioClip stopMovement;
    public static AudioClip success;
    public static AudioClip page;
    public static AudioClip generate;

    private void Awake()
    {
        SEManager.audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySE(AudioClip audioClip)
    {
        SEManager.audioSource.PlayOneShot(audioClip);
    }
}
