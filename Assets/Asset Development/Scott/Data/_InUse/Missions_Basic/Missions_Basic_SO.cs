using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Missions_Basic_SO", menuName = "SO/Missions_Basic_SO")]

public class Missions_Basic_SO : ScriptableObject
{
    [SerializeField] public int _missionUID; 
    [SerializeField] public String _name;
    [TextArea] [SerializeField] public String _info;
    [SerializeField] public Sprite _missionPic;
    [SerializeField] public int _daysToComplete;


    [SerializeField] public GameObject _unfinishedPrefab;
    [SerializeField] public GameObject _finishedPrefab;

    private bool _isVisible;
    private bool _isComplete;

}
