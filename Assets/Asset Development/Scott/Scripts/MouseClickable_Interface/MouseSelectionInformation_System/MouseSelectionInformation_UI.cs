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

    // - Public - 
    [SerializeField] private RectTransform _MouseSelectionInfoUI_Panel; 
    [SerializeField] private TMP_Text _nameTMP;
    
    // Purchase Panel
    [SerializeField] private RectTransform _PurchaseInfo_Panel;
    [SerializeField] private TMP_Text _costTMP;

    // Mission Panel
    [SerializeField] private RectTransform _MissionInfo_Panel;
    [SerializeField] private TMP_Text _pointsTMP;

    // - Priavte -

    private MouseSelectionInformation_Mng _MouseSelectionInfo_Mng;

    public void Start()
    {
        fn_HidePannel();
    }   
    public void fn_UpdateValues()
    {
        // connect if not
        _MouseSelectionInfo_Mng ??= MouseSelectionInformation_Mng.Instance;


        HandlePurchaseInfoPanel();
        HandleMissionInfoPanel();


        _nameTMP.SetText(_MouseSelectionInfo_Mng._currentMouseSelectionItem._name.ToString());
        _MouseSelectionInfoUI_Panel.gameObject.SetActive(true);
    }

    private void HandlePurchaseInfoPanel()
    {
        // disable  panel
        _PurchaseInfo_Panel.gameObject.SetActive(false);

        // check if panel is needed
        if (_MouseSelectionInfo_Mng._currentMouseSelectionItem._isPurchasable &&
            _MouseSelectionInfo_Mng._currentMouseSelectionItem._isPurchased == false)
        {
            // set buy panel cost value
            _costTMP.SetText(_MouseSelectionInfo_Mng._currentMouseSelectionItem._upgradeCost.ToString());
            // enable panel
            _PurchaseInfo_Panel.gameObject.SetActive(true);
        }
    }
    private void HandleMissionInfoPanel()
    {
        // disable buy panel
        _MissionInfo_Panel.gameObject.SetActive(false);

        // check if panel is needed
        if (_MouseSelectionInfo_Mng._currentMouseSelectionItem._isAMission)
        {
            // set output values
            _pointsTMP.SetText(_MouseSelectionInfo_Mng._currentMouseSelectionItem._missionRewardPoints.ToString());
            // enable panel
            _MissionInfo_Panel.gameObject.SetActive(true);
        }
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

    public void fn_HandleMissionSelectBtn()
    {
        _MouseSelectionInfo_Mng ??= MouseSelectionInformation_Mng.Instance;
        _MouseSelectionInfo_Mng.fn_TrySelectMission();
    }
}
