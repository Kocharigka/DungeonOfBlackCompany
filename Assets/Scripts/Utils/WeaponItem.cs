using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public WeaponData data;
    private SpriteRenderer rend;
    private PlayerController player;
    [SerializeField] private GameObject takeButton;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = data.inGameSprite;
    }


    private void Update()
    {
        if (takeButton.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {

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
        }
        else
        {
            takeButton.SetActive(false);
        }
    }
}
