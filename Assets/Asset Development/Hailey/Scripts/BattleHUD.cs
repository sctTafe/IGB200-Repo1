using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public Slider[] hpSliders;
    public TMP_Text[] names;
    public List<GameObject> characters;
    public GameObject[] specialAttackButtons;

    public GameObject[] icons;
    public Sprite[] iconSprites;

    public void SetHUD()
    {
        for(int i = 0; i < hpSliders.Length; i++)
        {
            //hpSliders[i].gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color =
              //  characters[i].GetComponent<Unit>().color;
            hpSliders[i].maxValue = characters[i].GetComponent<Unit>().maxHP;
            hpSliders[i].value = characters[i].GetComponent<Unit>().currentHP;
            names[i].text = characters[i].GetComponent<Unit>().unitName;
        }

        for (int i = 0; i < icons.Length; i++)
        {
            GameObject icon = icons[i];
            Unit player = characters[i].GetComponent<Unit>();
            Sprite iconSprite;

            if (player.type == UnitType.Water)
            {
                iconSprite = iconSprites[3];
            }
            else if (player.type == UnitType.Electric)
            {
                iconSprite = iconSprites[4];
            }
            else if (player.type == UnitType.Management)
            {
                iconSprite = iconSprites[2];
            }
            else if (player.type == UnitType.Practical)
            {
                iconSprite = iconSprites[0];
            }
            else if (player.type == UnitType.Planning)
            {
                iconSprite = iconSprites[1];
            }
            else
            {
                Debug.Log("error");
                iconSprite = iconSprites[0];
            }

            icon.GetComponent<Image>().sprite = iconSprite;
        }
    }

    public void SetHP(int hp, int index)
    {
        hpSliders[index].value = hp;
    }

    public void SetButtons(Unit player)
    {
        if(player.type == UnitType.Management)
        {
            for (int i = 0; i < specialAttackButtons.Length; i++)
            {
                if (i == 0)
                {
                    specialAttackButtons[i].SetActive(true);
                }
                else
                {
                    specialAttackButtons[i].SetActive(false);
                }
            }
        }
        else if(player.type == UnitType.Electric)
        {
            for (int i = 0; i < specialAttackButtons.Length; i++)
            {
                if (i == 1)
                {
                    specialAttackButtons[i].SetActive(true);
                }
                else
                {
                    specialAttackButtons[i].SetActive(false);
                }
            }
        }
        else if(player.type == UnitType.Water)
        {
            for (int i = 0; i < specialAttackButtons.Length; i++)
            {
                if (i == 2)
                {
                    specialAttackButtons[i].SetActive(true);
                }
                else
                {
                    specialAttackButtons[i].SetActive(false);
                }
            }
        }
        else if(player.type == UnitType.Planning)
        {
            for (int i = 0; i < specialAttackButtons.Length; i++)
            {
                if (i == 3)
                {
                    specialAttackButtons[i].SetActive(true);
                }
                else
                {
                    specialAttackButtons[i].SetActive(false);
                }
            }
        }
        else if(player.type == UnitType.Practical)
        {
            for (int i = 0; i < specialAttackButtons.Length; i++)
            {
                if (i == 4)
                {
                    specialAttackButtons[i].SetActive(true);
                }
                else
                {
                    specialAttackButtons[i].SetActive(false);
                }
            }
        }
    }
}
