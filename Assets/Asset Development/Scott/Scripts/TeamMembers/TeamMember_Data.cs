using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMember_Data 
{
    [SerializeField] public event Action _OnChange;

    [SerializeField] public int _uID;

    [SerializeField] public string _name;
    [SerializeField] public string _bio;
    [SerializeField] public Sprite _profileSprite;
    [SerializeField] public Sprite _toolSprite;

}
