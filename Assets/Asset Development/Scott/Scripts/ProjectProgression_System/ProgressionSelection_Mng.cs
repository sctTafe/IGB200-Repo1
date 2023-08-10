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
public class ProgressionSelection_Mng : MonoBehaviour
{
    #region Singelton Setup
    private static ProgressionSelection_Mng _instance;
    public static ProgressionSelection_Mng Instance {
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
    public ProgressionSelectionItem _currentPSI { get; private set; }
    private ProjectPoints_Mng _PPMng;
    private void Awake()
    {
        SingeltonSetup();
    }


    public void fn_Bind_CurrentlySelected(ProgressionSelectionItem psi)
    {
        _currentPSI = psi;
        OnUpdateOfPSI?.Invoke();
    }

    public void fn_TryBuyUpgrade()
    {
        // DOSE: Try to buy the upgrade for its cost from the Project Points Mng Instance, If it returns true, action the purchase

        _PPMng ??= ProjectPoints_Mng.Instance;
        // check is not already purchased
        if (!_currentPSI._isPurchased)
        {
            // check is there are enough Project Points to purchase & deduct them if true
            if (_PPMng.fn_TrySubtractPoints(_currentPSI._upgradeCost)) {
                // set the item to purchased 
                _currentPSI.fn_Purchase();
                OnUpdateOfPSI?.Invoke();
            }
        }
    }

}

[Serializable]
public class ProgressionSelectionItem
{
    public Action OnChange; 
    public string _name;
    public int _upgradeCost;
    public bool _isPurchased;
    public bool _isVisable;

    public void fn_Purchase()
    {
        _isPurchased = true;
        OnChange?.Invoke();
    }
}
