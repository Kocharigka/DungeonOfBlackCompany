using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private string projectileName;
    private Transform holder;
    private bool isShooting=false;
    private float cooldown=0.5f;
    public float Cooldown
    {
        set { cooldown = value; }
    }
    public string ProjectileName
    {
        set { projectileName = value; }
    }
    private float delay;
    public float Delay
    {
        set { delay = value; }
    }
    private GameObject projectile;

    
  public void Shoot(Vector3 source,float angle,MagicEffect effect)
    {
        if (!isShooting)
        {
            projectile = (GameObject)Resources.Load("Projectiles/"+projectileName);
            holder = GameObject.Find("ProjectileHolder").GetComponent<Transform>();
            isShooting = true;
            StartCoroutine(WaitForShot(source, angle,effect));
        }
       
    }
    IEnumerator WaitForShot(Vector3 source, float angle,MagicEffect effect)
    {

        yield return new WaitForSeconds(delay);
        GameObject projectileGO = Instantiate(projectile, position: source, Quaternion.Euler(0, 0, 360 - angle), holder);
        projectileGO.GetComponent<ProjectileScript>().Effect = effect;
        yield return new WaitForSeconds(cooldown);
        isShooting = false;
    }
}
