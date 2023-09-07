using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DataTransfer_Mng : MonoBehaviour
{
    public StaticData _StaticData;
    public TeamMemberTransfer_Data _Prefab;
    public TeanMember_SelectionGroupHolder_Mng _TeamMemberSelectionGroupHolder_Mng;

    public void fn_LoadMissionTeam()
    {
        connectToSelectionGroupHolder();
        if (_TeamMemberSelectionGroupHolder_Mng != null)
        {
            foreach (var item in _TeamMemberSelectionGroupHolder_Mng._selectedMissionTeam._teamMembersGroup.Values)
            {
                // Instantiate New Team Member GameObject
                 GameObject teamMember = InstantiateTeamMemberTransfereHolder(item._uID, item._classType, item._maxEnergy, item._currentEnergy);
                // Add to StaticData List
                //_StaticData.team.Add(teamMember);
            }
        }
    }

    private void connectToSelectionGroupHolder()
    {
        _TeamMemberSelectionGroupHolder_Mng ??= TeanMember_SelectionGroupHolder_Mng.Instance;

    }

    private GameObject InstantiateTeamMemberTransfereHolder(int _uID, TeamMemberClassType _classType, float _maxEnergy, float _currentEnergy)
    {
        TeamMemberTransfer_Data newTeamMemberTransfereData = Instantiate(_Prefab, this.transform);
        newTeamMemberTransfereData._uID = _uID;
        newTeamMemberTransfereData._classType = _classType;
        newTeamMemberTransfereData._maxEnergy = _maxEnergy;
        newTeamMemberTransfereData._currentEnergy = _currentEnergy;
        return newTeamMemberTransfereData.gameObject;
    }


}
