using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using UnityEngine;
using Newtonsoft.Json;
public class NetController : MonoBehaviour
{
    public string server="127.0.0.1";
    public static NetController instance;
    private static readonly HttpClient client = new HttpClient();

    private void Start()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public string GetSeed()
    {
        var responseString = client.GetStringAsync("http://"+server+"/api/v1/daily/today/");
        var a = JsonUtility.FromJson<GameObject>(responseString.ToString());
        return JsonConvert.DeserializeObject<object>(responseString);

        return responseString.ToString();

    }
    public  (string,string)[] GetLeaderbord()
    {
        var responseString = client.GetStringAsync("http://" + server + "/api/v1/daily/leaderbord/");
        return responseString.ToString();
        ;


    }
    public async void PostLeaderbord()
    {
        var values = new Dictionary<string, string>
        {
            { "thing1", "hello" },
            { "thing2", "world" }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync("http://" + server + "/api/v1/daily/today/", content);

        var responseString = await response.Content.ReadAsStringAsync();
    }
    public async void PostSave()
    {
        var values = new Dictionary<string, string>
        {
            { "thing1", "hello" },
            { "thing2", "world" }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync("http://" + server + "/api/v1/daily/today/", content);

        var responseString = await response.Content.ReadAsStringAsync();
    }
    public void GetSave()
    {
        var responseString = client.GetStringAsync("http://" + server + "/api/v1/daily/today/");
    }

}
