using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Stack<ItemData> items=new Stack<ItemData>();
    Dictionary<string, float> powerUps = new Dictionary<string, float>();
    Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();
    public static InventoryController instance;

    private void Start()
    {
        instance = this;
    }
    public void Take(ItemData item)
    {
        items.Push(item);

        if (powerUps.ContainsKey(item.Attribute))
        {
            powerUps[item.Attribute] += item.power;
        }
        else
        {
            powerUps.Add(item.Attribute, item.power);
        }
        if (inventory.ContainsKey(item))
        {
            inventory[item] ++;
        }
        else
        {
            inventory.Add(item, 1);
        }

    }
}
