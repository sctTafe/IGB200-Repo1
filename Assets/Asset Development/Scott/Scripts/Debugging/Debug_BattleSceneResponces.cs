using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Debug_BattleSceneResponces : MonoBehaviour
{
    public UnityEvent _OnChangeSceneTrigger;

    // function for win battle
    // function for lose battle,
    // function for gain PP points
    // function for 

    // Start is called before the first frame update

    public void fn_WinBattle()
    {
        // Set Battle to Won
        BattleTransferData_PersistentSingleton.Instance._isMissionCompletedSuccessfully = true;
        DataTransfer_PersistentSingletonMng.Instance.fn_HandleMissionFinished();
        _OnChangeSceneTrigger?.Invoke();
    }

    public void fn_LoseBattle()
    {
        // Set Battle to Won
        BattleTransferData_PersistentSingleton.Instance._isMissionCompletedSuccessfully = false;
        DataTransfer_PersistentSingletonMng.Instance.fn_HandleMissionFinished();
        _OnChangeSceneTrigger?.Invoke();
    }


}
