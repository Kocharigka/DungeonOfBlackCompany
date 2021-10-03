using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoom : Room
{
    // Start is called before the first frame update
    private void Awake()
    {
        Dictionary<string, int> tmp1 = new Dictionary<string, int>()
        {
            {"Wizard",3 },
            
        };
        enemiesToSpawn = new Dictionary<int, Dictionary<string, int>>()
        {
            {1,tmp1 },
            {2,tmp1 }
        };
    }
}
