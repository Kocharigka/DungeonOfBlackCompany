using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Edgar.Unity;

public class PostGen : MonoBehaviour
{
    WaveData[] waves;
    WaveData[] bosses;
    public bool daily=false;

    public void AfterStart()
    {
        waves = Resources.LoadAll<WaveData>("Waves/");
        bosses = Resources.LoadAll<WaveData>("WavesBoss/");
        StartCoroutine(Gen());
    }
    IEnumerator Gen()
    {
        yield return new WaitUntil(()=>GameObject.Find("Generated Level")!=null);
        foreach (Transform child in GameObject.Find("Rooms").transform)
        {
            if (Regex.IsMatch(child.name, ".*StartRoom.*"))
            {
                if (daily)
                {
                    GameObject.Find("Player").transform.position = child.position;

                }
            }
            else
            {
                if (Regex.IsMatch(child.name, ".*Room.*"))
                {

                    var rm = child.GetComponent<Room>();
                    if (Regex.IsMatch(child.name, ".*BossRoom.*"))
                    {
                        rm.wavesData.Add(bosses[Random.Range(0, bosses.Length - 1)]);
                    }
                    else
                    {
                        var wavesCount = Random.Range(1, 4);
                        for (int i = 0; i <= wavesCount; i++)
                        {
                            rm.wavesData.Add(waves[Random.Range(0, waves.Length - 1)]);

                        }
                    }
                }

            }

        }
    }
    public void BarsCtrl(bool state)
    {
        foreach (Transform room in GameObject.Find("Rooms").transform)
        {
            if (Regex.IsMatch(room.name, ".*Corridor.*"))
            {
                room.GetComponentInChildren<Animator>().SetBool("Opened", state);
            }
        }        
    }
}
