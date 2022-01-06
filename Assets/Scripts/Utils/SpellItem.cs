using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellItem : MonoBehaviour
{
    public SpellData data;
    private SpriteRenderer rend;
    private PlayerController player;
    public int cost = 0;
    public GameObject costHolder;
    public GameObject takeButton;
    private GameObject reroll;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = data.inGameSprite;
        costHolder.GetComponentInChildren<Text>().text = cost.ToString();
        reroll = GameObject.Find("rerollSpell");
    }


    private void Update()
    {
        if (takeButton.activeInHierarchy)
        {
            if (!GameController.paused && Input.GetKeyDown(KeyCode.Q) && player.money > cost)
            {
                player.money -= cost;
                cost = 0;
                data = PlayerController.instance.TakeSpell(data,KeyCode.Q);
                rend.sprite = data.inGameSprite;
                DestroyImmediate(reroll);
                
            }
            if (!GameController.paused && Input.GetKeyDown(KeyCode.E) && player.money > cost)
            {
                player.money -= cost;
                cost = 0;
                data = PlayerController.instance.TakeSpell(data, KeyCode.E);
                rend.sprite = data.inGameSprite;
                DestroyImmediate(reroll);
            }
            if (!GameController.paused && Input.GetKeyDown(KeyCode.F) && player.money > cost)
            {
                player.money -= cost;
                cost = 0;
                data = PlayerController.instance.TakeSpell(data, KeyCode.F);
                rend.sprite = data.inGameSprite;
                DestroyImmediate(reroll);
            }
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1)
        {
            takeButton.SetActive(true);
            PlayerController.instance.bying = true;

            takeButton.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(1, 0.5f, 0));
        }
        else
        {
            takeButton.SetActive(false);
            PlayerController.instance.bying = false;

        }
        if (cost == 0)
        {
            costHolder.SetActive(false);
        }
        else
        {
            costHolder.SetActive(true);
            costHolder.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-1, 0.5f, 0));

        }
    }
}
