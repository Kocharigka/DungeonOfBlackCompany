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
        public int id;
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

    public string server="127.0.0.1:8000";
    public static NetController instance;
    private static readonly HttpClient client = new HttpClient();

    private void Start()
    {
        instance = this;
    }
    public async void GetSeed()
    {
        var responseString = await client.GetStringAsync("http://"+server+"/api/v1/daily/today/");
        var a = JsonUtility.FromJson<Seed>(responseString.Trim('[', ']'));
        GameController.seed = a.seed;
        GameController.seedId = a.id;
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
    public async void PostLeaderbord()
    {
        var values = new Dictionary<string, string>
        {
            { "seed", GameController.seedId.ToString() },
            { "name", GameController.nick },
            {"score",GameController.score.ToString() }
        };

        Debug.Log(values["seed"]);

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync("http://" + server + "/api/v1/daily/leaderbord/", content);

        var responseString = await response.Content.ReadAsStringAsync();
        Debug.Log(responseString);
    }
}
