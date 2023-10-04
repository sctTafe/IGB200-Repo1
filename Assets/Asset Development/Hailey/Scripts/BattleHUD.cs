using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class BattleHUD : MonoBehaviour
{
    public Slider sliderPrefab;
    public TMP_Text namePrefab;

    public Transform[] sliderLocations;
    public List<Slider> hpSliders;
    public List<TMP_Text> names;
    public List<GameObject> characters;
    
    public GameObject[] specialAttackButtons;

    public GameObject[] icons;
    public Sprite[] iconSprites;

    public bool isEnemyHUD;

    public void SetHUD()
    {
        if (isEnemyHUD)
        {
            for(int i=0; i < sliderLocations.Length; i++)
            {
                //add slider and name
                Slider newSlider = Instantiate(sliderPrefab, sliderLocations[i]);
                TMP_Text newName = Instantiate(namePrefab, sliderLocations[i]);
            
                hpSliders.Add(newSlider);
                names.Add(newName);
            
                //Set hp and name values
                hpSliders[i].maxValue = characters[i].GetComponent<Unit>().maxHP;
                hpSliders[i].value = characters[i].GetComponent<Unit>().currentHP;
                names[i].text = characters[i].GetComponent<Unit>().unitName;
            }

            return;
        }
        else
        {
            for(int i=0; i < characters.Count; i++)
            {
                //add slider and name
                Slider newSlider = Instantiate(sliderPrefab, sliderLocations[i]);
                TMP_Text newName = Instantiate(namePrefab, sliderLocations[i]);
            
                hpSliders.Add(newSlider);
                names.Add(newName);
            
                //Set hp and name values
                hpSliders[i].maxValue = characters[i].GetComponent<Unit>().maxHP;
                hpSliders[i].value = characters[i].GetComponent<Unit>().currentHP;
                names[i].text = characters[i].GetComponent<Unit>().unitName;
            }
        }
         
        /*for(int i = 0; i < hpSliders.Count; i++)
        {
            hpSliders[i].maxValue = characters[i].GetComponent<Unit>().maxHP;
            hpSliders[i].value = characters[i].GetComponent<Unit>().currentHP;
            names[i].text = characters[i].GetComponent<Unit>().unitName;
        }*/

        for (int i = 0; i < characters.Count; i++)
        {
            Unit player = characters[i].GetComponent<Unit>();
            GameObject icon = icons[i];
            
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

            icon.AddComponent<Image>();
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
