using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MouseSelectionInformation_BridgeSection : MonoBehaviour
{
    public UnityEvent _OnBridgeSectionPurchased;

    // 'ProgressionSelectionItem' is an Information Holder, setup in the editor for what ever this script is attached to 
    public MouseSelectionInformation_ItemData ThisProgressionSelectionItemData;
    
    [SerializeField] private Transform _ghostTransform;
    [SerializeField] private Transform _finTransform;

    public void Awake()
    {
        // Bind to the 'ProgressionSelectionItem', OnChange Event
        ThisProgressionSelectionItemData.OnChange += HandlePSIChange;
    }
    public void Start()
    {
        Setup_SetThisProgressionSelectionItemInformationFromLocalDataHolder();
    }
    public void OnDestroy()
    {
        ThisProgressionSelectionItemData.OnChange -= HandlePSIChange;
    }

    // Called Externally ( to bind to UI Side of things )
    public void fn_BindToProgressionSelectionMng()
    {
        MouseSelectionInformation_Mng.Instance.fn_Bind_CurrentlySelected(ThisProgressionSelectionItemData);
    }

    private void Setup_SetThisProgressionSelectionItemInformationFromLocalDataHolder()
    {
        if (TryGetComponent<BridgePartsDataHolder>(out BridgePartsDataHolder bPDH))
        {
            BridgeParts_SO dataSO = bPDH._BridgePartSO;
            ThisProgressionSelectionItemData._name = dataSO._bridgePartName;
        }
    }


    private void HandlePSIChange()
    {
        // check if the 'ProgressionSelectionItem', _isPurchased is true, OnChange Event
        if (ThisProgressionSelectionItemData._isPurchased)
        {
            // Switch which game object (visual) version is active / showing
            _ghostTransform.gameObject.SetActive(false);
            _finTransform.gameObject.SetActive(true);
            // invoke 'OnPurchase' Unity Event - for particle effects and other responses
            _OnBridgeSectionPurchased?.Invoke();

        }
    }

}
