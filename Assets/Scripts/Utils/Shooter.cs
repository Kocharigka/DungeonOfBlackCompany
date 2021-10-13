using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Transform holder;
    private GameObject projectile;

    void Awake()
    {
        projectile = Resources.Load<GameObject>("Projectiles/StandardSpell");
    }
    public void Shoot(Vector3 source,float angle,SpellData spell)
    {
        spell.target = gameObject.tag == "Enemy" ? "Player" : "Enemy";
        holder = GameObject.Find("ProjectileHolder").GetComponent<Transform>();
        GameObject projectileGO = Instantiate(projectile, source, Quaternion.Euler(0, 0, 360 - angle), holder);
        projectileGO.BroadcastMessage("Init", spell);     
    }
}
