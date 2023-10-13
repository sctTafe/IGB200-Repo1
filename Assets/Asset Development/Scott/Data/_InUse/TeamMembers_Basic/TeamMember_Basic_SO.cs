using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamMember_Basic_SO", menuName = "SO/TeamMember_Basic_SO")]

public class TeamMember_Basic_SO : ScriptableObject
{
    [SerializeField] public String _name;
    [SerializeField] public String _nameAndJob;
    [SerializeField] public Sprite _profilePic;
    //[SerializeField] public Sprite _toolPic;          // Removed as this is replaced with TeamMemberAssetData SO
    [TextArea] [SerializeField] public String _bio;
    [SerializeField] public TeamMemberClassType _teamMemberClass;
}
