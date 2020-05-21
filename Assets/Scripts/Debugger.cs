using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Debugger : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(Math.Pow(2, 32) % 211);
        Debug.Log(Math.Pow(2, 64) % 211);
        Debug.Log((16 * 51 * 69) % 211);
    }
}
