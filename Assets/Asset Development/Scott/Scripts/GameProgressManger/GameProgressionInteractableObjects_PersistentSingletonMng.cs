using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 
/// DOSE: Manages which progression gameobjects are active/disabled - Works with Mission_Basic_SO (they are just relays) 
/// 
/// 
/// </summary>
public class GameProgressionInteractableObjects_PersistentSingletonMng : MonoBehaviour
{

    #region Singelton Setup
    private static GameProgressionInteractableObjects_PersistentSingletonMng _instance;
    public static GameProgressionInteractableObjects_PersistentSingletonMng Instance
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
    public Action _OnChange_UpdateOfActiveList;

    // - For Foward Calls -
    public Missions_Basic_SO[] _AllMissionArray;


    // public for testing
    public List<int> _currentlyEnabaledGPIOs_IDs_List = new List<int>();
    private bool _isSetUpComplete = false;


    void Awake()
    {
        SingeltonSetup();
    }

    void Start()
    {
        ChangeIsSetupComplete();
    }


    // - Mission Progression Functions - 
    public void fn_CompleteMission(int _missionUID)
    {
        
        foreach (var missionSO in _AllMissionArray)
        {
            if (missionSO._missionUID == _missionUID)
            {
                // Enable Objects
                foreach (var missionSOToEnable in missionSO._EnabledOnCompletion_Array)
                {
                    missionSOToEnable.fn_SetEnabledState(true);
                }
                // Disable Current Mission Object 
                missionSO.fn_SetEnabledState(false);
            }      
        }
    }



    // - Object Enable / Disable Functions -
    public void fn_SetObjectToDisabled(int uID)
    {
        _currentlyEnabaledGPIOs_IDs_List.Remove(uID);
        _OnChange_UpdateOfActiveList?.Invoke();
    }

    public void fn_SetObjectToEnabled(int uID)
    {
        _currentlyEnabaledGPIOs_IDs_List.Add(uID);
        _OnChange_UpdateOfActiveList?.Invoke();
    }

    // - Special Case Functions -
    public void fn_AddObjectToEnableAtStartOfGame(int uID)
    {
        if (!_isSetUpComplete)
            fn_SetObjectToEnabled(uID);
    }

    //




    public bool fn_Get_IsGOEnabled(int id)
    {
        if (_currentlyEnabaledGPIOs_IDs_List != null && id != null)
        {
            return _currentlyEnabaledGPIOs_IDs_List.Contains(id);
        }
        else
        {
            Debug.LogError("GameProgressionInteractableObjects_PersistentSingletonMng; fn_Get_IsGOEnabled - Error!");
            return false;
        }
    }

    

    private async Task ChangeIsSetupComplete()
    {
        await ChangeBoolValueAfterOneSecondAsync(() => _isSetUpComplete = true);
    }
    private async Task ChangeBoolValueAfterOneSecondAsync(Action changeBoolAction)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        changeBoolAction();
    }


    #region Debugging
    [ContextMenu("Testing - Mission Observer Relay - Complete 1")]
    private void Testing_SetMissionActiveAndDIsableCurrent1()
    {
        fn_CompleteMission(1);
    }
    [ContextMenu("Testing - Mission Observer Relay - Complete 2")]
    private void Testing_SetMissionActiveAndDIsableCurrent2()
    {
        fn_CompleteMission(2);
    }
    #endregion
}
