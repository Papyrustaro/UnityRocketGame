using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static AudioSource audioSource;
    public static AudioClip battleBGM;

    private void Awake()
    {
        BGMManager.audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        BGMManager.PlayBGM(BGMManager.battleBGM);
    }

    public static void PlayBGM(AudioClip audioClip)
    {
        BGMManager.audioSource.PlayOneShot(audioClip);
    }
}
