using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Edgar.Unity;

public class PostGen : MonoBehaviour
{
    WaveData[] waves;
    WaveData[] bosses;
    void Start()
    {

        waves = Resources.LoadAll<WaveData>("Waves/");
        bosses = Resources.LoadAll<WaveData>("WavesBoss/");
        Invoke("AfterStart", 0.01f);
    }

    void AfterStart()
    {
        foreach (Transform child in GameObject.Find("Rooms").transform)
        {
            if (Regex.IsMatch(child.name, ".*StartRoom.*"))
            {
                Debug.Log(child.position);
                GameObject.Find("Player").transform.position = child.position;
            }
            else
            {
                var rm=child.GetComponent<Room>();
                if (Regex.IsMatch(child.name, ".*BossRoom.*"))
                {
                    rm.wavesData.Add(bosses[Random.Range(0, bosses.Length-1)]);
                }
                else
                {
                    var wavesCount = Random.Range(1, 4);
                    for (int i = 0; i <= wavesCount; i++)
                    {
                        rm.wavesData.Add(waves[Random.Range(0, waves.Length-1)]);

                    }
                }
                
            }

        }
    }
}
