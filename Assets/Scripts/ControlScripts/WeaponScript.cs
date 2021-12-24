using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    PlayerController player;
    public WeaponData currentWeapon;
    public Image weaponSlider;
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        player.animator.SetFloat("Weapon", currentWeapon.weaponAnim);
        weaponSlider.sprite = currentWeapon.icon;
        PlayerController.instance.weapon = currentWeapon;

    }
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (!GameController.paused&& Input.GetKeyDown(KeyCode.Mouse0) && !player.isAttacking)
        {
            player.damagedEnemies.Clear();
            player.isAttacking = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Enemy"&&collision.isTrigger)
        {
            if (player.damagedEnemies.Contains(collision))
            {
                return;
            }
            else
            {
                collision.GetComponent<Enemy>().GetDamage(player.damage);
                collision.GetComponent<Enemy>().GetStun();
                player.damagedEnemies.Add(collision);

            }
        }
    }

    public WeaponData ChangeWeapon(WeaponData data)
    {
        player.animator.SetFloat("Weapon", data.weaponAnim);
        var tmp = currentWeapon;
        currentWeapon = data;
        weaponSlider.sprite = data.icon;
        PlayerController.instance.weapon = currentWeapon;
        return tmp;

    }
}
