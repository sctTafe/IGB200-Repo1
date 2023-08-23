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

    //public List<TeamMember_Data> _teamMembersGroup = new List<TeamMember_Data>();
    public Dictionary<int, TeamMember_Data> _teamMembersGroup = new Dictionary<int, TeamMember_Data>();

    public TeamMember_SelectionGroup_Data(SelectionGroupType type = SelectionGroupType.Other)
    {
        _groupType = type;
    }

    public void fn_Call_OnChange()
    {
        OnChange?.Invoke();
    }

    public bool fn_TryAddTeamMember(TeamMember_Data teamMember)
    {
        bool itWorked;
        itWorked = _teamMembersGroup.TryAdd(teamMember._uID, teamMember);
        if (itWorked)
        {
            OnChange?.Invoke();
            return true;
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

public enum SelectionGroupType
{
    error,
    Available,
    Mission,
    Purchase,
    Other
}