using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamMemberTypesProfile_SO", menuName = "SO/TeamMemberTypesProfile_SO")]

public class TeamMemberTypesProfile_SO : ScriptableObject {
    [SerializeField] private TeamMemberTypeData[] _teamMemberTypeProfiles;

    public TeamMemberTypeData fn_GetTeamMemberTypeData(TeamMemberTypes typeToGet)
    {
        List<TeamMemberTypeData> _out = new();
        foreach (var typeData in _teamMemberTypeProfiles)
        {
            if (typeData.fn_Get_TeamMemberType() == typeToGet)
                _out.Add(typeData);
        }

        if (_out.Count == 0)
        {
            Debug.LogWarning("Warning! - TeamMemberTypesProfile_SO: could not find TeamMemberTypeData");
            return null;
        }

        if (_out.Count > 1)
        {
            Debug.LogWarning("Warning! - TeamMemberTypesProfile_SO: TeamMemberTypeData duplicate types");
            return null;
        }

        if (_out.Count == 1)
        {
            return _out[0];
        }
        else
        {
            return null;
        }
    }
}


[Serializable]
public class TeamMemberTypeData
{
    [SerializeField] private TeamMemberTypes _TeamMemberType;
    [SerializeField] private int _damageBasic;
    [SerializeField] private int _damageSpecial;
    [SerializeField] private ElementalType _damageStrongAgainst;
    [SerializeField] private ElementalType _damageWeakAgainst;

    public TeamMemberTypes fn_Get_TeamMemberType() => _TeamMemberType;
    public void fn_Get_Damage(out int basic, out int special)
    {
        basic = _damageBasic;
        special = _damageSpecial;
    }

    public void fn_Get_StengthWeakness(out ElementalType strength, out ElementalType weakness)
    {
        strength = _damageStrongAgainst;
        weakness = _damageWeakAgainst;
    }
}

public enum TeamMemberTypes
{
    error,
    Electrical_Trade,
    Electrical_Specialist,
    Water_Trade,
    Water_Specialist,
    Structural_Trade,
    Structural_Specialist,
    SupportAndDesign_Trade,
    SupportAndDesign_Specialist,
    Management_Trade,
    Management_Specialist
}

public enum ElementalType
{
    error,
    Electric,
    Water,
    Structural,
    SupportAndDesign,
    Management
}

