using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerInMission11 : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(this.transform.position.y > 2)
        {
            this.transform.position = new Vector3(this.transform.position.x, 2f, 0f);
        }
    }
}
