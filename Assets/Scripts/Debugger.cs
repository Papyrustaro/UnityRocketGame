using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Debugger : MonoBehaviour
{
    private int hp;
    public int Hp { get; private set; }

    private void Awake()
    {
        this.Hp = this.hp;
    }
}
