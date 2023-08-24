using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///
/// Handles Progress To the Completion of the Game
/// 
/// </summary>
public class GameProgressManager : MonoBehaviour
{
    public UnityEvent _OnBridgeIsComplete;
    public int _numberOfBridgeBitsToBuildToWin = 5;
    private int _currentBridgeBitsBuild;

    void Start()
    {
        _currentBridgeBitsBuild = 0;
    }


    public void fn_RegisterBridgeBitComplete()
    {
        _currentBridgeBitsBuild++;
        CheckIfBridgeIsComplete();
    }

    private void CheckIfBridgeIsComplete()
    {
        if (_currentBridgeBitsBuild >= _numberOfBridgeBitsToBuildToWin)
        {
            _OnBridgeIsComplete?.Invoke();
        }
    }

}
