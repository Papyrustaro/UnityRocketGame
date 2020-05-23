using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySEInStart : MonoBehaviour
{
    [SerializeField] private E_SE playSE;

    private void Start()
    {
        PlaySEByEnum.PlaySE(this.playSE);
    }
}
