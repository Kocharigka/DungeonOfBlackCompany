using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public int waves;
    private int currentWave = 0;
    public List<GameObject> enemiesCurrent= new List<GameObject>();
    public Dictionary<int,Dictionary<string,int>> enemiesToSpawn;
    public bool passed=false;
    public Bounds bounds;

    private void Start()
    {
        waves = enemiesToSpawn.Keys.Count;
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
        currentWave++;
        foreach (KeyValuePair<string, int> enemy in enemiesToSpawn[currentWave])
        {
            for (int i = 0; i < enemy.Value; i++)
            {
                GameObject tmp = Instantiate(Resources.Load<GameObject>("Enemies/"+enemy.Key),  new Vector3(transform.position.x+Random.Range(-3,3),transform.position.y+Random.Range(-3,3),0), Quaternion.identity, gameObject.transform);
                enemiesCurrent.Add(tmp);
            }
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
