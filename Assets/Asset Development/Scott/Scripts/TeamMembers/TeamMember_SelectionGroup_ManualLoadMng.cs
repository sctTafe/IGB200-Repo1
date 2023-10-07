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
                    _profileSprite = teamMemberBasicSO._profilePic,
                    _toolSprite = teamMemberBasicSO._toolPic,
                    _bio = teamMemberBasicSO._bio,
                    _classType = teamMemberBasicSO._teamMemberClass
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
                    _profileSprite = teamMemberBasicSO._profilePic,
                    _toolSprite = teamMemberBasicSO._toolPic,
                    _bio = teamMemberBasicSO._bio,
                    _classType = teamMemberBasicSO._teamMemberClass
                });
            }
        }
    }

}
