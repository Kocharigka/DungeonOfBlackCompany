using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewWave", menuName = "Waves/Wave")]

public class WaveData : ScriptableObject
{
    public List<GameObject> enemies;
 
}
