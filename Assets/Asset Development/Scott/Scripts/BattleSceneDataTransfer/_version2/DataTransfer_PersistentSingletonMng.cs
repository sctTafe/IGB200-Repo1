using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// DOSE: 
///     Class for transfering team Member data to battle scene.
///     When 'LoadMissionTeamData' is called, TeanMember_SelectionGroupHolder_Mng is contacted, 
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
    public string _BattleScene;
    public string _TutorialBattleScene;

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

    #region Start Mission
    public void fn_LoadMissionDataAndTransfer(Missions_Basic_SO missionSO, bool isUsingTutorialScene = false)
    {
        LoadMissionData(missionSO);
        LoadMissionTeamData();

        if (isUsingTutorialScene)
            ChangeToBattleScene(_TutorialBattleScene);
        else
            ChangeToBattleScene(_BattleScene);

    }

    private void LoadMissionTeamData()
    {
        if (_isDebuggingOn) { Debug.Log("DataTransfer_Mng: LoadMissionTeamData - Called"); }
        ConnectToSelectionGroupHolder();

        if (_isUsingStaticDataModeOn)
        {
            StaticData.isBattleGameManagerInTestModeOverride = true;
            foreach (var item in _TeamMemberGroupsHolderMng._selectedMissionTeam._teamMembersGroup.Values) 
            {
                TeamMemberTransfer_Data tMTD = Instantiate(_Prefab, this.transform);
                tMTD._teamMemberName = item._name;
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
                    tMTD._teamMemberName = item._name;
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

    private void LoadMissionData(Missions_Basic_SO missionSO)
    {
        //BattleTransferData_PersistentSingleton.Instance._currentMissionID = missionID; // depreciated, all related functionality needs removing 
        
        StaticData.fn_ClearData();
        // - Override Test Mode
        StaticData.isBattleGameManagerInTestModeOverride = true;
        // - Mission UID -
        StaticData.currentMissionID = missionSO._missionUID;
        // - Mission enemy type - 
        if (missionSO._enemies != null && missionSO._enemies.Length > 0)
            StaticData.enemyType = missionSO._enemies[0];
        // - Mission setting position 
        StaticData.battlePosition = missionSO._battlePosition;
    }

    private void ChangeToBattleScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    #endregion
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
        UpdateDaysPassed();


        ConnectToSelectionGroupHolder();
        _TeamMemberGroupsHolderMng.fn_ClearMissionTeam();
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

    private void UpdateDaysPassed()
    {
        if (_isUsingStaticDataModeOn) {
            int days =   GameProgressionInteractableObjects_PersistentSingletonMng.Instance.fn_GetMissionSO(StaticData.currentMissionID)._daysToComplete;
            DayCounter_PersistentSingletonMng tmp = DayCounter_PersistentSingletonMng.Instance;
            for (int i = 0; i < days; i++)
            {
                tmp.fn_updateToNextDay();
            }
        }
    }

    #endregion END: - Pull Data From Missiom - 
    #endregion

    #region Private Functions

    private void ConnectToSelectionGroupHolder()
    {
        //_TeamMemberGroupsHolderMng ??= TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;
        if (_TeamMemberGroupsHolderMng == null)
            _TeamMemberGroupsHolderMng = TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;

    }
    #endregion



}
