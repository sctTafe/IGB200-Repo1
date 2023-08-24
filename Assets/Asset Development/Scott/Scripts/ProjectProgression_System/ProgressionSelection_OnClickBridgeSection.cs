using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class ProgressionSelection_OnClickBridgeSection : MonoBehaviour
{
    public UnityEvent _OnBridgeSectionPurchased;
    
    public ProgressionSelectionItem ThisProgressionSelectionItem;
    
    [SerializeField] private Transform _ghostTransform;
    [SerializeField] private Transform _finTransform;

    public void Awake()
    {
        ThisProgressionSelectionItem.OnChange += HandlePSIChange;
    }

    // Called Externally 
    public void fn_BindToProgressionSelectionMng()
    {
        ProgressionSelection_Mng.Instance.fn_Bind_CurrentlySelected(ThisProgressionSelectionItem);
    }

    private void HandlePSIChange()
    {
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
