using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelection_Mng : MonoBehaviour
{
    [SerializeField] private MissionBrife_UI _MissionBrife_UI;

    // Bound Mission
    MissionDataHolder _currentBound_MissionDataHolder;



    public void fn_BindMissionDataHolder(MissionDataHolder _mDH)
    {
        if (_currentBound_MissionDataHolder != null)
        {
            _currentBound_MissionDataHolder = _mDH;
            BindDataToMissionBrife();
        }
        
    } 

    private void BindDataToMissionBrife()
    {
        if (_MissionBrife_UI != null)
        {
            if (_currentBound_MissionDataHolder != null)
                _MissionBrife_UI.fn_BindMissionDataHolder(_currentBound_MissionDataHolder);
                     
        } else
        {
            Debug.LogError("MissionSelection_Mng: _MissionBrife_UI is Null!");
        }
    }
    private void BindDataToMissionTeamSelection()
    {

    }


}
