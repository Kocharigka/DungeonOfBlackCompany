using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterTeleport : MonoBehaviour
{
    public Image img;
    public string anchor;
    static bool teleported=true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && teleported)
        {
            teleported = false;
            StartCoroutine(Teleport(collision));
        }
    }
    IEnumerator Teleport(Collider2D collision)
    {
        GameObject.Find("GameController").GetComponent<GameController>().Faid(false, img);
        yield return new WaitForSeconds(0.5f);
        if (name== "entrance")
        {
            collision.transform.position = GameObject.Find(anchor).GetComponentInChildren<SpriteRenderer>().transform.position - new Vector3(0, 5, 0);
        }
        else
        {
            collision.transform.position = GameObject.Find(anchor).transform.position;
        }
        GameObject.Find("GameController").GetComponent<GameController>().Faid(true, img);
        teleported = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!teleported)
        {
            PlayerController.instance.blockInput = true;
        }
        else
        {
            PlayerController.instance.blockInput = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        teleported = true;
        PlayerController.instance.blockInput = false;
    }
}
