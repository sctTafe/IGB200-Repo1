using UnityEngine;
using UnityEngine.Events;

public class MissionSelection_Mng : MonoBehaviour
{
    


    [Header("Debugging & Test Options")]
    public bool _isDebugging = false;

    private bool _isTestBattleModeOn = false; // depreciated, all related functionality needs removing

    [Header("UI Output Dependencies")]
    [SerializeField] private MissionBrife_UI _MissionBrife_UI;
    [SerializeField] private MissionTeamSelection_UI _MissionTeamSelection_UI;

    // Bound Mission
    MissionDataHolder _currentBound_MissionDataHolder;

    /// <summary>
    ///  Loads the relevant data to the Transfer System and starts the Mission
    /// </summary>

    public void fn_TryStartMission()
    {
        // 1) Check if mission team > 0 members
        if (TeanMember_SelectionGroupHolder_PersistentSingletonMng.Instance._selectedMissionTeam._teamMembersGroup.Count > 0)
        {
            fn_StartMission();
        }
    }
    public void fn_StartMission()
    {
        DataTransfer_PersistentSingletonMng.Instance.fn_LoadMissionDataAndTransfer(_currentBound_MissionDataHolder._MissionSO);
    }

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
