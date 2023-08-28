using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamMember_Basic_SO", menuName = "SO/TeamMember_Basic_SO")]

public class TeamMember_Basic_SO : ScriptableObject
{
    [SerializeField] public String _name;
    [SerializeField] public Sprite _profilePic;
    [SerializeField] public Sprite _toolPic;
    [SerializeField] public String _bio;
}
