using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamMemberAssetsData_SO", menuName = "SO/TeamMemberAssetsData_SO")]
public class TeamMemberAssetsData_SO : ScriptableObject
{
    [SerializeField] private TeamMemberClassTypeIcons[] _TypeIcon;

    public Sprite fn_GetIconSprite(TeamMemberClassType classType)
    {
        if (_TypeIcon != null && _TypeIcon.Length > 0)
        {
            foreach (TeamMemberClassTypeIcons TMCTI  in _TypeIcon)
            {
                if (TMCTI._ClassType == classType)
                    return TMCTI._toolPic;
            }
        }
        return null;
    }
}

[Serializable]
public class TeamMemberClassTypeIcons
{
    [SerializeField] public TeamMemberClassType _ClassType;
    [SerializeField] public Sprite _toolPic;
}