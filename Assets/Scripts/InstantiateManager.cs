using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> instantiateGameObjects = new List<GameObject>();
    [SerializeField] private bool destroyMeAfterInstantiate = true;

    public void InstantiateGameObject()
    {
        foreach(GameObject prefab in this.instantiateGameObjects)
        {
            Instantiate(prefab, this.transform.position, Quaternion.identity);
        }
        if (this.destroyMeAfterInstantiate)
        {
            Destroy(this.gameObject);
        }
    }
}
