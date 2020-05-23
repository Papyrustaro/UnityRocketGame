using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySE : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    public void PlayAudioClip()
    {
        SEManager.PlaySE(this.audioClip);
    }
}
