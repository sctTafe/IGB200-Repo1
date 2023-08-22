using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// Handles The Mission Team Selection; UI & Data
/// 
/// </summary>
public class MissionGroupSelction_Mng : MonoBehaviour
{

    [SerializeField] private TeanMember_SelectionGroupHolder_Mng groupsHolder;
    [SerializeField] private GameObject _TeamMemberMissionSelection_root;
    [SerializeField] private TeamMember_UI_ElementsMng _selectedTeam_UIEMng;
    [SerializeField] private TeamMember_UI_ElementsMng _avaliblePool_UIEMng;
    [SerializeField] private TeamMember_UI_ElementsMng _SelectedBigInfo_UIEMng;

    // local TeamMember_SelectionGroup_Data, created and updated in this script, for the BigInfo UI Element
    private TeamMember_SelectionGroup_Data _currentBigInfo_groupData = new();
    // cached refs
    private TeamMember_SelectionGroup_Data _availableTeamMemberPool;
    private TeamMember_SelectionGroup_Data _selectedMissionTeam;


    void Start() {
        _TeamMemberMissionSelection_root.SetActive(false);
        _availableTeamMemberPool = groupsHolder._avalibleTeamMemberPool;
        _selectedMissionTeam = groupsHolder._selectedMissionTeam;

    }

    public void fn_OpenTeamMemberSelectionWindow()
    {
        BindToTeamMemberElementMngs();
        _TeamMemberMissionSelection_root.SetActive(true);
        
    }

    private void BindToTeamMemberElementMngs()
    {
        _avaliblePool_UIEMng.fn_Bind(_availableTeamMemberPool);
        _selectedTeam_UIEMng.fn_Bind(_selectedMissionTeam);
        _SelectedBigInfo_UIEMng.fn_Bind(_currentBigInfo_groupData);

        // Bind the two pool display elements to the event
        _avaliblePool_UIEMng._OnTeamMemberClicked += Handle_TeamMemberSection;
        _selectedTeam_UIEMng._OnTeamMemberClicked += Handle_TeamMemberSection;

        // Bind the Primary Action Btn's Events
        _avaliblePool_UIEMng._OnPrimaryActionBtn += Handle_AddToSelectedTeam;
        _selectedTeam_UIEMng._OnPrimaryActionBtn += Handle_AddToSelectedTeam;
    }

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
        if (tMSGD == _availableTeamMemberPool)
        {
            itworked = _availableTeamMemberPool.fn_TryRemoveTeamMember(tMD);
            itworked = _selectedMissionTeam.fn_TryAddTeamMember(tMD);
        }
        if (tMSGD == _selectedMissionTeam)
        {
            itworked = _selectedMissionTeam.fn_TryRemoveTeamMember(tMD);
            itworked = _availableTeamMemberPool.fn_TryAddTeamMember(tMD);
        }
        Debug.Log("MissionGroupSelection_Mng: Handle_AddToSelectedTeam: Worked: " + itworked + ", request sent by: " + tMD._name);

    }


    // Update is called once per frame
    void Update()
    {
        
    }




}
