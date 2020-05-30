using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBGM : MonoBehaviour
{
    [SerializeField] private AudioClip menuBGM;
    [SerializeField] private AudioClip missionBGM;
    [SerializeField] private AudioClip bossBGM0;
    [SerializeField] private AudioClip bossBGM1;

    private void Awake()
    {
        BGMManager.menuBGM = this.menuBGM;
        BGMManager.missionBGM = this.missionBGM;
        BGMManager.bossBGM0 = this.bossBGM0;
        BGMManager.bossBGM1 = this.bossBGM1;
    }

}
