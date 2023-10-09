using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// IS: Singelton
/// DOSE: Handles ProjectDay Progression 
/// </summary>
/// 
public class DayCounter_PersistentSingletonMng : MonoBehaviour
{

    #region Singelton Setup
    private static DayCounter_PersistentSingletonMng _instance;
    public static DayCounter_PersistentSingletonMng Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("DayCounter_Mng instance is not found.");
            }
            return _instance;
        }
    }
    private void SingeltonSetup()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    // - Events - 
    public UnityEvent _OnDayChange_UnityEvent;      // used for any effects to be attacked to
    public Action _OnDayChange;   // used for internal subscriptions 
    public Action <int> _OnDayChange_CurrentDay;    // used for updating the UI element

    // - Private - 
    private int _currentDay = 1;

    void Awake()
    {
        SingeltonSetup();
    }


    // - public functions - 
    public void fn_updateToNextDay()
    {
        nextDay();
    }
    public int fn_GetCurrentDay()
    {
        return _currentDay;
    }

    private void nextDay()
    {
        _currentDay++;
        _OnDayChange?.Invoke();
        _OnDayChange_UnityEvent?.Invoke();
        _OnDayChange_CurrentDay?.Invoke(_currentDay);
    }
}
