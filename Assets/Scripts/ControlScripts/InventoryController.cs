using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private List<ItemData> items;
    public void Take(ItemData item)
    {
        items.Add(item);
    }
}
