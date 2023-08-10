using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressionSelection_UI : MonoBehaviour
{
    [SerializeField] private RectTransform _ProgressionSelectionUI_Pannel;
    [SerializeField] private RectTransform _PurchaseInfo_Pannel;
    [SerializeField] private TMP_Text _nameTMP;
    [SerializeField] private TMP_Text _costTMP;
    private ProgressionSelection_Mng _PSM;

    public void Start()
    {
        fn_HidePannel();
    }   
    public void fn_UpdateValues()
    {
        _PSM ??= ProgressionSelection_Mng.Instance;
        _PurchaseInfo_Pannel.gameObject.SetActive(false);
        _nameTMP.SetText(_PSM._currentPSI._name.ToString());
        _costTMP.SetText(_PSM._currentPSI._upgradeCost.ToString());
        if (_PSM._currentPSI._isPurchased == false)
            _PurchaseInfo_Pannel.gameObject.SetActive(true);
        _ProgressionSelectionUI_Pannel.gameObject.SetActive(true);
    }

    public void fn_HidePannel()
    {
        _ProgressionSelectionUI_Pannel.gameObject.SetActive(false);
    }

    public void fn_HandleBuyBtn()
    {
        _PSM ??= ProgressionSelection_Mng.Instance;
        _PSM.fn_TryBuyUpgrade();
    } 

}
