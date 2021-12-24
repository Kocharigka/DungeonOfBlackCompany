using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private List<ItemData> items=new List<ItemData>();
    public Dictionary<string, float> powerUps;
    public Dictionary<ItemData, int> inventory = new Dictionary<ItemData, int>();
    public static InventoryController instance;
    public GameObject inventoryWindow;
    public bool inventoryActive=false;

    private void Awake()
    {
        instance = this;
        powerUps= new Dictionary<string, float>()
    {
        {"damage",1 },
        {"speed",1},
        {"health",1},
        {"magicDamage",1},
        {"armor",1 }
    };
    }
    public void Take(ItemData item)
    {
        if (item.Attribute=="heal")
        {
            PlayerController.instance.RestoreHealth(item.power);
            return;
        }
        items.Add(item);
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryActive)
            {
                GameController.paused = false;
                Time.timeScale = 1;

            }
            else
            {
                GameController.paused = true;
                Time.timeScale = 0;
            }
            InventoryUpdate();
            inventoryActive = !inventoryActive;
            inventoryWindow.SetActive(inventoryActive);
        }
    }
    public void InventoryUpdate()
    {
        var arts=inventoryWindow.transform.Find("Artifacts");
        var counter = 1;
        foreach (var item in inventory)
        {
            var slot=arts.Find("Slot" + counter.ToString());
            slot.gameObject.SetActive(true);
            slot.Find("Image").GetComponent<Image>().sprite = item.Key.image;
            slot.Find("Count").GetComponent<Text>().text = item.Value.ToString();
            counter++;
        }
        var spells= inventoryWindow.transform.Find("Character");
        foreach (var item in PlayerController.instance.spells)
        {
            var slot = spells.Find("Slot" + item.Key);
            slot.Find("Image").GetComponent<Image>().sprite = item.Value.sprite;
        }
        spells.Find("Slot4").Find("Image").GetComponent<Image>().sprite = PlayerController.instance.weapon.icon;
        spells.Find("Slot6").Find("Image").GetComponent<Image>().sprite = PlayerController.instance.dash.sprite;
        spells.Find("Money").GetComponent<Text>().text = PlayerController.instance.money.ToString();
    }

    public void GetSprite(string key)
    {
        if (key=="weapon")
        {
            var spell = PlayerController.instance.weapon;
            inventoryWindow.transform.Find("Information").Find("Text").GetComponent<Text>().text = "Damage: " + spell.damage + "\nSpeed: " + spell.speed;
        }
        else if (key=="dash")
        {
            var descr = PlayerController.instance.dash.name;
            inventoryWindow.transform.Find("Information").Find("Text").GetComponent<Text>().text = descr.ToString();
        }
        else if (PlayerController.instance.spells.ContainsKey(int.Parse(key)))
        {
            var spell = PlayerController.instance.spells[int.Parse(key)];
            var level = spell.effectPower + spell.resonancePower;
            inventoryWindow.transform.Find("Information").Find("Text").GetComponent<Text>().text = "Element: " + spell.element + "\nDamage: " + spell.damage + "\nSpell level:  " + level;
        }
    }
    public void GetEffect(GameObject slot)
    {
        var img = slot.gameObject.GetComponent<Image>().sprite;
        var num = int.Parse(slot.name.Substring(4));
        var data = items[num-1];
        inventoryWindow.transform.Find("Information").Find("Text").GetComponent<Text>().text = data.itemName + "\n" + data.Attribute + " + " + data.power*100+"%";        
    }
}
