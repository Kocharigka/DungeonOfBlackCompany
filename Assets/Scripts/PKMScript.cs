using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKMScript : MonoBehaviour
{
    private PlayerController player;
    private SectorChooser chooser = new SectorChooser();
    private Shooter shooter;
    MagicEffect effect = new MagicEffect("Ice",2,2,2);
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shooter = player.GetComponent<Shooter>();
        shooter.Cooldown = 0;
        shooter.ProjectileName = "arrow";
        shooter.Delay = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
           shooter.Shoot(player.transform.position,
               chooser.getAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition), player.transform.position),
               effect);
        }
    }
}
