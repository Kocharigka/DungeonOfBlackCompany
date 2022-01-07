using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.transform.position = GameObject.Find("StartRoom - StartRoomEdgar").GetComponentInChildren<SpriteRenderer>().transform.position-new Vector3(0,5,0);
        }
    }
}
