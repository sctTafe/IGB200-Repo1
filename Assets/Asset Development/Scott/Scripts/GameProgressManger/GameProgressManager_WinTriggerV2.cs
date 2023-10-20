using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameProgressManager_WinTriggerV2 : MonoBehaviour
{

    public UnityEvent _OnBridgeIsComplete;
    public UnityEvent _OnSwitchSceneEvent;

    [SerializeField] private float _timeToWiatBeforeFinishing = 2f;

    public void fn_ProgressionSceneComplete()
    {
        _OnBridgeIsComplete?.Invoke();
        StartCoroutine(WaitTime_CR(_timeToWiatBeforeFinishing));
        _OnSwitchSceneEvent?.Invoke();
    }


    IEnumerator WaitTime_CR(float waittime) {
        yield return new WaitForSeconds(waittime);
    }

}
