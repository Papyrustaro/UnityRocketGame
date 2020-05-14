using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();

    private void Start()
    {
        foreach(GameObject obj in this.gameObjects)
        {
            Debug.Log(obj.transform.right);
            //Debug.Log(obj.transform.left);
            //Debug.Log(obj.transform.);
        }
    }
}
