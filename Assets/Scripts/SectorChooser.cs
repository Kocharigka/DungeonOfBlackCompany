using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorChooser
{
    private Dictionary<string, (int, int)> attackSectors = new Dictionary<string, (int, int)>()
    {
        //{"LU",(0,90) },
        {"R",(45,135)},
        //{"LD",(90,180)},
        {"D",(135,225)},
        //{"RD",(180,270)},
        {"L",(225,315)}
        //{"RU",(270,360)}
    };
    public float getAngle(Vector2 target,Vector2 player)
    {
        target = new Vector2(target.x - player.x, target.y - player.y);
        float angle = Vector2.Angle(new Vector2(0, 10), target);
        if (target.x < 0)
        {
            angle = 360 - angle;
        }

        return angle;
    }
    public string getSector(float angle)
    {
        foreach (KeyValuePair<string, (int, int)> sector in attackSectors)
        {

            if (sector.Value.Item1 <= angle && angle <= sector.Value.Item2)
            {
                return sector.Key;
            }
        }
        return "U";
    }
    public bool targetInSector(string sector, HealthScript enemy,Vector2 player)
    {
        float angle = getAngle(enemy.transform.position,player);
        if (sector != "U")
        {
            if (attackSectors[sector].Item1 <= angle && angle <= attackSectors[sector].Item2 && !enemy.IsDead)
            {
                return true;
            }
        }
        else if (315 <= angle && angle < 360 || 0 < angle && angle < 45 && !enemy.IsDead)
        {
            return true;
        }
        return false;
    }
    public Vector2 sectorToVector(Vector2 target, Vector2 player)
    {
        string sector = getSector(getAngle(target, player));
        switch(sector)
        {
            case "U":
                return new Vector2(0, 1);
            case "R":
                return new Vector2(1, 0);
            case "D":
                return new Vector2(0, -1);
            case "L":
                return new Vector2(-1, 0);
        }
        return new Vector2(0, 0);
    }
}
