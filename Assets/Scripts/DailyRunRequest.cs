using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class DailyRunRequest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebRequest request = WebRequest.Create("http://localhost:8000/api/v1/today/");
        WebResponse response = request.GetResponse();
        Stream receiveStream = response.GetResponseStream();
        StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
        Debug.Log(readStream.ReadToEnd());
        string a = readStream.ReadToEnd();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
