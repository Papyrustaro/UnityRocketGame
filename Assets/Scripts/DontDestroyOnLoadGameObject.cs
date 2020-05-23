using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadGameObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
