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
    public TeamMember_Basic_SO[] _AvalibleTeamMembers;

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



    private void HandleResponseToSetUpComplete()
    {
        if (_isManuleLoadingEnabled)
        {
            CreateTeamMembereDataAndLoad();
        }
        
    }
    private void CreateTeamMembereDataAndLoad()
    {
        if (_AvalibleTeamMembers != null && _AvalibleTeamMembers.Length > 0)
        {
            foreach (var teamMemberBasicSO in _AvalibleTeamMembers)
            {
                _TeanMemberSelectionGroupHolderMng.fn_LoadingIntoAvalibleSelectionGroup(new TeamMember_Data { 
                    _name = teamMemberBasicSO._name,
                    _profileSprite = teamMemberBasicSO._profilePic,
                    _toolSprite = teamMemberBasicSO._toolPic,
                    _bio = teamMemberBasicSO._bio,
                    _classType = teamMemberBasicSO._teamMemberClass
                });
            }
        }
    } 

}
