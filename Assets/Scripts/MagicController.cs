using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicController : MonoBehaviour
{
    public Component target;
    public Image effectHolder;
    private Dictionary<string, Sprite> statuses=new Dictionary<string,Sprite>();
    private Vector3 effectHolderOffcet;
    private Coroutine currentStatus;
    private string lastStatusName;
    void Start()
    {
        if (gameObject.tag == "Player")
        {
           target = GetComponent<PlayerController>();
        }
        else
        {
           target = GetComponent<Enemy>();
        }
        GameObject tmp = (GameObject)Resources.Load("fireEffect");
        statuses.Add("Fire", tmp.GetComponent<SpriteRenderer>().sprite);
        tmp = (GameObject)Resources.Load("iceEffect");
        statuses.Add("Ice", tmp.GetComponent<SpriteRenderer>().sprite);
        tmp = (GameObject)Resources.Load("waterEffect");
        statuses.Add("Water", tmp.GetComponent<SpriteRenderer>().sprite);
        tmp = (GameObject)Resources.Load("elecEffect");
        statuses.Add("Elec", tmp.GetComponent<SpriteRenderer>().sprite);
        effectHolderOffcet = new Vector3(0, 2, 0);
    }
    private void Update()
    {
        effectHolder.transform.position = Camera.main.WorldToScreenPoint(transform.position + effectHolderOffcet);
    }

    public void ApplyEffect(MagicEffect effect)
    {
        if (currentStatus==null)
        {
            currentStatus=StartCoroutine(effect.EffectName,effect);
        }
        else
        {
            StopCoroutine(currentStatus);
            currentStatus=StartCoroutine(lastStatusName+effect.EffectName, effect);
        }
    }

    IEnumerator Fire(MagicEffect effect)
    {
        effectHolder.sprite = statuses["Fire"];

        effectHolder.gameObject.SetActive(true);
        for (int i = 0; i < effect.power; i++)
        {
            target.BroadcastMessage("GetDamage", 2*effect.power);
            yield return new WaitForSeconds(1);
        }
        effectHolder.gameObject.SetActive(false);

    }
    IEnumerator Ice(MagicEffect effect)
    {
        effectHolder.sprite = statuses["Ice"];

        effectHolder.gameObject.SetActive(true);
        target.BroadcastMessage("Slow");
        yield return new WaitForSeconds(effect.power);
        target.BroadcastMessage("SetDefaultSpeed");
        effectHolder.gameObject.SetActive(false);

    }
    IEnumerator Water(MagicEffect effect)
    {
        effectHolder.sprite = statuses["Water"];
        effectHolder.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        effectHolder.gameObject.SetActive(false);


    }
    IEnumerator Elec(MagicEffect effect)
    {
        effectHolder.sprite = statuses["Elec"];
        effectHolder.gameObject.SetActive(true);
        for (int i = 0; i <= effect.power; i++)
        {
            target.BroadcastMessage("GetStun", 0.5f);
            yield return new WaitForSeconds(1);
        }
        effectHolder.gameObject.SetActive(false);

    }
}
