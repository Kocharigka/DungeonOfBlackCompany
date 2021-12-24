using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class NetController : MonoBehaviour
{
    [Serializable] 
    public class Seed
    {
        public int seedId;
        public string seed;
    }
    [Serializable]
    public class LeaderbordRecord
    {
        public string name;
        public int score;
    }
    [Serializable]
    public class Leaderbord
    {
        public List<LeaderbordRecord> leaderbord=new List<LeaderbordRecord>();
    }


    [Serializable]
    public class Save
    {
        public string seed;
        public string nick;
        public float posX;
        public float posY;
        public float health;
        public string inventory;
        public int currentLevel;
        public string rooms_passed;
        public int score;
        public DateTime run_time;
    }

    public string server="127.0.0.1:8000";
    public static NetController instance;
    private static readonly HttpClient client = new HttpClient();

    private void Start()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public async void GetSeed()
    {
        var responseString = await client.GetStringAsync("http://"+server+"/api/v1/daily/today/");
        var a = JsonUtility.FromJson<Seed>(responseString.Trim('[', ']'));
        GameController.seed = a.seed;
        GameController.seedId = a.seedId;
    }
    public async void GetLeaderbord()
    {
        var responseString = await client.GetStringAsync("http://" + server + "/api/v1/daily/leaderbord/");
        responseString = "{ \"leaderbord\":[" + responseString.Trim('[', ']') + "]}";
        var b = JsonUtility.FromJson<Leaderbord>(responseString);
        foreach (LeaderbordRecord item in b.leaderbord)
        {
            GameController.leaderbord.Add((item.name,item.score));
        }
        Debug.Log(b.leaderbord[0].name);
    }
    public async void GetSave()
    {
        var responseString =await client.GetStringAsync("http://" + server + "/api/v1/daily/today/");
        var a = JsonUtility.FromJson<Save>(responseString.Trim('[', ']'));
    }
    public async void PostLeaderbord()
    {
        var values = new Dictionary<string, string>
        {
            { "seed", "4" },
            { "name", GameController.nick },
            {"score",GameController.score.ToString() }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync("http://" + server + "/api/v1/daily/leaderbord/", content);

        var responseString = await response.Content.ReadAsStringAsync();
        Debug.Log(responseString);
    }
    public async void PostSave()
    {
        var values = new Dictionary<string, string>
        {
            { "seed", GameController.seed },
            { "nick", GameController.nick },
            { "posX", PlayerController.instance.transform.position.x.ToString() },
            { "posY", PlayerController.instance.transform.position.y.ToString() },
            { "health", PlayerController.instance.getHealthPercents().ToString() },
            { "inventory", InventoryController.instance.GenerateInventorySeed() },
            { "currentLevel", GameController.currentLevel.ToString() },
            { "rooms_passed", "hello" },
            { "score", GameController.score.ToString() },
            { "run_time", DateTime.Now.ToString() },
        };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync("http://" + server + "/api/v1/daily/today/", content);

        var responseString = await response.Content.ReadAsStringAsync();
    }
}
