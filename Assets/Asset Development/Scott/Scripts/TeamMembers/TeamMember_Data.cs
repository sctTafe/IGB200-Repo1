using System;
using UnityEngine;

public class TeamMember_Data 
{
    [SerializeField] public event Action _OnChange;

    // Unique ID
    [SerializeField] public int _uID;

    // Static Information
    [SerializeField] public string _name;
    [SerializeField] public string _bio;
    [SerializeField] public Sprite _profileSprite;
    [SerializeField] public Sprite _toolSprite;

    // Dynamic Information
    [SerializeField] public int _level;
    [SerializeField] public int _xp;
    [SerializeField] public float _currentMorale;
    [SerializeField] public float _currentEnergy;
    
    // Dynamic Supporting Information
    [SerializeField] public float _maxMorale;
    [SerializeField] public float _maxEnergy;

    public TeamMember_Data()
    {
        _level = 1;
        _xp = 0;
        _maxMorale = 100;
        _maxEnergy = 100;
        _currentMorale = _maxMorale;
        _currentEnergy = _maxEnergy;
    }
}
