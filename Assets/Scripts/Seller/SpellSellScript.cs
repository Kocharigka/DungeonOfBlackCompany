using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSellScript : MonoBehaviour
{
    SpellData[] toSell;
    public GameObject spellPrefab;
    List<SpellData> used=new List<SpellData>();
    private GameObject current;
    // Start is called before the first frame update
    void Start()
    {
        toSell = Resources.LoadAll<SpellData>("Spells/");
        Invoke("UpdateAssortiment", 0.1f);

    }
    void UpdateAssortiment()
    {
        foreach (var spell in PlayerController.instance.spells)
        {
            used.Add(spell.Value);
        }
        Sell();
    }
    void Sell()
    {
        var data = toSell[Random.Range(0, toSell.Length - 1)];
        while (true)
        {
            if (used.Count-1==toSell.Length)
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
            }
        }
        Random.InitState(Random.Range(0, 100));
        if (current!=null)
        {
            DestroyImmediate(current);
            current = null;
        }
        current = Instantiate(spellPrefab, transform.position, Quaternion.identity, null);
        current.GetComponent<SpellItem>().data = data;
        current.GetComponent<SpellItem>().cost = data.moneyCost;
    }
}
