using System;
using UnityEditor;
using UnityEngine;

public class GameProgressionInteractableObjects_Mission: MonoBehaviour
{
    private bool isDebuggingOn = true;

    // Mission Data Holder - Set In Editor 
    [SerializeField] public MissionDataHolder _localMissionDataHolder;
    private GameProgressionInteractableObjects_PersistentSingletonMng _GameProgressionObjectsMng;
    private GameObject _thisGameObject;
    private int _uID;
    

    #region Unity Native Functions
    private void Awake()
    {
        _thisGameObject = this.gameObject;
        
    }

    private void Start()
    {
        _thisGameObject.SetActive(false);

        Setup_MissionDataHolder();

        Setup_ConnectToProgressionInteractableObjectsMng();

        Setup_CheckIfMissionIsEnabledFromStart();

        // check to see if the object is already on the enabled list
        CheckEnableStatusOfObject();     
    }
    private void OnDestroy() {
        if (_GameProgressionObjectsMng != null)
            _GameProgressionObjectsMng._OnChange_UpdateOfMissionObjectsActiveList -= HandleOnUpdateOfActiveListChange;

        _localMissionDataHolder._MissionSO._OnStateChange_isEnabled -= Handle_MissionsBasicSO_OnStateChangeIsEnabled;
    }
    #endregion

    public int fn_GetUID()
    {
        return _uID;
    }

    #region Private Functions
    private void Setup_MissionDataHolder()
    {
        // Retrive uID;
        _uID = _localMissionDataHolder.fn_GetMissionUID();
        if (isDebuggingOn) Debug.Log("GameProgressionInteractableObjects_Mission: UID Value: " + _uID);

        // Subscribe to '_OnStateChange_isEnabled' Event
        _localMissionDataHolder._MissionSO._OnStateChange_isEnabled += Handle_MissionsBasicSO_OnStateChangeIsEnabled;
    }
    private void Setup_CheckIfMissionIsEnabledFromStart()
    {
        // check if bool is true in '_MissionSO', if true try enable in Mng
        if (_localMissionDataHolder.fn_GetIsMissionEnabledFromStart())
        {
            _GameProgressionObjectsMng.fn_AddMissionObjectToEnableAtStartOfGame(_uID);
        }
    }
    private void Setup_ConnectToProgressionInteractableObjectsMng()
    {
        _GameProgressionObjectsMng ??= GameProgressionInteractableObjects_PersistentSingletonMng.Instance;
        if (_GameProgressionObjectsMng != null)
        {
            _GameProgressionObjectsMng._OnChange_UpdateOfMissionObjectsActiveList += HandleOnUpdateOfActiveListChange;
        }
    }
    #endregion

    #region Event Response
    // - _MissionSO Events -
    private void Handle_MissionsBasicSO_OnStateChangeIsEnabled(bool enabled)
    {
        if (_GameProgressionObjectsMng == null)
            Setup_ConnectToProgressionInteractableObjectsMng();

        if (enabled)
            _GameProgressionObjectsMng.fn_SetMissionObjectToEnabled(_uID); 
        else 
            _GameProgressionObjectsMng.fn_SetMissionObjectToDisabled(_uID); 
    }


    // - GameProgressionInteractableObjects_PersistentSingletonMng Events-
    private void HandleOnUpdateOfActiveListChange()
    {
        if (isDebuggingOn) Debug.Log("GameProgressionInteractableObjects_Mission ( "+ _uID + " ) Handle On Update Called");
        CheckEnableStatusOfObject();
    }

    private void CheckEnableStatusOfObject()
    {
        if (_GameProgressionObjectsMng == null)
            Setup_ConnectToProgressionInteractableObjectsMng();


        if (_GameProgressionObjectsMng != null)
        {
            // was being called before this was set in execution order, hence line below added
            _thisGameObject ??= this.gameObject;

            _thisGameObject.SetActive(_GameProgressionObjectsMng.fn_Get_IsMissionGOEnabled(_uID));
        }
        else
        {
            Debug.LogError("GameProgressionInteractableObjects_Mission Error! - could not find Mng");
        } 
    }
    #endregion

}
