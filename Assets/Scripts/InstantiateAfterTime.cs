using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstantiateAfterTime : MonoBehaviour
{
    [SerializeField] private float instantiateTime = 3f;
    [SerializeField] private GameObject[] instantiatePrefab;
    [SerializeField] private UnityEvent onInstantiateEvents;
    [SerializeField] private UnityEvent onStartEvents;
    private float countTime = 0f;
    private bool instanted = false;

    private void Start()
    {
        this.onStartEvents.Invoke();
    }

    private void Update()
    {
        if (StageManager.Instance.IsStop) return;
        this.countTime += Time.deltaTime;
        if(!this.instanted && this.countTime >= this.instantiateTime)
        {
            this.instanted = true;
            foreach (GameObject prefab in this.instantiatePrefab)
            {
                Instantiate(prefab, this.transform.position, Quaternion.identity);
            }
            SEManager.PlaySE(SEManager.generate);
            this.onInstantiateEvents.Invoke();
        }
    }
}
