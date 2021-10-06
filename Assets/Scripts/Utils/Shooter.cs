using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Transform holder;
    [SerializeField]private GameObject projectile;
    public void Shoot(Vector3 source,float angle,SpellData spell)
    {       
        holder = GameObject.Find("ProjectileHolder").GetComponent<Transform>();
        GameObject projectileGO = Instantiate(projectile, source, Quaternion.Euler(0, 0, 360 - angle), holder);
        projectileGO.GetComponent<ProjectileScript>().Init(spell);     
    }
}
