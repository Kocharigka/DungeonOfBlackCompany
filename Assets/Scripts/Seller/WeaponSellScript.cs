using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSellScript : MonoBehaviour
{
    WeaponData[] toSell;
    public GameObject weaponPrefab;
    List<WeaponData> used = new List<WeaponData>();
    private GameObject current;
    // Start is called before the first frame update
    void Start()
    {
        toSell = Resources.LoadAll<WeaponData>("Weapon/");
        Invoke("UpdateAssortiment", 0.1f);

    }
    void UpdateAssortiment()
    {
        toSell = toSell.Where(val => val != PlayerController.instance.gameObject.GetComponentInChildren<WeaponScript>().currentWeapon).ToArray();
        Sell();
    }
    void Sell()
    {
        var data = toSell[Random.Range(0, toSell.Length)];
        toSell = toSell.Where(val => val != data).ToArray(); 
        Random.InitState(Random.Range(0, 100));
        if (current != null)
        {
            DestroyImmediate(current);
            current = null;
        }
        current = Instantiate(weaponPrefab, transform.position, Quaternion.identity, null);
        current.GetComponent<WeaponItem>().data = data;
        current.GetComponent<WeaponItem>().cost = 500;
    }

}
