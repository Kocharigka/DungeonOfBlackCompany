using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    public WeaponData data;
    private SpriteRenderer rend;
    private PlayerController player;
    public int cost = 0;
    [SerializeField] private GameObject takeButton;
    public GameObject costHolder;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = data.inGameSprite;
        costHolder.GetComponentInChildren<Text>().text = cost.ToString();
    }


    private void Update()
    {
        if (takeButton.activeInHierarchy)
        {
            if (!GameController.paused&& Input.GetKeyDown(KeyCode.R)&&player.money>cost)
            {
                player.money -= cost;
                cost = 0;
                data=player.GetComponentInChildren<WeaponScript>().ChangeWeapon(data);
                rend.sprite = data.inGameSprite;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 2)
        {
            takeButton.SetActive(true);
            takeButton.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(1, 0.5f, 0));
        }
        else
        {
            takeButton.SetActive(false);
        }
        if (cost==0)
        {
            costHolder.SetActive(false);
        }
        else
        {
            costHolder.SetActive(true);
            costHolder.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-1,0.5f,0));

        }
    }
}
