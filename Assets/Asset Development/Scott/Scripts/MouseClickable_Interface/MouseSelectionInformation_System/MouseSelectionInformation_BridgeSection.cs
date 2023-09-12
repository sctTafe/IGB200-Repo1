using UnityEngine;
using UnityEngine.Events;

public class MouseSelectionInformation_BridgeSection : MonoBehaviour
{
    public UnityEvent _OnBridgeSectionPurchased;

    // 'ProgressionSelectionItem' is an Information Holder, setup in the editor for what ever this script is attached to 
    public MouseSelectionInformation_Item ThisProgressionSelectionItem;
    
    [SerializeField] private Transform _ghostTransform;
    [SerializeField] private Transform _finTransform;

    public void Awake()
    {
        // Bind to the 'ProgressionSelectionItem', OnChange Event
        ThisProgressionSelectionItem.OnChange += HandlePSIChange;
    }

    // Called Externally 
    public void fn_BindToProgressionSelectionMng()
    {
        MouseSelectionInformation_Mng.Instance.fn_Bind_CurrentlySelected(ThisProgressionSelectionItem);
    }

    private void HandlePSIChange()
    {
        // check if the 'ProgressionSelectionItem', _isPurchased is true, OnChange Event
        if (ThisProgressionSelectionItem._isPurchased)
        {
            // Switch which game object (visual) version is active / showing
            _ghostTransform.gameObject.SetActive(false);
            _finTransform.gameObject.SetActive(true);
            // invoke 'OnPurchase' Unity Event - for particle effects and other responses
            _OnBridgeSectionPurchased?.Invoke();

        }
    }

}
