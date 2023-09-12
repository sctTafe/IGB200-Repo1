using UnityEngine;
using TMPro;

/// <summary>
/// 
/// 
/// 
/// 
/// </summary>

public class MouseSelectionInformation_UI : MonoBehaviour
{
    [SerializeField] private RectTransform _MouseSelectionInfoUI_Panel;
    [SerializeField] private RectTransform _PurchaseInfo_Panel;
    [SerializeField] private TMP_Text _nameTMP;
    [SerializeField] private TMP_Text _costTMP;
    private MouseSelectionInformation_Mng _MouseSelectionInfo_Mng;

    public void Start()
    {
        fn_HidePannel();
    }   
    public void fn_UpdateValues()
    {
        // connect if not
        _MouseSelectionInfo_Mng ??= MouseSelectionInformation_Mng.Instance;

        #region 'Buy Info Sub Panel'
        // disable buy panel
        _PurchaseInfo_Panel.gameObject.SetActive(false);
       
        if (_MouseSelectionInfo_Mng._currentMouseSelectionItem._isPurchasable &&
            _MouseSelectionInfo_Mng._currentMouseSelectionItem._isPurchased == false)
        {
            // set buy panel cost value
            _costTMP.SetText(_MouseSelectionInfo_Mng._currentMouseSelectionItem._upgradeCost.ToString());
            // enable buy panel
            _PurchaseInfo_Panel.gameObject.SetActive(true);
        }
        #endregion

        _nameTMP.SetText(_MouseSelectionInfo_Mng._currentMouseSelectionItem._name.ToString());
        _MouseSelectionInfoUI_Panel.gameObject.SetActive(true);
    }

    public void fn_HidePannel()
    {
        _MouseSelectionInfoUI_Panel.gameObject.SetActive(false);
    }

    public void fn_HandleBuyBtn()
    {
        _MouseSelectionInfo_Mng ??= MouseSelectionInformation_Mng.Instance;
        _MouseSelectionInfo_Mng.fn_TryBuyUpgrade();
    } 

}
