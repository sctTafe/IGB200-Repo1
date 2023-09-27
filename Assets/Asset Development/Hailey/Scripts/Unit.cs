using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string unitName;
    public int unitLevel;
    public UnitType type;
    public UnitType weakness;

    public int damage;

    public int maxHP;
    public int currentHP;

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
            return true;
        }
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if(currentHP > maxHP)
            currentHP = maxHP;
    }
}
