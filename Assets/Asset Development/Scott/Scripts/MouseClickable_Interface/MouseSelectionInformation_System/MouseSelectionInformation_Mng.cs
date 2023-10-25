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
                Debug.LogError("ProgressionSelection_Mng Instance is not found.");
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

    public MouseSelectionInformation_ItemData CurrentMouseSelectionItemData { get; private set; }
    private ProjectPoints_PersistentSingletonMng _PPMng;
    private void Awake()
    {
        SingeltonSetup();
    }


    public void fn_Bind_CurrentlySelected(MouseSelectionInformation_ItemData mouseSelectionItemData)
    {
        CurrentMouseSelectionItemData = mouseSelectionItemData;
        OnUpdateOfPSI?.Invoke();
    }

    public void fn_TryBuyUpgrade()
    {
        // DOSE: Try to buy the upgrade for its cost from the Project Points Mng Instance, If it returns true, action the purchase

        _PPMng ??= ProjectPoints_PersistentSingletonMng.Instance;
        // check is not already purchased
        if (!CurrentMouseSelectionItemData._isPurchased)
        {
            // check is there are enough Project Points to purchase & deduct them if true
            if (_PPMng.fn_TrySubtractPoints(CurrentMouseSelectionItemData._upgradeCost)) {
                // set the item to purchased 
                CurrentMouseSelectionItemData.fn_Purchase();
                OnUpdateOfPSI?.Invoke();
            }
        }
    }

    public void fn_TrySelectMission()
    {
        // NOTE: this is a bad way of doing this -> this just triggers the action on the 'CurrentMouseSelectionItemData'
        CurrentMouseSelectionItemData.fn_MissionSelect();
    }



}


