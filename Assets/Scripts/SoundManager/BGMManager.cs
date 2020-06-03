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
    public static E_BGM playingBGM;


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
        if (audioClip == bossBGM0) playingBGM = E_BGM.boss0;
        else if (audioClip == bossBGM1) playingBGM = E_BGM.boss1;
    }
}

public enum E_BGM
{
    menu,
    mission,
    boss0,
    boss1,
    other
}