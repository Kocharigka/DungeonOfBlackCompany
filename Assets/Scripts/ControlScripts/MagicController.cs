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
    private float waterPower;
    public bool holderFreeze = false;
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
        SpriteRenderer tmp = Resources.Load<SpriteRenderer>("MagicEffects/fireEffect");
        statuses.Add("Fire", tmp.sprite);
        tmp = Resources.Load<SpriteRenderer>("MagicEffects/iceEffect");
        statuses.Add("Ice", tmp.sprite);
        tmp = Resources.Load<SpriteRenderer>("MagicEffects/waterEffect");
        statuses.Add("Water", tmp.sprite);
        tmp = Resources.Load<SpriteRenderer>("MagicEffects/elecEffect");
        statuses.Add("Elec", tmp.sprite);
        tmp = Resources.Load<SpriteRenderer>("MagicEffects/freezeEffect");
        statuses.Add("Freeze", tmp.sprite);
        effectHolderOffcet = new Vector3(0, 2, 0);
    }
    private void Update()
    {
        if (!holderFreeze)
        {
            effectHolder.transform.position = Camera.main.WorldToScreenPoint(transform.position + effectHolderOffcet);
        }
    }

    public void ApplyEffect(MagicEffect effect)
    {
        if (effect.name=="None"|| enabled==false)
        {
            return;
        }
        if (currentStatus==null)
        {
            lastStatusName = effect.name;
            currentStatus=StartCoroutine(effect.name,effect);
        }
        else if (lastStatusName==effect.name)
        {
            effectHolder.gameObject.SetActive(false);
            StopCoroutine(currentStatus);
            currentStatus = StartCoroutine(effect.name, effect);
        }
        else
        {
            effectHolder.gameObject.SetActive(false);
            StopCoroutine(currentStatus);
            currentStatus=StartCoroutine(lastStatusName+effect.name, effect);
        }
    }

    #region simpleEffects
    IEnumerator Fire(MagicEffect effect)
    {
        effectHolder.sprite = statuses["Fire"];

        effectHolder.gameObject.SetActive(true);
        for (int i = 0; i < effect.power; i++)
        {
            target.BroadcastMessage("GetMagicDamage", 2*effect.power);
            yield return new WaitForSeconds(1);
        }
        effectHolder.gameObject.SetActive(false);
        currentStatus = null;
    }
    IEnumerator Ice(MagicEffect effect)
    {
        effectHolder.sprite = statuses["Ice"];

        effectHolder.gameObject.SetActive(true);
        if (gameObject.tag!="Player")
        {
            target.BroadcastMessage("Slow");
        }
        yield return new WaitForSeconds(effect.power);
        currentStatus = null;
        target.BroadcastMessage("setDefaultSpeed");
        effectHolder.gameObject.SetActive(false);
    }
    IEnumerator Water(MagicEffect effect)
    {
        effectHolder.sprite = statuses["Water"];
        effectHolder.gameObject.SetActive(true);
        waterPower = effect.power;
        yield return new WaitForSeconds(3);
        effectHolder.gameObject.SetActive(false);
        currentStatus = null;
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
        currentStatus = null;
    }
    #endregion simpleEffects

    #region fireResonance
    IEnumerator FireIce(MagicEffect effect)
    {
        steam(effect);
        yield return null;
    }
    IEnumerator FireWater(MagicEffect effect)
    {
        steam(effect);
        yield return null;
    }
    IEnumerator FireElec(MagicEffect effect)
    {
        if (gameObject.tag != "Player")
        {
            target.BroadcastMessage("set_IsImpulse", true);
        }
            yield return new WaitForSeconds(0.1f);
            effectHolder.gameObject.SetActive(false);
            currentStatus = null;
        if (gameObject.tag != "Player")
        {
            target.BroadcastMessage("set_IsImpulse", false);
        }
    }
    #endregion fireResonance

    #region waterResonance
    IEnumerator WaterFire(MagicEffect effect)
    {
        steam(effect);
        yield return null;
    }
    IEnumerator WaterIce(MagicEffect effect)
    {
        freeze(effect);
        yield return new WaitForSeconds(2 + effect.resonancePower);
        currentStatus = null;
        effectHolder.gameObject.SetActive(false);
    }
    IEnumerator WaterElec(MagicEffect effect)
    {
        effectHolder.sprite = statuses["Elec"];
        effectHolder.gameObject.SetActive(true);
        for (int i = 0; i <= effect.power*2; i++)
        {
            target.BroadcastMessage("GetStun", 1f);
            yield return new WaitForSeconds(2);
        }
        effectHolder.gameObject.SetActive(false);
        currentStatus = null;
    }
    #endregion waterResonance

    #region iceResonance
    IEnumerator IceFire(MagicEffect effect)
    {
        steam(effect);
        yield return null;
    }
    IEnumerator IceWater(MagicEffect effect)
    {
        freeze(effect);
        yield return new WaitForSeconds(2 + effect.resonancePower);
        currentStatus = null;
        effectHolder.gameObject.SetActive(false);
    }
    IEnumerator IceElec(MagicEffect effect)
    {
        conductor(effect);
        yield return null;
    }
    #endregion iceResonance

    #region elecResonance
    IEnumerator ElecFire(MagicEffect effect)
    {
        if (gameObject.tag != "Player")
        {
            target.BroadcastMessage("set_IsImpulse", true);
        }
        yield return new WaitForSeconds(0.1f);
        effectHolder.gameObject.SetActive(false);
        currentStatus = null;
        if (gameObject.tag != "Player")
        {
            target.BroadcastMessage("set_IsImpulse", false);
        }
    }
    IEnumerator ElecIce(MagicEffect effect)
    {
        conductor(effect);
        yield return null;
    }
    IEnumerator ElecWater(MagicEffect effect)
    {
        effectHolder.sprite = statuses["Elec"];
        effectHolder.gameObject.SetActive(true);
        for (int i = 0; i <= effect.power * 2; i++)
        {
            target.BroadcastMessage("GetStun", 1f);
            yield return new WaitForSeconds(2);
        }
        effectHolder.gameObject.SetActive(false);
        currentStatus = null;
    }
    #endregion elecResonance

    #region freezeResonance
    IEnumerator FreezeFire(MagicEffect effect)
    {
        steam(effect);
        return null;
    }
    IEnumerator FreezeIce(MagicEffect effect)
    {
        return null;
    }
    IEnumerator FreezeWater(MagicEffect effect)
    {
        return null;
    }
    IEnumerator FreezeElec(MagicEffect effect)
    {
        conductor(effect);
        yield return null;
    }
    #endregion freezeResonance

    #region shared
    void steam(MagicEffect effect)
    {
        effectHolder.gameObject.SetActive(false);
        currentStatus = null;
        target.BroadcastMessage("GetMagicDamage", effect.damage * (1 + (float)effect.resonancePower / 2));
    }
    void conductor(MagicEffect effect)
    {
        effectHolder.gameObject.SetActive(false);
        currentStatus = null;
        target.BroadcastMessage("GetMagicDamage", effect.damage * (2 + (float)effect.resonancePower / 2));
    }
    void freeze(MagicEffect effect)
    {
        effectHolder.sprite = statuses["Freeze"];
        effectHolder.gameObject.SetActive(true);
        lastStatusName = "Freeze";
        target.BroadcastMessage("GetStun", 2 + effect.resonancePower);
    }
    #endregion shared

    #region utils
    public bool canMove()
    {
        if(currentStatus==null)
        {
            return true;
        }
        if (currentStatus != null && !(lastStatusName == "Freeze" || lastStatusName == "Ice"))
        {
            return true;
        }
        return false;
    }

    public float WaterStatus()
    {
        if (currentStatus != null && lastStatusName=="Water")
        {
            return waterPower;
        }
        return 0;
    }
    #endregion utils
}
