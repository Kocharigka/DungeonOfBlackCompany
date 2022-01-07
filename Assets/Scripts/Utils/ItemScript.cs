using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public ItemData data;
    private SpriteRenderer rend;
    private PlayerController player;
    [SerializeField]private GameObject takeButton;
    public int cost;
    public GameObject costHolder;
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
            if (!GameController.paused&& Input.GetKeyDown(KeyCode.R))
            {

                player.GetComponent<InventoryController>().Take(data);
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position,player.transform.position)<2)
        {
            takeButton.SetActive(true);
            takeButton.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(1, 0.5f, 0));
        }
        else
        {
            takeButton.SetActive(false);
        }
    }
}
