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
    public Bounds bounds;
    private GameObject dungeonHolder;

    private void Start()
    {
        dungeonHolder = GameObject.Find("GameController");
        bounds=GetComponent<Collider2D>().bounds;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        waves = wavesData.Count;

        if (collision.tag == "Player" && !collision.isTrigger&&!passed && currentWave == 0)
        {
            dungeonHolder.GetComponent<PostGen>().BarsCtrl(false);
            InvokeRepeating("checkEnemies", 0, 1f);
        }
    }
    private void spawnNextWave()
    {
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
        if (bounds.max.x + transform.position.x>obj.x && bounds.max.y+ transform.position.y > obj.y &&
            bounds.min.x+ transform.position.x < obj.x && bounds.min.y+ transform.position.y < obj.y)
        {
            return true;
        }
        return false;
    }
    public List<Vector2> getCorners()
    {
        return new List<Vector2>() { transform.position+bounds.max, transform.position+bounds.min, (Vector2)transform.position + new Vector2(bounds.max.x,bounds.min.y), (Vector2)transform.position+new Vector2(bounds.min.x,bounds.max.y)};
    }
    public void checkEnemies()
    {
        if (enemiesCurrent.Count == 0)
        {
            if (currentWave==waves)
            {
                dungeonHolder.GetComponent<PostGen>().BarsCtrl(true);
                passed = true;
                CancelInvoke("checkEnemies");
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
}
