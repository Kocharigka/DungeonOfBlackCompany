using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private GameObject arrow;
    private Transform holder;
    private bool isShooting=false;

    void Awake()
    {
        arrow = (GameObject)Resources.Load("arrow");
        holder = GameObject.Find("ProjectileHolder").GetComponent<Transform>();
    }
    
  public void Shoot(Vector3 source,float angle)
    {      
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(WaitForShot(source, angle));
        }
       
    }
    IEnumerator WaitForShot(Vector3 source, float angle)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(arrow, position: source, Quaternion.Euler(0, 0, 360 - angle), holder);
        isShooting = false;
    }
}
