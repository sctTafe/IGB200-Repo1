using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public Slider hpSlider;
    public Slider[] hpSliders;
    public GameObject[] characters;

    public void SetHUD()
    {
        for(int i = 0; i < hpSliders.Length; i++)
        {
            hpSliders[i].maxValue = characters[i].GetComponent<Unit>().maxHP;
            hpSliders[i].value = characters[i].GetComponent<Unit>().currentHP;
        }
        //hpSliders[index].maxValue = unit.maxHP;
        //hpSliders[index].value = unit.currentHP;
    }

    public void SetHP(int hp, int index)
    {
        hpSliders[index].value = hp;
    }
}
