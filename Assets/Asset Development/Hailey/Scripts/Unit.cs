using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public enum UnitType 
{ 
    error,
    Water,
    Electric,
    Planning,
    Practical,
    Management
}

public class Unit : MonoBehaviour
{
    public string _teamMemberName;
    public string unitName;
    [FormerlySerializedAs("unitLevel")] public int index;
    public int level;
    public UnitType type;
    public UnitType weakness;

    public int damage;

    public int maxHP;
    public int currentHP;

    public int maxMorale;
    public int currentMorale;

    public int specialDamage;

    public bool isHealer;
    public bool isSleeping = false;

    public int numDeaths = 0;

    public Color color;

    //apply damage to unit
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            numDeaths++;
            currentHP = 0;
            return true;
        }
        else
            return false;
    }

    public void fn_Heal(bool isMoraleDepleting = false)
    {
        if (isMoraleDepleting)
        {
            currentHP = maxHP;
            fn_ReduceMorale(25);
        }
        else
        {
            currentHP = maxHP;
        }
    }

    public int fn_GetDamage(bool isSpecial = false)
    {
        if (isSpecial)
        {
            return specialDamage;
        }
        else
        {
            return damage;
        }
    }

    public int fn_GetEnergy_PctOfTotal(float pct)
    {
        return Mathf.RoundToInt(maxHP * (pct/100));
    }

    public float fn_GetRemainingMoralePct()
    {
        return ((currentMorale * 1f) / (maxMorale * 1f));
    }

    public void fn_ReduceMorale(float pct)
    {
        currentMorale -= Mathf.RoundToInt(maxMorale*1f *(pct/100));

        if (currentMorale < 0)
            currentMorale = 0;
    }
    public void fn_ReduceMorale(int amount) 
    {
        if (amount > 0)
            currentMorale -= amount;

        if (currentMorale<0)
            currentMorale = 0;
    }

}
