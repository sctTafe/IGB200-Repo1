using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMember_Data 
{
    [SerializeField] public event Action OnChange;
    [SerializeField] public string _name;
    [SerializeField] public int _uID;

}
