using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class InstantiateAfterTime : MonoBehaviour
{
    [SerializeField] private float instantiateTime = 3f;
    [SerializeField] private GameObject[] instantiatePrefab;
    [SerializeField] private UnityEvent onInstantiateEvents;
    [SerializeField] private UnityEvent onStartEvents;
    

    private void Start()
    {
        this.onStartEvents.Invoke();
        StartCoroutine(DelayMethod(this.instantiateTime, () =>
        {
            foreach (GameObject prefab in this.instantiatePrefab)
            {
                Instantiate(prefab, this.transform.position, Quaternion.identity);
            }
            SEManager.PlaySE(SEManager.generate);
            this.onInstantiateEvents.Invoke();
        }));
    }

    IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
