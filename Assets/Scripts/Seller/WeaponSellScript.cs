using System.Collections;
using System.Collections.Generic;
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
        used.Add(PlayerController.instance.gameObject.GetComponentInChildren<WeaponScript>().currentWeapon);
        Sell();
    }
    void Sell()
    {
        var data = toSell[Random.Range(0, toSell.Length - 1)];
        Debug.Log(used.Count);
        Debug.Log(toSell.Length);
        var i = 0;
        while (i<10)
        {
            if (used.Count == toSell.Length)
            {
                return;
            }
            if (!used.Contains(data))
            {
                used.Add(data);
                break;
            }
            else
            {
                Random.InitState(Random.Range(0, 100));
                data = toSell[Random.Range(-1, toSell.Length)];
                Debug.Log(data);
                i++;
            }
        }
        Debug.Log(data.name);
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
