using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Debugger : MonoBehaviour
{
    [field: SerializeField]
    [field: RenameField("health")]
    public int Health { get; private set; }

    [SerializeField] private InputField inputField;

    private void Start()
    {
        //this.inputField.Select();
        //SceneManager.LoadScene("SelectMissionStage");
    }
}
