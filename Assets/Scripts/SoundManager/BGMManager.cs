using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static AudioSource audioSource;
    public static AudioClip menuBGM;
    public static AudioClip missionBGM;
    public static AudioClip bossBGM0;
    public static AudioClip bossBGM1;

    private void Awake()
    {
        BGMManager.audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayBGM(menuBGM);
    }

    public static void PlayBGM(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
