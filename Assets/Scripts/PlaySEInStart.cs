using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySEInStart : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        SEManager.PlaySE(audioClip);
    }
}
