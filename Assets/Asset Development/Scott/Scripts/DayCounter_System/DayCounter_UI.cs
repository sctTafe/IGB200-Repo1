using TMPro;
using UnityEngine;

public class DayCounter_UI : MonoBehaviour
{

    [SerializeField] private TMP_Text _dayTMP;

    private DayCounter_PersistentSingletonMng _DayCountManager;

    void Awake()
    {
        ConnectToDayCountManager();
        if (_DayCountManager != null)
        {
            // set to current day on start
            fn_UpdateCurrentDay(_DayCountManager.fn_GetCurrentDay());
        }
    }

    private void OnDestroy()
    {
        if (_DayCountManager != null) 
            _DayCountManager._OnDayChange_CurrentDay -= HandleOnDayChange;
    }

    public void fn_UpdateCurrentDay(int day)
    {
        _dayTMP.SetText(day.ToString());
    }

    private void ConnectToDayCountManager()
    {
        _DayCountManager ??= DayCounter_PersistentSingletonMng.Instance;
        if (_DayCountManager != null)
        {
            _DayCountManager._OnDayChange_CurrentDay += HandleOnDayChange;
        }
    }

    private void HandleOnDayChange(int day)
    {
        fn_UpdateCurrentDay(day);
    }

}
