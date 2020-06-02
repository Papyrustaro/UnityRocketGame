using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private float spawnInterval = 5f;

    private float countTime;

    private void Update()
    {
        if (StageManager.Instance.IsStop) return;
        this.countTime += Time.deltaTime;
        if(countTime > this.spawnInterval)
        {
            //出現処理
            Instantiate(this.spawnObject, this.transform.position, Quaternion.identity);
            this.countTime = 0f;
        }
    }
}
