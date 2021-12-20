using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edgar.Unity;

public class GameController : MonoBehaviour
{
    public static string seed="";
    public static int seedId;
    public static string nick="dsdfsdf";
    public static int score=2222;
    public static (string, string)[] leaderbord= new (string, string)[10];
    static List<(int, string, int)> parsedSeed= new List<(int, string, int)>();
    public static int currentLevel=1;
    static bool paused = false;
    NetController server = new NetController();

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") && Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
            paused = !paused;
        }
    }


    public void ParseSeed(string seed)
    {
        GameController.seed = seed;
        foreach (var item in seed.Split('-'))
        {
            (int, string, int) tmp = (int.Parse(item.Substring(0, 2)), item.Substring(2, 1), int.Parse(item.Substring(3, 2)));
            parsedSeed.Add(tmp);
        }
    }

    public void GenerateLevel()
    {
        var currentLevelSeed = parsedSeed[currentLevel];
        GetComponent<DungeonGenerator>().RandomGeneratorSeed = currentLevelSeed.Item1;
        GetComponent<DungeonGenerator>().RandomGeneratorSeed = currentLevelSeed.Item1;
        var lvlGraph = Resources.Load<LevelGraph>("LevelGrafs/Level" + currentLevelSeed.Item2);
        GetComponent<DungeonGenerator>().FixedLevelGraphConfig.LevelGraph = lvlGraph;
        Random.InitState(currentLevelSeed.Item3);
        currentLevel++;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
