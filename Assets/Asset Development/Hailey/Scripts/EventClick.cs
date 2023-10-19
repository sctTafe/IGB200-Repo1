using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private BattleSystem battleSystem;
    private Unit unit;

    private void Awake()
    {
        battleSystem = GameObject.FindGameObjectWithTag("BattleSystem").GetComponent<BattleSystem>();
        unit = GetComponent<Unit>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //empty
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //empty
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(battleSystem.isHealing)
        {
            Debug.Log("heal");
            battleSystem.isHealing = !battleSystem.isHealing;
            unit.Heal();
            battleSystem.playerHUD.SetHP(unit.currentHP, unit.index);
            Debug.Log("unit index " + unit.index);
            StartCoroutine(battleSystem.PlayerHeal());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //empty
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //empty
    }
}
