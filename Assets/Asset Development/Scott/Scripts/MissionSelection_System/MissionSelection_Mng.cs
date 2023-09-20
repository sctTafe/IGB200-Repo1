using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelection_Mng : MonoBehaviour
{
    private bool _isDebugging = false;
    [SerializeField] private MissionBrife_UI _MissionBrife_UI;
    [SerializeField] private MissionTeamSelection_UI _MissionTeamSelection_UI;

    // Bound Mission
    MissionDataHolder _currentBound_MissionDataHolder;



    public void fn_BindMissionDataHolder(MissionDataHolder newMissionDataHolder)
    {
        if (_isDebugging) Debug.Log("MissionSelection_Mng: fn_BindMissionDataHolder Called");
        if (newMissionDataHolder != null)
        {
            _currentBound_MissionDataHolder = newMissionDataHolder;
            BindDataToMissionBrife();
            BindDataToMissionTeamSelection();            
        }
        
    } 

    private void BindDataToMissionBrife()
    {
        if (_MissionBrife_UI != null)
        {
            if (_currentBound_MissionDataHolder != null)
            {
                _MissionBrife_UI.fn_BindMissionDataHolder(_currentBound_MissionDataHolder);
                _MissionBrife_UI.fn_EnableMissionBrifeWindow();
            }             
        } else
        {
            Debug.LogError("MissionSelection_Mng: _MissionBrife_UI is Null!");
        }
    }
    private void BindDataToMissionTeamSelection()
    {
        if (_MissionTeamSelection_UI != null)
        {
            if (_currentBound_MissionDataHolder != null)
            {
                _MissionTeamSelection_UI.fn_BindMissionDataHolder(_currentBound_MissionDataHolder);              
            }
        }
        else
        {
            Debug.LogError("MissionSelection_Mng: _MissionTeamSelection_UI is Null!");
        }
    }


}
