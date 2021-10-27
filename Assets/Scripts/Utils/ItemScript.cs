using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public ItemData data;
    private SpriteRenderer rend;
    private PlayerController player;
    private GameObject takeButton;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = data.image;
    }


    private void Update()
    {
        if (takeButton.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                player.GetComponent<InventoryController>().Take(data);
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position,player.transform.position)<2)
        {
            takeButton.SetActive(true);
        }
        else
        {
            takeButton.SetActive(false);
        }
    }
}
