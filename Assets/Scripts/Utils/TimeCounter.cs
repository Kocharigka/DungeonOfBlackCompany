using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    void Update()
    {
        TimeSpan untilMidnight = DateTime.Today.AddDays(1.0) - DateTime.Now;
        GetComponent<Text>().text = untilMidnight.ToString();
    }
}
