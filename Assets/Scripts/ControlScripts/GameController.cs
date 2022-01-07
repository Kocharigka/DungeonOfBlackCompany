using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edgar.Unity;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static string seed = "";
    public static int seedId;
    public static string nick = "Unnamed";
    public static int score = 0;
    public static List<(string, int)> leaderbord = new List<(string, int)>();
    static List<(int, string, int)> parsedSeed = new List<(int, string, int)>();
    public static int currentLevel = 1;
    public static bool paused = false;
    NetController server = new NetController();
    public GameObject pauseScreen;
    public Image fader;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") && Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        if (paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
            PlayerController.instance.isAttacking = true;
        }
        pauseScreen.SetActive(!paused);
        paused = !paused;
        PlayerController.instance.isAttacking = false;

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

    public void GenerateLevel(bool daily)
    {
        var generator = GameObject.Find("Generator").GetComponent<DungeonGenerator>();
        var level = GetComponent<PostGen>();
        var currentLevelSeed = parsedSeed[currentLevel - 1];
        Debug.Log(currentLevelSeed);
        generator.RandomGeneratorSeed = currentLevelSeed.Item1;
        generator.UseRandomSeed = false;
        var lvlGraph = Resources.Load<LevelGraph>("LevelGrafs/Level" + currentLevelSeed.Item2);
        generator.FixedLevelGraphConfig.LevelGraph = lvlGraph;
        Random.InitState(currentLevelSeed.Item3);
        currentLevel++;
        generator.Generate();
        level.daily = daily;
        level.AfterStart();
        Faid(true, GameObject.Find("Fader").GetComponent<Image>());
        if (daily)
        {
            StartCoroutine(countScore());
        }

    }

    IEnumerator countScore()
    {
        score = 2000;
        while (!PlayerController.instance.isDead)
        {
            score--;
            yield return new WaitForSeconds(1);
        }
        server.PostLeaderbord();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void GenerateSeed()
    {
        var alphs = new char[] { 'A', 'B', 'C', 'D', 'E' };
        string seed = "";
        for (int i = 0; i < 4; i++)
        {
            seed += Random.Range(0, 9);
            seed += Random.Range(0, 9);
            seed += alphs[Random.Range(0, alphs.Length)];
            seed += Random.Range(0, 9);
            seed += Random.Range(0, 9);
            seed += '-';
        }
        seed = seed.Trim('-');
        Debug.Log(seed);
        ParseSeed(seed);
    }
    public void StartGame()
    {

        StartCoroutine(startGame());
    }
    IEnumerator startGame()
    {
        fader.gameObject.SetActive(true);
        Faid(false,fader);
        yield return new WaitForSeconds(1f);
        GenerateSeed();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(waitForLevelLoad(false));
    }
    IEnumerator waitForLevelLoad(bool daily)
    {
        yield return new WaitUntil(() => GameObject.Find("Generator") != null);
        GenerateLevel(daily);
    }

    public void Exit()
    {
        Application.Quit(0);
    }
    public void OpenWindow(GameObject window)
    {
        window.SetActive(true);
    }
    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
    public void DailyRun(GameObject window)
    {
        server.GetLeaderbord();
        server.GetSeed();
        StartCoroutine(waitForServerResponce(window));
    }
    IEnumerator waitForServerResponce(GameObject window)
    {
        leaderbord = new List<(string, int)>();
        seed = "";
        yield return new WaitUntil(() => seed != "");

        window.transform.Find("Loading").gameObject.SetActive(false);
        window.transform.Find("Title").gameObject.SetActive(true);
        window.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        for (int i = 0; i < leaderbord.Count; i++)
        {
            var line = window.transform.Find("line" + (i + 1).ToString());
            line.gameObject.SetActive(true);
            line.Find("Name").GetComponent<Text>().text = leaderbord[i].Item1;
            line.Find("Score").GetComponent<Text>().text = leaderbord[i].Item2.ToString();
            line.Find("Number").GetComponent<Text>().text = i.ToString();
        }
        window.transform.Find("Seed").Find("Seed").GetComponent<Text>().text = seed;
        ParseSeed(seed);
        Debug.Log(seed);
        window.SetActive(true);
    }
    public void playDaily()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(waitForLevelLoad(true));
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        ResumeGame();
        paused = false;
        pauseScreen.SetActive(false);
        DestroyImmediate(gameObject);
    }
    public void changeNick()
    {
        nick = GameObject.Find("NickField").GetComponent<InputField>().text;
    }
    public void Faid(bool faidTIme,Image img)
    {
        StartCoroutine(FadeImage(faidTIme, img));
    }
    IEnumerator FadeImage(bool fadeAway,Image img)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
