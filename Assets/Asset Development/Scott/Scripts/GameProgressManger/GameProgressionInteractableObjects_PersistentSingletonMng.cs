using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// DOSE: Manages which progression game objects are active/disabled; Works with 'Mission_Basic_SO' & 'Bridge_SO' (they work as data holders and event relays) 
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
                Debug.LogError("DayCounter_Mng Instance is not found.");
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

    #region Variables
    // - Events - 
    public Action _OnChange_UpdateOfMissionObjectsActiveList;
    public Action _OnChange_UpdateOfBridgeObjectsActiveList;

    // - Debugging -
    private bool _isDebuggingOn = false;

    // - For Forward Calls -
    public Missions_Basic_SO[] _AllMissionSORefArray;
    public BridgeParts_SO[] _AllBridgePartsSORefArray;

    // NOTE: lists are public for testing
    public List<int> _currentlyEnabaledMissionUIDs_List = new List<int>();
    public List<int> _currentlyEnabaledBuildingUIDs_List = new List<int>();

    private bool _isSetUpComplete = false;
    #endregion

    #region Unitys Native Functions
    void Awake() {
        SingeltonSetup();
    }
    void Start() {
        ChangeIsSetupComplete();
    }
    #endregion

    // - Mission Progression Functions - 
    // Call when Mission 'Won' to trigger changes
    public void fn_CompleteMission(int missionUID)
    {
        if (_isDebuggingOn) Debug.Log("GameProgressionInteractableObjects_PersistentSingletonMng: fn_CompleteMission - Called, with ID: [ " + missionUID + "]");
        foreach (var missionSO in _AllMissionSORefArray)
        {
            if (missionSO._missionUID == missionUID)
            {
                // Enable Objects in SO's '_MissionsEnabledOnCompletion_Array'
                foreach (var missionSOToEnable in missionSO._MissionsEnabledOnCompletion_Array)
                {
                    missionSOToEnable.fn_SetEnabledState(true);
                    // if ID not in list add the ID to the list - Uses both Push Method & a List update for if the event listener is not around
                    if (!_currentlyEnabaledMissionUIDs_List.Contains(missionSOToEnable._missionUID))
                        fn_SetMissionObjectToEnabled(missionSOToEnable._missionUID);
                }

                // Enable Objects in SO's '_MissionsEnabledOnCompletion_Array'
                foreach (var bridgePartsSOsToEnable in missionSO._BridgePartsEnabledOnCompletion_Array) {
                    //bridgePartsSOsToEnable.fn_SetEnabledState(true);
                    // if ID not in list add the ID to the list - Uses both Push Method & a List update for if the event listener is not around
                    if (!_currentlyEnabaledBuildingUIDs_List.Contains(bridgePartsSOsToEnable._bridgePartUID))
                        fn_SetBridgeObjectToEnabled(bridgePartsSOsToEnable._bridgePartUID);
                }

                // Disable Current Mission Object - Uses both Push Method & a List update for if the event listener is not around
                missionSO.fn_SetEnabledState(false);
                if (_currentlyEnabaledMissionUIDs_List.Contains(missionSO._missionUID))
                    fn_SetMissionObjectToDisabled(missionSO._missionUID);
            }      
        }
    }
    
    // - Object Enable / Disable Functions -
    #region Mission Objects
    public void fn_SetMissionObjectToDisabled(int uID) {
        _currentlyEnabaledMissionUIDs_List.Remove(uID);
        _OnChange_UpdateOfMissionObjectsActiveList?.Invoke();
    }
    public void fn_SetMissionObjectToEnabled(int uID) {
        _currentlyEnabaledMissionUIDs_List.Add(uID);
        _OnChange_UpdateOfMissionObjectsActiveList?.Invoke();
    }
    #endregion
    #region Bridge Objects
    public void fn_SetBridgeObjectToDisabled(int uID) {
        _currentlyEnabaledBuildingUIDs_List.Remove(uID);
        _OnChange_UpdateOfBridgeObjectsActiveList?.Invoke();
    }

    public void fn_SetBridgeObjectToEnabled(int uID) {
        _currentlyEnabaledBuildingUIDs_List.Add(uID);
        _OnChange_UpdateOfBridgeObjectsActiveList?.Invoke();
    }
    #endregion

    
    public Missions_Basic_SO fn_GetMissionSO(int missionUID)
    {
        foreach (var missionSO in _AllMissionSORefArray)
        {
            if (missionSO._missionUID == missionUID)
            {
                return missionSO;
            }
        }
        Debug.LogError("GameProgressionInteractableObjects_PersistentSingletonMng: fn_GetMissionSO; Error - missionID could not be found!");
        return null;
    }

    // - Special Case Functions -
    public void fn_AddMissionObjectToEnableAtStartOfGame(int uID)
    {
        if (!_isSetUpComplete)
            fn_SetMissionObjectToEnabled(uID);
    }

    #region GET: Check if Objects are currently in enabled List
    // NOTE: The 2 func below should be refactored 
    public bool fn_Get_IsMissionGOEnabled(int id) {
        if (_currentlyEnabaledMissionUIDs_List != null && id != null) {
            return _currentlyEnabaledMissionUIDs_List.Contains(id);
        }
        else {
            Debug.LogError("GameProgressionInteractableObjects_PersistentSingletonMng; fn_Get_IsMissionGOEnabled - Error!");
            return false;
        }
    }
    public bool fn_Get_IsBuildingGOEnabled(int id) {
        if (_currentlyEnabaledBuildingUIDs_List != null && id != null) {
            return _currentlyEnabaledBuildingUIDs_List.Contains(id);
        }
        else {
            Debug.LogError("GameProgressionInteractableObjects_PersistentSingletonMng; fn_Get_IsBuildingGOEnabled - Error!");
            return false;
        }
    }
    #endregion

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
