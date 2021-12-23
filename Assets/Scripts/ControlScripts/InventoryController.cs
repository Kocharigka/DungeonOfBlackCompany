using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private Stack<ItemData> items=new Stack<ItemData>();
    public Dictionary<string, float> powerUps = new Dictionary<string, float>() 
    {
        {"damage",1 },
        {"speed",1},
        {"health",1},
        { "magicDamage",1},
        {"armor",1 }
    };
    public Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();
    public static InventoryController instance;

    private void Start()
    {
        instance = this;
    }
    public void Take(ItemData item)
    {
        if (item.Attribute=="heal")
        {
            PlayerController.instance.RestoreHealth(item.power);
            return;
        }
        items.Push(item);
        powerUps[item.Attribute] += item.power;
        if (item.Attribute=="health")
        {
            PlayerController.instance.UpdateHealth(item.power+1);
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
    public string GenerateInventorySeed()
    {
        var resp = "";
        foreach (var kvp in inventory)
        {
            resp += kvp.Key + kvp.Value.ToString();
        }
        return resp;
    }
}
