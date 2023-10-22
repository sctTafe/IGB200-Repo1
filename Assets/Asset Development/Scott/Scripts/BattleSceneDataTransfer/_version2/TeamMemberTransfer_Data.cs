using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMemberTransfer_Data : MonoBehaviour
{
    // - Foward Passed Data Only (not pulled back from) - 
    [SerializeField] public string _teamMemberName;
    [SerializeField] public TeamMemberClassType _classType;
    [SerializeField] public int _teamMemberLevel;
    [SerializeField] public float _maxEnergy;
    [SerializeField] public float _maxMorale;

    // - Foward & Backpassed Data -
    [SerializeField] public int _uID; // Unique ID - used to target team member to pass data back to
    [SerializeField] public float _currentEnergy;
    [SerializeField] public float _currentMorale;
    
    //Post battle information
    [SerializeField] public int _numDeaths;
    [SerializeField] public int moraleCount = 0;
    [SerializeField] public bool isExhausted;
}
