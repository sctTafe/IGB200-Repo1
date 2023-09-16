using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamMemberTransfer_Data : MonoBehaviour
{
    // Unique ID
    [SerializeField] public int _uID;
    [SerializeField] public TeamMemberClassType _classType;   
    [SerializeField] public float _maxEnergy;
    [SerializeField] public float _currentEnergy;
    
    //Post battle information
    [SerializeField] public int _numDeaths;
}
