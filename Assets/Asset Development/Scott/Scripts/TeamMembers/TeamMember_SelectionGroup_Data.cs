using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// Holds and group of Team Members
///
/// </summary>
[Serializable]
public class TeamMember_SelectionGroup_Data
{
    public event Action OnChange;
    public SelectionGroupType _groupType;
    public int _maxGroupSize;

    //public List<TeamMember_Data> _teamMembersGroup = new List<TeamMember_Data>();
    public Dictionary<int, TeamMember_Data> _teamMembersGroup = new Dictionary<int, TeamMember_Data>();

    public TeamMember_SelectionGroup_Data(SelectionGroupType type = SelectionGroupType.Other, int maxGroupSize = -1)
    {
        _groupType = type;
        _maxGroupSize = maxGroupSize;
    }

    public int fn_Get_CurrentGroupSize()
    {
        return _teamMembersGroup.Count;
    }
    public int fn_Get_MaxGroupSize()
    {
        return _maxGroupSize;
    }

    public void fn_Call_OnChange()
    {
        OnChange?.Invoke();
    }

    public bool fn_TryAddTeamMember(TeamMember_Data teamMember)
    {
        bool isAddedToDictionary;

        // check to see if the is space to be added to the group, or group size is unlimited (-1)
        if(_maxGroupSize < 0 || fn_Get_CurrentGroupSize() < _maxGroupSize)
        {
            // check to see the ID is not already in the dictionary
            isAddedToDictionary = _teamMembersGroup.TryAdd(teamMember._uID, teamMember);
            if (isAddedToDictionary)
            {
                OnChange?.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    public bool fn_TryRemoveTeamMember(TeamMember_Data teamMember)
    {
        if (_teamMembersGroup.ContainsKey(teamMember._uID))
        {
            _teamMembersGroup.Remove(teamMember._uID);
            OnChange?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }
}

