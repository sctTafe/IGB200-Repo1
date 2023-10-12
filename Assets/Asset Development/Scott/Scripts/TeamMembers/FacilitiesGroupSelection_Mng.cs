using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilitiesGroupSelection_Mng : MonoBehaviour
{
    [SerializeField] private bool isDebuggingOn = false;

    [SerializeField] private GameObject _FacilitiesPannel_root;
    [SerializeField] private TeamMember_UI_ElementsMng _facilities_UIEMng;
    [SerializeField] private TeamMember_UI_ElementsMng _avalible_UIEMng;
    [SerializeField] private TeamMember_UI_ElementsMng _bigInfo_UIEMng;

    // local TeamMember_SelectionGroup_Data, created and updated in this script, for the BigInfo UI Element
    private TeamMember_SelectionGroup_Data _currentBigInfo_groupData = new();
    // cached refs
    private TeamMember_SelectionGroup_Data _availableTeamMemberPool;
    private TeamMember_SelectionGroup_Data _selectedMissionTeam;

    // external dependency
    private TeanMember_SelectionGroupHolder_PersistentSingletonMng TeamMemberGroupsMng;

    #region Unity Native Functions

    void Start() {
        // Disable Mission UI Panel Root
        _FacilitiesPannel_root.SetActive(false);
    }

    private void OnDestroy() {
        _avalible_UIEMng._OnTeamMemberClicked -= Handle_TeamMemberSection;
        _facilities_UIEMng._OnTeamMemberClicked -= Handle_TeamMemberSection;
        _avalible_UIEMng._OnPrimaryActionBtn -= Handle_AddRemove_ToFacilitesTeemPool;
        _facilities_UIEMng._OnPrimaryActionBtn -= Handle_AddRemove_ToFacilitesTeemPool;
    }
    #endregion


    // - Primary Trigger Function -
    #region Primary Trigger Functions
    public void fn_OpenTeamMemberSelectionWindow() {
        BindToTeamMemberElementMngs();
        //fn_ClearFacilitiesTeamMemberPool();
        _FacilitiesPannel_root.SetActive(true);
    }
    private void BindToTeamMemberElementMngs() {
        if (TeamMemberGroupsMng == null)
            TeamMemberGroupsMng = TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;


        _avalible_UIEMng.fn_Bind(TeamMemberGroupsMng._avalibleTeamMemberPool);
        _facilities_UIEMng.fn_Bind(TeamMemberGroupsMng._facilitiesTeamMemberPool);

        _bigInfo_UIEMng.fn_Bind(_currentBigInfo_groupData);

        // Bind the two pool display elements to the event
        _avalible_UIEMng._OnTeamMemberClicked += Handle_TeamMemberSection;
        _facilities_UIEMng._OnTeamMemberClicked += Handle_TeamMemberSection;

        // Bind the Primary Action Btn's Events
        _avalible_UIEMng._OnPrimaryActionBtn += Handle_AddRemove_ToFacilitesTeemPool;
        _facilities_UIEMng._OnPrimaryActionBtn += Handle_AddRemove_ToFacilitesTeemPool;
    }
    #endregion

    public void fn_ClearFacilitiesTeamMemberPool() {
        if (TeamMemberGroupsMng._facilitiesTeamMemberPool._teamMembersGroup.Count > 0) {
            Stack<TeamMember_Data> _toClearStack = new();
            foreach (var teamMembers in TeamMemberGroupsMng._facilitiesTeamMemberPool._teamMembersGroup.Values) {
                _toClearStack.Push(teamMembers);
            }

            int count = _toClearStack.Count;
            for (int i = 0; i < count; i++) {
                TeamMember_Data teamMember = _toClearStack.Pop();
                TeamMemberGroupsMng._avalibleTeamMemberPool.fn_TryAddTeamMember(teamMember);
                TeamMemberGroupsMng._facilitiesTeamMemberPool.fn_TryRemoveTeamMember(teamMember);
            }
        }
    }
    
    private void Handle_TeamMemberSection(TeamMember_Data tMD) {
        _currentBigInfo_groupData._teamMembersGroup.Clear();
        _currentBigInfo_groupData._teamMembersGroup.TryAdd(tMD._uID, tMD);
        _currentBigInfo_groupData.fn_Call_OnChange();
    }

    private void Handle_AddRemove_ToFacilitesTeemPool(TeamMember_Data tMD, TeamMember_SelectionGroup_Data tMSGD) {
        bool itworked = false;
        //Request is to add to selected pool
        if (tMSGD._groupType == SelectionGroupType.Available) //check if current teamMember Group is part of the 'Avalible Pool' i.e. trying to add it to the 'Mission pool' group
        {
            if (TeamMemberGroupsMng._facilitiesTeamMemberPool.fn_TryAddTeamMember(tMD)) // try add to group, if i can be added to the group, remove it from the 'Avalible Pool'
            {
                itworked = TeamMemberGroupsMng._avalibleTeamMemberPool.fn_TryRemoveTeamMember(tMD);
            }
        }
        if (tMSGD._groupType == SelectionGroupType.Resting) {

            if (TeamMemberGroupsMng._avalibleTeamMemberPool.fn_TryAddTeamMember(tMD)) {
                itworked = TeamMemberGroupsMng._facilitiesTeamMemberPool.fn_TryRemoveTeamMember(tMD);
            }
        }
        if (isDebuggingOn) Debug.Log("MissionGroupSelection_Mng: Handle_AddRemove_To_FacilitiesPanel_rootTeemPool: Worked: " + itworked + ", request sent by: " + tMD._name);
    }

}
