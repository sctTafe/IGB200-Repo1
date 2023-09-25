using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Missions_Basic_SO", menuName = "SO/Missions_Basic_SO")]

public class Missions_Basic_SO : ScriptableObject
{
    public Action <bool> _OnStateChange_isEnabled;
    
    [SerializeField] public int _missionUID; 
    [SerializeField] public String _missionName;
    
    [TextArea] [SerializeField] public String _missionShortBrife;
    [TextArea] [SerializeField] public String _missionDescription;
    [SerializeField] public Sprite _missionPic;

    // - Mission Stats -
    [SerializeField] public int _missionDifficultyRating;
    [SerializeField] public int _completionBonusPoints;
    [SerializeField] public int _daysToComplete;
    
     
    [SerializeField] public GameObject _unfinishedPrefab;
    [SerializeField] public GameObject _finishedPrefab;

    // - Other - 
    [SerializeField] public bool _isEnabledFromGameStart = false;

    // - Basic Internal Stats - 
    private bool _isEnabled = false;
    private bool _isVisible = true;
    private bool _isComplete = false;

    public void fn_SetEnabledState(bool enabled)
    {
        _isEnabled = enabled;
        _OnStateChange_isEnabled?.Invoke(enabled);
    }


}
