using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Debugger : MonoBehaviour
{
    [field: SerializeField]
    [field: RenameField("health")]
    public int Health { get; private set; }
}
