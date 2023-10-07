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
///     This all should be refactored!
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

    public bool _isUsingStaticDataModeOn = true;
    public TeamMemberTransfer_Data _Prefab;

    private bool _isDebuggingOn = true;
    private TeanMember_SelectionGroupHolder_PersistentSingletonMng _TeamMemberGroupsHolderMng;


    void Awake()
    {
        SingeltonSetup();
    }


    #region Public Functions
    #region - Pass Data To Missiom - 

    #region fn_LoadMissionTeam 
    /// <summary>
    /// 'fn_LoadMissionTeam' - Instantiates the team member game objects and populates their data sets
    /// </summary>
    public void fn_LoadMissionTeam()
    {
        if (_isDebuggingOn) { Debug.Log("DataTransfer_Mng: fn_LoadMissionTeam - Called"); }
        ConnectToSelectionGroupHolder();

        if (_isUsingStaticDataModeOn)
        {
            StaticData.isBattleGameManagerInTestModeOverride = true;
            foreach (var item in _TeamMemberGroupsHolderMng._selectedMissionTeam._teamMembersGroup.Values) 
            {
                TeamMemberTransfer_Data tMTD = Instantiate(_Prefab, this.transform);
                tMTD._uID = item._uID;
                tMTD._classType = item._classType;
                tMTD._maxEnergy = item._maxEnergy;
                tMTD._maxMorale = item._maxMorale;
                tMTD._currentEnergy = item._currentEnergy;
                tMTD._currentMorale = item._currentMorale;

                StaticData.team.Add(tMTD);
            }
        }
        else
        {
            if (_TeamMemberGroupsHolderMng != null) {
                foreach (var item in _TeamMemberGroupsHolderMng._selectedMissionTeam._teamMembersGroup.Values) {
                    TeamMemberTransfer_Data tMTD = Instantiate(_Prefab, this.transform);
                    tMTD._uID = item._uID;
                    tMTD._classType = item._classType;
                    tMTD._maxEnergy = item._maxEnergy;
                    tMTD._maxMorale = item._maxMorale;
                    tMTD._currentEnergy = item._currentEnergy;
                    tMTD._currentMorale = item._currentMorale;

                    // Add to List
                    BattleTransferData_PersistentSingleton._missionTeam_List.Add(tMTD.gameObject);
                }
            }
        }


    }
    #endregion


    // Set the Mission ID value in 'BattleTransferData_PersistentSingleton'
    public void fn_LoadIn_MissionData(int missionID)
    {
        BattleTransferData_PersistentSingleton.Instance._currentMissionID = missionID;
        StaticData.currentMissionID = missionID;
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
        ReintegrateTeamMemberData();
        UpdateProjectPoints();
    }

    private void UpdateMissionObjects()
    {

        if (_isUsingStaticDataModeOn)
        {
            if (StaticData.isBattleWon)
            {
                // - Update Missions & Buildings -
                if (_isDebuggingOn) Debug.Log("DataTransfer_PersistentSingletonMng: fn_HandleMissionFinished - Mission Was Successful");
                GameProgressionInteractableObjects_PersistentSingletonMng.Instance.fn_CompleteMission(
                    StaticData.currentMissionID);
            }

        }
        else
        {
            // Update Mission Outcomes
            // 1) retrieve mission ID from staticData
            BattleTransferData_PersistentSingleton battleTransferData = BattleTransferData_PersistentSingleton.Instance;
            if (battleTransferData != null) {
                if (battleTransferData._isMissionCompletedSuccessfully) {
                    // - Update Missions & Buildings -
                    if (_isDebuggingOn) Debug.Log("DataTransfer_PersistentSingletonMng: fn_HandleMissionFinished - Mission Was Successful");
                    GameProgressionInteractableObjects_PersistentSingletonMng.Instance.fn_CompleteMission(
                        battleTransferData._currentMissionID);
                }
            }
        }

    }
    private void ReintegrateTeamMemberData()
    {
        TeamMember_SelectionGroup_Data groupData = _TeamMemberGroupsHolderMng._selectedMissionTeam;

        if (_isUsingStaticDataModeOn)
        {
            foreach (var teamMemberTransData in StaticData.team)
            {
                TeamMemberTransfer_Data transferData = teamMemberTransData.GetComponent<TeamMemberTransfer_Data>();
                // Find and match team member based on ID, then pass data back 
                groupData._teamMembersGroup.TryGetValue(transferData._uID, out TeamMember_Data teamMemberData);
                teamMemberData._currentMorale = transferData._currentMorale;
                teamMemberData._currentEnergy = transferData._currentEnergy;
                // Destroy Holder
                Destroy(teamMemberTransData.gameObject);
            }
            StaticData.team.Clear();
        }
        else
        {
            
            foreach (var tempTeamMemberGO in BattleTransferData_PersistentSingleton._missionTeam_List)
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
            BattleTransferData_PersistentSingleton._missionTeam_List.Clear();
        }
    }

    private void UpdateProjectPoints()
    {
        int missionPointsGained = 0;

        if (_isUsingStaticDataModeOn)
        {
            // Bonus Points From Mission 
            missionPointsGained += StaticData.additionalProjectPointsEarned;

            // Points From Completing Mission
            if (StaticData.isBattleWon)
            {
                // Note: gets the related missionSO, then get the Bonus points for completing the mission from it
                missionPointsGained += GameProgressionInteractableObjects_PersistentSingletonMng.Instance.fn_GetMissionSO(
                    StaticData.currentMissionID)._completionBonusPoints;
            }
        }
        else
        {
            // Bonus Points From Mission 
            BattleTransferData_PersistentSingleton battleTransferData = BattleTransferData_PersistentSingleton.Instance;
            missionPointsGained = battleTransferData._earnedBonuseProjectPoints;

            // Points From Completing Mission
            if (battleTransferData._isMissionCompletedSuccessfully) {
                Missions_Basic_SO missionSO = GameProgressionInteractableObjects_PersistentSingletonMng.Instance.fn_GetMissionSO(
                    battleTransferData._currentMissionID);
                missionPointsGained += missionSO._completionBonusPoints;
            }
        }

        // Add the Points to the 'ProjectPointsMng'
        ProjectPoints_PersistentSingletonMng.Instance.fn_AddPoints(missionPointsGained);
    }

    #endregion END: - Pull Data From Missiom - 
    #endregion

    #region Private Functions

    private void ConnectToSelectionGroupHolder()
    {
        _TeamMemberGroupsHolderMng ??= TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;

    }
    #endregion



}
