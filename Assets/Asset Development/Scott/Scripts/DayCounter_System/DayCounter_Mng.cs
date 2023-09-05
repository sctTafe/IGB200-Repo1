using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayCounter_Mng : MonoBehaviour
{
    public UnityEvent _OnDayChange;
    [SerializeField] private DayCounter_UI _DayCounterUI;
    private int _currentDay = 1;

    //void Start()
    //{
        
    //}
    //void Update()
    //{
        
    //}

    public void fn_updateToNextDay()
    {
        nextDay();
    }

    private void nextDay()
    {
        _currentDay++;
        _OnDayChange?.Invoke();
        if (_DayCounterUI != null)
        {
            _DayCounterUI.fn_UpdateCurrentDay(_currentDay);
        }
        else
        {
            Debug.LogError("DayCounter_Mng: Error No UI Element attatched ");
        }        
    }


}
