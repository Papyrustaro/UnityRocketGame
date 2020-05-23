using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBGM : MonoBehaviour
{
    [SerializeField] private AudioClip battleBGM;

    private void Awake()
    {
        BGMManager.battleBGM = this.battleBGM;
    }

}
