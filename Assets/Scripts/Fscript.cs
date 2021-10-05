using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fscript : MonoBehaviour
{
    private PlayerController player;
    private SectorChooser chooser = new SectorChooser();
    private Shooter shooter;
    private string projectileName;
    MagicEffect effect = new MagicEffect("Water", 2, 2, 2);
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shooter = player.GetComponent<Shooter>();
        shooter.Cooldown = 0;
        projectileName = "waterball";
        shooter.Delay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            shooter.Shoot(player.transform.position,
                chooser.getAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition), player.transform.position),
                effect,projectileName);
        }
    }
}
