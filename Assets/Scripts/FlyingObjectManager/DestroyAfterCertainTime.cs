using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyAfterCertainTime : MonoBehaviour
{
    [SerializeField] private float destroyTimeFromInstantiate = 5f;
    private float countTime = 0f;

    [SerializeField] private UnityEvent destroyEvent = null;

    private void Update()
    {
        this.countTime += Time.deltaTime;
        if(countTime >= this.destroyTimeFromInstantiate)
        {
            this.destroyEvent.Invoke();
            Destroy(this.gameObject);

            /*if(this.destroyEvent.GetPersistentEventCount() == 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.destroyEvent.Invoke(); 
            }*/
        }
    }
}
