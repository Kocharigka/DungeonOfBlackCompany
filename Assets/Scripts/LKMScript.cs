using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LKMScript : MonoBehaviour
{

    private PlayerController player;
    public LayerMask enemyLayer;
    private SectorChooser chooser = new SectorChooser();
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            performAttack();
        }
    }
    private void performAttack()
    {
        string sector = chooser.getSector(chooser.getAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition),player.transform.position));
        Collider2D[] hitEnemites = Physics2D.OverlapCircleAll(player.transform.position, player.AttackRange, enemyLayer);
        foreach (Collider2D enemyCollider in hitEnemites)
        {
            Enemy enemy = enemyCollider.GetComponent<Enemy>();
            if (chooser.targetInSector(sector, enemy,player.transform.position))
            {
                enemy.GetDamage(2);
            }
        }
    }
}
