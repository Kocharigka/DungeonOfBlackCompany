using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashController : MonoBehaviour
{
    private float cooldown = 1;
    private float timer = 1;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    public DashData data;

    float nextIter = 0;
    private void Start()
    {
        PlayerController.instance.dash = data;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < cooldown)
        {
            slider.value = timer - cooldown + nextIter;
            return;
        }
        else
        {
            fill.color = new Color(0, 0, 0, 0);
            slider.value = 0;
        }
        if (!GameController.paused&& Input.GetKeyDown(KeyCode.Space) && timer > cooldown && PlayerController.instance.direction != new Vector2(0, 0)&& PlayerController.instance.moveSpeed != 0)
        {
            
            timer = 0 - PlayerController.instance.magic.WaterStatus();
            nextIter = cooldown - timer;
            slider.maxValue = nextIter;
            fill.color = new Color(0.22f, 0.22f, 0.22f, a: 0.7f);
            PlayerController.instance.animator.SetTrigger("Dash");
            timer = 0;
        }
    }
}
