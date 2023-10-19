using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Slider hpSlider;
    public float maxHP;
    public Sprite[] icons;
    // Start is called before the first frame update
    void Start()
    {
        maxHP = hpSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpSlider.value >= maxHP * 0.75)
        {
            GetComponent<SpriteRenderer>().sprite = icons[0];
        }
        else if (hpSlider.value >= maxHP * 0.5 && hpSlider.value < maxHP * 0.75)
        {
            GetComponent<SpriteRenderer>().sprite = icons[1];
        }
        else if (hpSlider.value >= maxHP * 0.25 && hpSlider.value < maxHP * 0.5)
        {
            GetComponent<SpriteRenderer>().sprite = icons[2];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = icons[3];
        }
    }
}
