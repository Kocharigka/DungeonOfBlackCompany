using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSellScript : MonoBehaviour
{
    WeaponData toSell;
    public GameObject weaponPrefob;
    // Start is called before the first frame update
    void Start()
    {
        toSell = Resources.Load<WeaponData>("Weapon/Spear");
        var weapon = Instantiate(weaponPrefob, transform.position, Quaternion.identity, null);
        weapon.GetComponent<WeaponItem>().data = toSell;
        weapon.GetComponent<WeaponItem>().cost = 500;
    }

    // Update is called once per frame

}
