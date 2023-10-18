using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// DOSE: Manualy Loads Player Defined Team Members into Selection Groups 
/// 
/// </summary>
public class TeamMember_SelectionGroup_ManualLoadMng : MonoBehaviour
{
    [SerializeReference] private bool _isManuleLoadingEnabled;

    public TeamMemberAssetsData_SO _TypeIconDataSO;
    public TeamMemberAssetsData_Faces_SO _FaceIconDataSO;

    public TeamMember_Basic_SO[] _StartingTeamMembers;
    public TeamMember_Basic_SO[] _PurchasableTeamMembers;
    

    private TeanMember_SelectionGroupHolder_PersistentSingletonMng _TeanMemberSelectionGroupHolderMng;

    private void Awake()
    {
        _TeanMemberSelectionGroupHolderMng = TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance;
        _TeanMemberSelectionGroupHolderMng._OnSetupComplete += HandleResponseToSetUpComplete;
    }
    private void OnDestroy()
    {
        _TeanMemberSelectionGroupHolderMng._OnSetupComplete -= HandleResponseToSetUpComplete;
    }


    // Event Based, so only loads in the team members after other scrips are good to go!
    private void HandleResponseToSetUpComplete()
    {
        if (_isManuleLoadingEnabled)
        {
            CreateTeamMembersDataAndLoad();
            CreateTeamMembersDataAndLoad_PurchasableTeam();
        }
        
    }
    private void CreateTeamMembersDataAndLoad()
    {
        if (_StartingTeamMembers != null && _StartingTeamMembers.Length > 0)
        {
            foreach (var teamMemberBasicSO in _StartingTeamMembers)
            {
                _TeanMemberSelectionGroupHolderMng.fn_LoadingInto_AvailableTeamMemberPool(new TeamMember_Data { 
                    _name = teamMemberBasicSO._name,
                    _nameAndJob = teamMemberBasicSO._nameAndJob,
                    _profileSprite = _FaceIconDataSO.fn_GetProfilePicSprite(teamMemberBasicSO._profilePicGroupOverride,teamMemberBasicSO._hairTypeOverride),
                    _classType = teamMemberBasicSO._teamMemberClass,
                    _toolSprite = _TypeIconDataSO.fn_GetIconSprite(teamMemberBasicSO._teamMemberClass),
                    _bio = teamMemberBasicSO._bio,
                });
            }
        }
    }
    private void CreateTeamMembersDataAndLoad_PurchasableTeam() {
        if (_PurchasableTeamMembers != null && _PurchasableTeamMembers.Length > 0) {
            foreach (var teamMemberBasicSO in _PurchasableTeamMembers) {
                _TeanMemberSelectionGroupHolderMng.fn_LoadingInto_PurchasableTeamMemberPool(new TeamMember_Data {
                    _name = teamMemberBasicSO._name,
                    _nameAndJob = teamMemberBasicSO._nameAndJob,
                    _profileSprite = _FaceIconDataSO.fn_GetProfilePicSprite(teamMemberBasicSO._profilePicGroupOverride, teamMemberBasicSO._hairTypeOverride),
                    _toolSprite = _TypeIconDataSO.fn_GetIconSprite(teamMemberBasicSO._teamMemberClass),
                    _bio = teamMemberBasicSO._bio,
                    _classType = teamMemberBasicSO._teamMemberClass
                });
            }
        }
    }

}
