using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;

    public void GenerateWeapon()
    {
        GameObject prefab = Instantiate(this.weaponPrefab, this.transform.parent.position, this.transform.parent.rotation);
        float z  = (this.transform.parent.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
        prefab.GetComponent<ForwardMovement>().SetMoveDirection(new Vector2(Mathf.Cos(z), Mathf.Sin(z)));
    }
}
