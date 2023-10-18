using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamMemberAssetsData_Faces_SO", menuName = "SO/TeamMemberAssetsData_Faces_SO")]
public class TeamMemberAssetsData_Faces_SO : ScriptableObject
{
    [SerializeField] private TeamMember_ProfilePicImage[] _TypeIcon;

    public Sprite fn_GetProfilePicSprite(TeamMembers_ProfileImageGroup group = TeamMembers_ProfileImageGroup.any, TeamMember_ProfileHairType hair = TeamMember_ProfileHairType.any)
    {
        List<TeamMember_ProfilePicImage> _tempList = new List<TeamMember_ProfilePicImage>();

        foreach (TeamMember_ProfilePicImage profilePics in _TypeIcon) {
            if ((group == TeamMembers_ProfileImageGroup.any || profilePics._group == group) &&
                (hair == TeamMember_ProfileHairType.any || profilePics._hairType == hair)) {
                _tempList.Add(profilePics);
            }
        }

        if (_tempList.Count > 0) {
            int randomIndex = UnityEngine.Random.Range(0, _tempList.Count);
            return _tempList[randomIndex]._profilePic;
        }
        Debug.LogError("TeamMemberAssetsData_Faces_SO: fn_GetProfilePicSprite; Error! - could not find matching Profile Pic! ");
        return null;
    }
}

[Serializable]
public class TeamMember_ProfilePicImage {
    [SerializeField] public Sprite _profilePic;
    [SerializeField] public TeamMembers_ProfileImageGroup _group;
    [SerializeField] public TeamMember_ProfileHairType _hairType;

}

public enum TeamMembers_ProfileImageGroup {
    error,
    any,
    GroupA,
    GroupB,
    GroupC
}

public enum TeamMember_ProfileHairType
{
    error,
    any,
    Short,
    Long,
    Rounded
}