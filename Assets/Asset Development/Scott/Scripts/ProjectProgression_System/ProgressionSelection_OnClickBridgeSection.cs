using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProgressionSelection_OnClickBridgeSection : MonoBehaviour
{
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
            _ghostTransform.gameObject.SetActive(false);
            _finTransform.gameObject.SetActive(true);
        }
    }

}
