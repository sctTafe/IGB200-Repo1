using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Is Singleton
///
/// Dependencies
///     ProjectPoints_Mng Instance - used for Try Buy Upgrade
/// 
/// </summary>
public class MouseSelectionInformation_Mng : MonoBehaviour
{
    #region Singelton Setup
    private static MouseSelectionInformation_Mng _instance;
    public static MouseSelectionInformation_Mng Instance {
        get {
            if (_instance == null) {
                Debug.LogError("ProgressionSelection_Mng instance is not found.");
            }
            return _instance;
        }
    }
    private void SingeltonSetup() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion


    public UnityEvent OnUpdateOfPSI;
    public MouseSelectionInformation_Item _currentMouseSelectionItem { get; private set; }
    private ProjectPoints_Mng _PPMng;
    private void Awake()
    {
        SingeltonSetup();
    }


    public void fn_Bind_CurrentlySelected(MouseSelectionInformation_Item mouseSelectionItem)
    {
        _currentMouseSelectionItem = mouseSelectionItem;
        OnUpdateOfPSI?.Invoke();
    }

    public void fn_TryBuyUpgrade()
    {
        // DOSE: Try to buy the upgrade for its cost from the Project Points Mng Instance, If it returns true, action the purchase

        _PPMng ??= ProjectPoints_Mng.Instance;
        // check is not already purchased
        if (!_currentMouseSelectionItem._isPurchased)
        {
            // check is there are enough Project Points to purchase & deduct them if true
            if (_PPMng.fn_TrySubtractPoints(_currentMouseSelectionItem._upgradeCost)) {
                // set the item to purchased 
                _currentMouseSelectionItem.fn_Purchase();
                OnUpdateOfPSI?.Invoke();
            }
        }
    }

}


