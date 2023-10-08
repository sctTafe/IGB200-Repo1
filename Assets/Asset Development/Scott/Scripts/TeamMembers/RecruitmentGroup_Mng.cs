using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
///
/// DOSE: Handles The Team Member Recruitment; UI & Data
/// 
/// </summary>
public class RecruitmentGroup_Mng : MonoBehaviour
{
    [SerializeField] private bool isDebuggingOn = false;

    // - Panel Root -
    [SerializeField] private GameObject _TeamMemberRecruitmentSelection_root;
    // - TeamMember_UI_ElementsMng -
    [SerializeField] private TeamMember_UI_ElementsMng _recruits_UIEMng;
    [SerializeField] private TeamMember_UI_ElementsMng _selectedBigInfo_UIEMng;

    // - TeamMember_SelectionGroup_Data -
    // local TeamMember_SelectionGroup_Data, created and updated in this script, for the BigInfo UI Element
    private TeamMember_SelectionGroup_Data _currentBigInfo_groupData = new();
    // cached refs
    private TeamMember_SelectionGroup_Data _availableTeamMemberPool;
    private TeamMember_SelectionGroup_Data _purchasableTeamMemberPool;

    // external dependency
    private TeanMember_SelectionGroupHolder_PersistentSingletonMng TeamMemberGroupsMng;

    #region Unity Native Functions
    void Start() {
        // Disable Mission UI Panel Root
        _TeamMemberRecruitmentSelection_root.SetActive(false);
    }
    private void OnDestroy() {
        _recruits_UIEMng._OnTeamMemberClicked -= Handle_TeamMemberSection;
        _recruits_UIEMng._OnPrimaryActionBtn -= Handle_AddToSelectedTeam;

    }
    #endregion

    // - Primary Trigger Function -
    public void fn_OpenTeamMemberSelectionWindow() {
        BindToTeamMemberElementMngs();
        _TeamMemberRecruitmentSelection_root.SetActive(true);
    }

    private void BindToTeamMemberElementMngs() {

        TeamMemberGroupsMng ??= TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;

        // - Bind Data -
        _recruits_UIEMng.fn_Bind(TeamMemberGroupsMng._purchasableTeamMemberPool);
        _selectedBigInfo_UIEMng.fn_Bind(_currentBigInfo_groupData);
        // - Subscribe to Events -
        _recruits_UIEMng._OnTeamMemberClicked += Handle_TeamMemberSection;          // Display Member Event Trigger
        _selectedBigInfo_UIEMng._OnPrimaryActionBtn += Handle_AddToSelectedTeam;    // Primary Action Event Trigger (Hire)
    }



    #region Event Handeling
    private void Handle_TeamMemberSection(TeamMember_Data tMD) {
        _currentBigInfo_groupData._teamMembersGroup.Clear();
        _currentBigInfo_groupData._teamMembersGroup.TryAdd(tMD._uID, tMD);
        _currentBigInfo_groupData.fn_Call_OnChange();
    }

    // Needs Editing
    private void Handle_AddToSelectedTeam(TeamMember_Data tMD, TeamMember_SelectionGroup_Data tMSGD) {
        bool itworked = false;

        // - Try Purchase Amount - Project Points - PP cost will stored in the Team Member Data
        int purchaseCostTemp = 40;
        if (ProjectPoints_PersistentSingletonMng.Instance.fn_TrySubtractPoints(purchaseCostTemp))
        {
            if (TeamMemberGroupsMng._avalibleTeamMemberPool
                .fn_TryAddTeamMember(
                    tMD)) // try add to group, if i can be added to the group, remove it from the 'Available Pool'
            {
                itworked = TeamMemberGroupsMng._purchasableTeamMemberPool.fn_TryRemoveTeamMember(tMD);
            }
            _recruits_UIEMng.fn_DisplayNextTeamMemberInBigInfo(this);
        }
        if (isDebuggingOn) Debug.Log("MissionGroupSelection_Mng: Handle_AddToSelectedTeam: Worked: " + itworked + ", request sent by: " + tMD._name);
    }
    #endregion


    public void fn_ClearBigInfo()
    {
        _selectedBigInfo_UIEMng.fn_Bind(null);
    }

}
