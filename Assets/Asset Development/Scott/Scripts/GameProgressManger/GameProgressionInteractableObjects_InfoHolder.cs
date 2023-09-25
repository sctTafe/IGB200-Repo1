using System;
using UnityEditor;
using UnityEngine;

public class GameProgressionInteractableObjects_InfoHolder: MonoBehaviour
{
    private bool isDebuggingOn = true;

    // Mission Data Holder - Set In Editor 
    [SerializeField] public MissionDataHolder _localMissionDataHolder;
    
    private GameObject _thisGameObject;
    private int _uID;
    private GameProgressionInteractableObjects_PersistentSingletonMng _GameProgressionObjectsMng;

    #region Unity Native Functions
    private void Awake()
    {
        _thisGameObject = this.gameObject;
        
    }
    private void OnDestroy()
    {
        if (_GameProgressionObjectsMng != null)
            _GameProgressionObjectsMng._OnChange_UpdateOfActiveList -= HandleOnUpdateOfActiveListChange;

        _localMissionDataHolder._MissionSO._OnStateChange_isEnabled -= Handle_MissionsBasicSO_OnStateChangeisEnabled;
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
        if (isDebuggingOn) Debug.Log("GameProgressionInteractableObjects_InfoHolder: UID Value: " + _uID);

        // Subscribe to '_OnStateChange_isEnabled' Event
        _localMissionDataHolder._MissionSO._OnStateChange_isEnabled += Handle_MissionsBasicSO_OnStateChangeisEnabled;
    }
    private void Setup_CheckIfMissionIsEnabledFromStart()
    {
        // check if bool is true in '_MissionSO', if true try enable in Mng
        if (_localMissionDataHolder.fn_GetIsMissionEnabledFromStart())
        {
            _GameProgressionObjectsMng.fn_AddObjectToEnableAtStartOfGame(_uID);
        }
    }
    private void Setup_ConnectToProgressionInteractableObjectsMng()
    {
        _GameProgressionObjectsMng ??= GameProgressionInteractableObjects_PersistentSingletonMng.Instance;
        if (_GameProgressionObjectsMng != null)
        {
            _GameProgressionObjectsMng._OnChange_UpdateOfActiveList += HandleOnUpdateOfActiveListChange;
        }
    }
    #endregion

    #region Event Response
    // - _MissionSO Events -
    private void Handle_MissionsBasicSO_OnStateChangeisEnabled(bool enabled)
    {
        if (_GameProgressionObjectsMng == null)
            Setup_ConnectToProgressionInteractableObjectsMng();

        if (enabled)
            _GameProgressionObjectsMng.fn_SetObjectToEnabled(_uID); 
        else 
            _GameProgressionObjectsMng.fn_SetObjectToDisabled(_uID); 
    }


    // - GameProgressionInteractableObjects_PersistentSingletonMng Events-
    private void HandleOnUpdateOfActiveListChange()
    {
        if (isDebuggingOn) Debug.Log("GameProgressionInteractableObjects_InfoHolder ( "+ _uID + " ) Handle On Update Called");
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

            _thisGameObject.SetActive(_GameProgressionObjectsMng.fn_Get_IsGOEnabled(_uID));
        }
        else
        {
            Debug.LogError("GameProgressionInteractableObjects_InfoHolder Error! - could not find Mng");
        } 
    }
    #endregion

}
