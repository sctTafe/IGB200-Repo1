using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDataHolder : MonoBehaviour
{
    [SerializeField] public Missions_Basic_SO _MissionSO;
   
    // - Needs Cleaning UP - 
    [SerializeField] private MissionSelection_Mng _MissionSelection_Mng;

    public void fn_BindToMissionSelection_Mng()
    {
        if (_MissionSelection_Mng != null)
        {
            // Bind Self to MissionSelection_Mng for display
            _MissionSelection_Mng.fn_BindMissionDataHolder(this);
        }
        else
        {
            Debug.LogError("MissionDataHolder: fn_BindToMissionSelection_Mng; _MissionSelection_Mng is null!");
        }
    }
    public int fn_GetMissionUID()
    {
        return _MissionSO._missionUID;
    }
    public bool fn_GetIsMissionEnabledFromStart()
    {
        return _MissionSO._isEnabledFromGameStart;
    }




}
