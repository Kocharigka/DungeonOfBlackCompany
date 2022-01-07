using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellControl : MonoBehaviour
{
    private PlayerController player;
    private SectorChooser chooser = new SectorChooser();
    private Shooter shooter;
    public SpellData spell;
    private float cooldown;
    private float timer;
    [SerializeField]private Slider slider;
    private MagicController magic;
    [SerializeField] private Image fill;
    [SerializeField] private Image spellImage;
    public KeyCode key;
    float nextIter = 0;
    bool shoot = false;
    float target;
    public int counter;

    void Start()
    {
        player = GetComponent<PlayerController>();
        shooter = player.GetComponent<Shooter>();
        magic = GetComponent<MagicController>();
        cooldown = spell.cooldown;
        timer = cooldown;
        spellImage.sprite = spell.sprite;
        PlayerController.instance.spells.Add(counter,spell);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < cooldown)
        {
            slider.value = timer-cooldown+nextIter;
            return;
        }
        else
        {
            fill.color = new Color(0, 0, 0, 0);
            slider.value = 0;
        }
        if (Input.GetKeyDown(key)&&!player.isCast&&!GameController.paused&&!player.bying)
        {
            player.isCast = true;
            player.animator.SetTrigger(spell.triggerName);
            shoot = true;
            target = chooser.getAngle(Camera.main.ScreenToWorldPoint(Input.mousePosition), player.transform.position);
        }
    }
    public void Cast()
    {
        if (!shoot)
        {
            return;
        }
        timer = 0 - magic.WaterStatus();
        nextIter = cooldown - timer;
        slider.maxValue = nextIter;
        fill.color = new Color(0.22f, 0.22f, 0.22f, a: 0.9f);
        shooter.Shoot(player.transform.position, target, spell);
        shoot = false;
    }
    public SpellData ChangeSpell(SpellData spell)
    {
        var tmp = this.spell;
        this.spell = spell;
        cooldown = spell.cooldown;
        spellImage.sprite = spell.sprite;
        PlayerController.instance.spells[counter]=spell;
        return tmp;
    }
}
