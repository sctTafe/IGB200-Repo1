using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public Slider[] hpSliders;
    public List<GameObject> characters;
    public GameObject healButton;
    public GameObject specialAttackButton;

    public void SetHUD()
    {
        for(int i = 0; i < hpSliders.Length; i++)
        {
            hpSliders[i].maxValue = characters[i].GetComponent<Unit>().maxHP;
            hpSliders[i].value = characters[i].GetComponent<Unit>().currentHP;
        }        
    }

    public void SetHP(int hp, int index)
    {
        hpSliders[index].value = hp;
    }

    public void SetButtons(Unit player)
    {
        if(player.isHealer)
        {
            healButton.SetActive(true);
            specialAttackButton.SetActive(false);
        }
        else
        {
            healButton.SetActive(false);
            specialAttackButton.SetActive(true);
        }
    }
}
