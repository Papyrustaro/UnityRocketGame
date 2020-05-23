using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    private static AudioSource audioSource;
    public static AudioClip shotBullet;
    public static AudioClip missileExplosion;

    private void Awake()
    {
        SEManager.audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySE(AudioClip audioClip)
    {
        SEManager.audioSource.PlayOneShot(audioClip);
    }
}
