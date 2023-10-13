using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Missions_Basic_SO", menuName = "SO/Missions_Basic_SO")]

public class Missions_Basic_SO : ScriptableObject
{
    public Action <bool> _OnStateChange_isEnabled;

    [SerializeField] public int _missionUID; 
    [SerializeField] public String _missionName;
    
    [TextArea] [SerializeField] public String _missionShortBrief;
    [TextArea] [SerializeField] public String _missionDescription;
    [SerializeField] public Sprite _missionPic;

    // - Mission Stats -
    [SerializeField] public int _missionDifficultyRating;
    [SerializeField] public int _completionBonusPoints;
    [SerializeField] public int _daysToComplete;
   
    [Header("Battle Settings")]
    [SerializeField] public int _secnePosition;
    [SerializeField] public EnemyTypes[] _enemies;

    [Header("Mission Success Outcomes")]
    // - Missions & Interaction Objects Enabled On Mission Completion -
    [SerializeField] public Missions_Basic_SO[] _MissionsEnabledOnCompletion_Array;
    [SerializeField] public BridgeParts_SO[] _BridgePartsEnabledOnCompletion_Array;

    // - Other - 
    [Header("Other Settings")]
    [SerializeField] public bool _isEnabledFromGameStart = false;
    [SerializeField] public GameObject _unfinishedPrefab;
    [SerializeField] public GameObject _finishedPrefab;

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
