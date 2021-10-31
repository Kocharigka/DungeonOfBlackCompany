using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    private int waves;
    private int currentWave = 0;
    private List<GameObject> enemiesCurrent= new List<GameObject>();
    public List<WaveData> wavesData;
    private bool passed=false;
    private Bounds bounds;

    private void Start()
    {
        waves = wavesData.Count;
        bounds=GetComponent<Collider2D>().bounds;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !passed && currentWave == 0)
        {
            barsControl(false);
            spawnNextWave();
            InvokeRepeating("checkEnemies", 2.0f, 1f);
        }
    }
    private void spawnNextWave()
    {
        Debug.Log(currentWave);
        var wave = wavesData[currentWave].enemies;
        currentWave++;
        foreach (var enemy in wave)
        {
            GameObject tmp = Instantiate(enemy,new Vector3(transform.position.x+Random.Range(-3,3),transform.position.y+Random.Range(-3,3),0), Quaternion.identity, gameObject.transform);
            enemiesCurrent.Add(tmp);
        }
        
    }

    public bool InBounds(Vector3 obj)
    {       
        if (bounds.max.x>obj.x && bounds.max.y > obj.y &&
            bounds.min.x< obj.x && bounds.min.y<obj.y)
        {
            Debug.Log("InBounds");
            return true;
        }
        return false;
    }
    public List<Vector2> getCorners()
    {
        return new List<Vector2>() { bounds.max,bounds.min,new Vector2(bounds.max.x,bounds.min.y), new Vector2(bounds.min.x,bounds.max.y)};
    }
    public void checkEnemies()
    {
        if (enemiesCurrent.Count == 0)
        {
            if (currentWave==waves)
            {
                barsControl(true);
                passed = true;
            }
            else
            {
            spawnNextWave();
            }
        }
    }
    public void Killed(GameObject obj)
    {
        enemiesCurrent.Remove(obj);
    }
    void barsControl(bool state)
    {
        foreach (Transform child in transform)
        {
            foreach (Transform childChild in child)
            {
                if (childChild.name=="Bars")
                {

                    childChild.GetComponent<Animator>().SetBool("Opened", state);
                }
            }
        }
    }
}
