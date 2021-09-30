using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicController : MonoBehaviour
{
    private Component target;
    public Image effectHolder;
    private Dictionary<string, Sprite> statuses=new Dictionary<string,Sprite>();
    private Vector3 effectHolderOffcet;
    private Coroutine currentStatus;
    private string currentStatusName;
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

    public void ApplyEffect(int damage,string effect)
    {
        Invoke(effect, 0);
    }

    public void Fire()
    {
        //dot
        effectHolder.sprite = statuses["Fire"];
        currentStatus=StartCoroutine(fire());
    }
    public void Water()
    {
        //???
        effectHolder.sprite = statuses["Water"];
        currentStatus = StartCoroutine(water());
    }
    public void Ice()
    {
        //slow
        effectHolder.sprite = statuses["Ice"];
        currentStatus=StartCoroutine(ice());
    }
    public void Elec()
    {
        //stun over time
        effectHolder.sprite = statuses["Elec"];
        currentStatus=StartCoroutine(elec());
    }
    IEnumerator fire()
    {
        effectHolder.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        effectHolder.gameObject.SetActive(false);
        currentStatusName = "";

    }
    IEnumerator ice()
    {
        effectHolder.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        effectHolder.gameObject.SetActive(false);
        currentStatusName = "";

    }
    IEnumerator water()
    {
        effectHolder.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        effectHolder.gameObject.SetActive(false);
        currentStatusName = "";

    }
    IEnumerator elec()
    {
        effectHolder.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        effectHolder.gameObject.SetActive(false);
        currentStatusName = "";

    }
}
