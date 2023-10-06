using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDataTransferSupportScript : MonoBehaviour
{
    public bool _isTransferTestingEnabled = false;

    // - Primary Functions - 
    public void fn_FinishBattle()
    {
        if (_isTransferTestingEnabled)
        {
            fn_DepleteTeamMemberStarts();
        }

        DataTransfer_PersistentSingletonMng.Instance.fn_HandleMissionFinished();
    }

    // - Testing Functions - 
    public void fn_DepleteTeamMemberStarts() {
        // Deplete the Team Members Energy & Morale by between 10 - 20 % (of Max)
        foreach (var teamMemberGO in StaticData.team) {
            TeamMemberTransfer_Data data = teamMemberGO.GetComponent<TeamMemberTransfer_Data>();
            data._currentEnergy -= data._maxEnergy * Random.Range(0.1f, 0.2f); //(10:20)%
            data._currentMorale -= data._maxMorale * Random.Range(0.1f, 0.2f);
        }
    }

}
