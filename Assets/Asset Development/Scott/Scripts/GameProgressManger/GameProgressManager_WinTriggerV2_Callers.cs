using System.Collections;
using UnityEngine;

public class GameProgressManager_WinTriggerV2_Callers : MonoBehaviour
{
    [SerializeField] private GameProgressManager_WinTriggerV2 _WinTriggerV2;

    void Start()
    {
        // STEP1: wait till the Progression Game Objects have updated to enabled/disabled
        StartCoroutine(WaitTime_CR(2f,this.gameObject));

        //STEP2: check if this game object is active
    }

    IEnumerator WaitTime_CR(float waittime, GameObject gameObject) {

        Debug.Log("GameProgressManager_WinTriggerV2_Callers - 1");

        yield return new WaitForSeconds(waittime);

        Debug.Log("GameProgressManager_WinTriggerV2_Callers - 2");


        if (gameObject.activeSelf) {
            _WinTriggerV2.fn_ProgressionSceneComplete();
        }
        //else {
            
        //}

    }



}
