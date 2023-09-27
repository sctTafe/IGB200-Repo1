using UnityEngine;

/// <summary>
/// 
/// DOSE: 
///     Class for transfering team Member data to battle scene.
///     When 'fn_LoadMissionTeam' is called, TeanMember_SelectionGroupHolder_Mng is contacted, 
///     '_selectedMissionTeam' & its members are retrived, then a holder class is instanciated and added to the  
/// DEPENDENCY: 
///     Calls on 'TeanMember_SelectionGroupHolder_Mng' to get Mission Team
///     Instantiates holder classes, then addes it to the 'team' list in 'StaticData' Class
/// NOTE:
/// 
/// </summary>

public class DataTransfer_PersistentSingletonMng : MonoBehaviour
{

    #region Singelton Setup
    private static DataTransfer_PersistentSingletonMng _instance;
    public static DataTransfer_PersistentSingletonMng Instance
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


    private bool _isDebuggingOn = true;
    public TeamMemberTransfer_Data _Prefab;
    private TeanMember_SelectionGroupHolder_PersistentSingletonMng _TeamMemberSelectionGroupHolder_Mng;


    void Awake()
    {
        SingeltonSetup();
    }


    #region Public Functions
    // - Passing Data In -
    public void fn_LoadMissionTeam()
    {
        if (_isDebuggingOn) { Debug.Log("DataTransfer_Mng: fn_LoadMissionTeam Called, "); }

        connectToSelectionGroupHolder();
        if (_TeamMemberSelectionGroupHolder_Mng != null)
        {
            foreach (var item in _TeamMemberSelectionGroupHolder_Mng._selectedMissionTeam._teamMembersGroup.Values)
            {
                // Instantiate New Team Member GameObject
                 GameObject teamMember = InstantiateTeamMemberTransfereHolder(item._uID, item._classType, item._maxEnergy, item._currentEnergy);
                
                // Add to StaticData List
                BattleTransferData_PersistentSingleton.missionTeam.Add(teamMember);
            }
        }
    }

    // - Pass in the Data From the Mission - 
    public void fn_LoadIn_MissionData(int missionID)
    {
        BattleTransferData_PersistentSingleton.Instance._currentMissionID = missionID;
    }



    // - Returning Data Back - 

    /// <summary>
    ///  Called on Completion of the Battle, pulls data from 'BattleTransferData_PersistentSingleton' to update outcomes
    /// </summary>
    public void fn_HandleMissionFinished()
    {
        if (_isDebuggingOn) Debug.Log("DataTransfer_PersistentSingletonMng: fn_HandleMissionFinished - Called");
        // Update Mission Outcomes
        // 1) retrived mission ID from staticData
        BattleTransferData_PersistentSingleton battleTransferData = BattleTransferData_PersistentSingleton.Instance;
        if (battleTransferData != null)
        {
            if (battleTransferData._isMissionCompletedSuccessfully)
            {
                if (_isDebuggingOn) Debug.Log("DataTransfer_PersistentSingletonMng: fn_HandleMissionFinished - Mission Was Successful");
                // 2) with the ID get what ID will be enabled
                GameProgressionInteractableObjects_PersistentSingletonMng.Instance.fn_CompleteMission(battleTransferData._currentMissionID);
            }
        }
        

        // Update Project Points Outcomes


    }




    // - 
    #endregion

    #region Private Functions
    private GameObject InstantiateTeamMemberTransfereHolder(int _uID, TeamMemberClassType _classType, float _maxEnergy, float _currentEnergy)
    {
        TeamMemberTransfer_Data newTeamMemberTransfereData = Instantiate(_Prefab, this.transform);
        newTeamMemberTransfereData._uID = _uID;
        newTeamMemberTransfereData._classType = _classType;
        newTeamMemberTransfereData._maxEnergy = _maxEnergy;
        newTeamMemberTransfereData._currentEnergy = _currentEnergy;
        return newTeamMemberTransfereData.gameObject;
    }
    private void connectToSelectionGroupHolder()
    {
        _TeamMemberSelectionGroupHolder_Mng ??= TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;

    }
    #endregion



}
