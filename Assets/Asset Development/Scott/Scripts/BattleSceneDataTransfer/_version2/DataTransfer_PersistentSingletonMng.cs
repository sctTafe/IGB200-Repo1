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
    private TeanMember_SelectionGroupHolder_PersistentSingletonMng _TeamMemberGroupsHolderMng;


    void Awake()
    {
        SingeltonSetup();
    }


    #region Public Functions
    #region - Pass Data To Missiom - 
    // - Passing Data In -
    public void fn_LoadMissionTeam()
    {
        if (_isDebuggingOn) { Debug.Log("DataTransfer_Mng: fn_LoadMissionTeam - Called"); }

        connectToSelectionGroupHolder();
        if (_TeamMemberGroupsHolderMng != null)
        {
            foreach (var item in _TeamMemberGroupsHolderMng._selectedMissionTeam._teamMembersGroup.Values)
            {
                // Instantiate New Team Member GameObject
                 GameObject teamMember = InstantiateTeamMemberTransfereHolder(item._uID, item._classType, item._maxEnergy, item._currentEnergy);
                
                // Add to StaticData List
                BattleTransferData_PersistentSingleton.missionTeam.Add(teamMember);
            }
        }
    }
    // Set the Mission ID value in 'BattleTransferData_PersistentSingleton'
    public void fn_LoadIn_MissionData(int missionID)
    {
        BattleTransferData_PersistentSingleton.Instance._currentMissionID = missionID;
    }

    #endregion END: - Pass Data To Missiom - 

    #region - Pull Data From Missiom - 

    // - Pass in the Data From the Mission - 

    /// <summary>
    ///  Called on Completion of the Battle, pulls data from 'BattleTransferData_PersistentSingleton' to update outcomes
    /// </summary>
    public void fn_HandleMissionFinished()
    {
        if (_isDebuggingOn) Debug.Log("DataTransfer_PersistentSingletonMng: fn_HandleMissionFinished - Called");
        UpdateMissionObjects();
        ReintegrateTeamMemberDataWith();
    }

    private void UpdateMissionObjects()
    {
        // Update Mission Outcomes
        // 1) retrieve mission ID from staticData
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
    }

    private void ReintegrateTeamMemberDataWith()
    {
        TeamMember_SelectionGroup_Data groupData = _TeamMemberGroupsHolderMng._selectedMissionTeam;
        foreach (var tempTeamMemberGO in BattleTransferData_PersistentSingleton.missionTeam)
        {
            TeamMemberTransfer_Data transferData = tempTeamMemberGO.GetComponent<TeamMemberTransfer_Data>();
            
            // Find and match team member based on ID, then pass data back 
            groupData._teamMembersGroup.TryGetValue(transferData._uID, out TeamMember_Data teamMemberData);

            teamMemberData._currentMorale = transferData._currentMorale;
            teamMemberData._currentEnergy = transferData._currentEnergy;    

            // Destroy Holder
            Destroy(tempTeamMemberGO);
        }
        // Clear the list after all the related Team Member GameObjects have been destroyed
        BattleTransferData_PersistentSingleton.missionTeam.Clear();
    }


    #endregion END: - Pull Data From Missiom - 
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
        _TeamMemberGroupsHolderMng ??= TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;

    }
    #endregion



}
