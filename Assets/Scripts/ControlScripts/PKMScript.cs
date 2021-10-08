using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PKMScript : MonoBehaviour
{
    private PlayerController player;
    private SectorChooser chooser = new SectorChooser();
    private Shooter shooter;
    [SerializeField]private SpellData spell;
    private float cooldown = 5;
    private float timer=5;
    private Slider slider;
    [SerializeField]private Image fill;
    [SerializeField]private Image spellImage;

    void Start()
    {
        slider = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shooter = player.GetComponent<Shooter>();
        slider.maxValue = cooldown;
        spellImage.sprite = spell.sprite;
        cooldown = spell.cooldown;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < cooldown)
        {
            slider.value = timer;
            return;
        }
        else
        {
            fill.color = new Color(0, 0, 0, 0);
            slider.value = 0;
        }
        if (Input.GetMouseButtonDown(1))
        {
           timer = 0;
           fill.color =new Color(0.22f, 0.22f, 0.22f, a:0.7f);
           shooter.Shoot(player.transform.position,chooser.getAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition), player.transform.position),spell);
        }
    }
}
