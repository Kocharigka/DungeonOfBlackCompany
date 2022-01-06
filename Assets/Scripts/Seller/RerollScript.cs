using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollScript : MonoBehaviour
{
    public int cost = 30;
    public GameObject costHolder;
    public GameObject takeButton;
    public GameObject seller;
    public int rerollCount=2;
    // Start is called before the first frame update


    private void Update()
    {
        if (takeButton.activeInHierarchy)
        {           
            if (!GameController.paused && Input.GetKeyDown(KeyCode.R) && PlayerController.instance.money > cost)
            {
                PlayerController.instance.money -= cost;
                seller.BroadcastMessage("Sell");
                rerollCount--;
                if (rerollCount==0)
                {
                    DestroyImmediate(gameObject);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) < 1)
        {
            takeButton.SetActive(true);
            takeButton.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(1, 0.5f, 0));
        }
        else
        {
            takeButton.SetActive(false);
        }
        costHolder.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-1, 0.5f, 0));
    }
}
