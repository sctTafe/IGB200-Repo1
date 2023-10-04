using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Debug_BattleSceneResponces : MonoBehaviour
{
    public UnityEvent _OnChangeSceneTrigger;

    // Notes:
    // - Added -
    // function for win battle
    // function for lose battle,
    // function for depleting team members stats,
    // - Need to Add - 
    // function for gain PP points



    public void fn_WinBattle()
    {
        // Set Battle to Won
        BattleTransferData_PersistentSingleton.Instance._isMissionCompletedSuccessfully = true;

        // Call DataTransfer Mng To End Mission
        DataTransfer_PersistentSingletonMng.Instance.fn_HandleMissionFinished();

        // Change Scene
        _OnChangeSceneTrigger?.Invoke();
    }

    public void fn_LoseBattle()
    {
        // Set Battle to Lost
        BattleTransferData_PersistentSingleton.Instance._isMissionCompletedSuccessfully = false;

        // Call DataTransfer Mng To End Mission
        DataTransfer_PersistentSingletonMng.Instance.fn_HandleMissionFinished();

        // Change Scene
        _OnChangeSceneTrigger?.Invoke();
    }

    public void fn_DepleteTeamMemberStarts()
    {
        // Deplete the Team Members Energy & Morale by between 10 - 20 % (of Max)
        foreach (var teamMemberGO in BattleTransferData_PersistentSingleton._missionTeam_List)
        {
            TeamMemberTransfer_Data data = teamMemberGO.GetComponent<TeamMemberTransfer_Data>();
            data._currentEnergy -= data._maxEnergy * Random.Range(0.1f, 0.2f); //(10:20)%
            data._currentMorale -= data._maxMorale * Random.Range(0.1f, 0.2f);
        }
    }

}
