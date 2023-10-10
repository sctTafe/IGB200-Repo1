using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
///
/// Handles The Mission Team Selection; UI & Data
/// 
/// </summary>
public class MissionGroupSelction_Mng : MonoBehaviour
{
    [SerializeField] private bool isDebuggingOn = false;
    
    [SerializeField] private GameObject _TeamMemberMissionSelection_root;
    [SerializeField] private TeamMember_UI_ElementsMng _selectedTeam_UIEMng;
    [SerializeField] private TeamMember_UI_ElementsMng _avaliblePool_UIEMng;
    [SerializeField] private TeamMember_UI_ElementsMng _SelectedBigInfo_UIEMng;

    // local TeamMember_SelectionGroup_Data, created and updated in this script, for the BigInfo UI Element
    private TeamMember_SelectionGroup_Data _currentBigInfo_groupData = new();
    // cached refs
    private TeamMember_SelectionGroup_Data _availableTeamMemberPool;
    private TeamMember_SelectionGroup_Data _selectedMissionTeam;

    // external dependency
    private TeanMember_SelectionGroupHolder_PersistentSingletonMng TeamMemberGroupsMng;

    #region Unity Native Functions
    //private void Awake() {
    //    TeamMemberGroupsMng ??= TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;
    //}
    void Start() {
        // Disable Mission UI Panel Root
        _TeamMemberMissionSelection_root.SetActive(false);
    }

    private void OnDestroy() {
        _avaliblePool_UIEMng._OnTeamMemberClicked -= Handle_TeamMemberSection;
        _selectedTeam_UIEMng._OnTeamMemberClicked -= Handle_TeamMemberSection;
        _avaliblePool_UIEMng._OnPrimaryActionBtn -= Handle_AddToSelectedTeam;
        _selectedTeam_UIEMng._OnPrimaryActionBtn -= Handle_AddToSelectedTeam;
    }
    #endregion


    // - Primary Trigger Function -
    #region Primary Trigger Functions
    public void fn_OpenTeamMemberSelectionWindow() {
        BindToTeamMemberElementMngs();
        ClearMissionTeam();
        _TeamMemberMissionSelection_root.SetActive(true);
    }
    private void BindToTeamMemberElementMngs() {
        TeamMemberGroupsMng ??= TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;

        _avaliblePool_UIEMng.fn_Bind(TeamMemberGroupsMng._avalibleTeamMemberPool);
        _selectedTeam_UIEMng.fn_Bind(TeamMemberGroupsMng._selectedMissionTeam);
        _SelectedBigInfo_UIEMng.fn_Bind(_currentBigInfo_groupData);

        // Bind the two pool display elements to the event
        _avaliblePool_UIEMng._OnTeamMemberClicked += Handle_TeamMemberSection;
        _selectedTeam_UIEMng._OnTeamMemberClicked += Handle_TeamMemberSection;

        // Bind the Primary Action Btn's Events
        _avaliblePool_UIEMng._OnPrimaryActionBtn += Handle_AddToSelectedTeam;
        _selectedTeam_UIEMng._OnPrimaryActionBtn += Handle_AddToSelectedTeam;
    }
    private void ClearMissionTeam()
    {
        if (TeamMemberGroupsMng._selectedMissionTeam._teamMembersGroup.Count > 0)
        {
            Stack<TeamMember_Data> _toClearStack = new();
            foreach (var teamMembers in TeamMemberGroupsMng._selectedMissionTeam._teamMembersGroup.Values) {
                _toClearStack.Push(teamMembers);
            }

            int count = _toClearStack.Count;
            for (int i = 0; i < count; i++)
            {
                TeamMember_Data teamMember = _toClearStack.Pop();
                TeamMemberGroupsMng._avalibleTeamMemberPool.fn_TryAddTeamMember(teamMember);
                TeamMemberGroupsMng._selectedMissionTeam.fn_TryRemoveTeamMember(teamMember);
            }
        }
    }
    #endregion



    private void Handle_TeamMemberSection(TeamMember_Data tMD)
    {
        _currentBigInfo_groupData._teamMembersGroup.Clear();
        _currentBigInfo_groupData._teamMembersGroup.TryAdd(tMD._uID, tMD);
        _currentBigInfo_groupData.fn_Call_OnChange();
    }

    private void Handle_AddToSelectedTeam(TeamMember_Data tMD, TeamMember_SelectionGroup_Data tMSGD)
    {
        bool itworked = false;
        //Request is to add to selected pool
        if (tMSGD._groupType == SelectionGroupType.Available) //check if current teamMember Group is part of the 'Avalible Pool' i.e. trying to add it to the 'Mission pool' group
        {
            if (TeamMemberGroupsMng._selectedMissionTeam.fn_TryAddTeamMember(tMD)) // try add to group, if i can be added to the group, remove it from the 'Avalible Pool'
            {
                itworked = TeamMemberGroupsMng._avalibleTeamMemberPool.fn_TryRemoveTeamMember(tMD);
            }           
        }
        if (tMSGD._groupType == SelectionGroupType.Mission) {

            if (TeamMemberGroupsMng._avalibleTeamMemberPool.fn_TryAddTeamMember(tMD))
            {
                itworked = TeamMemberGroupsMng._selectedMissionTeam.fn_TryRemoveTeamMember(tMD);
            }         
        }
        if(isDebuggingOn) Debug.Log("MissionGroupSelection_Mng: Handle_AddToSelectedTeam: Worked: " + itworked + ", request sent by: " + tMD._name);
    }
}
