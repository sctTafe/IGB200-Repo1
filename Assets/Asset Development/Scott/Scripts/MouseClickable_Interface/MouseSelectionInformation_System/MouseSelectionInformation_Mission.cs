using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class MouseSelectionInformation_Mission : MonoBehaviour
{
    public UnityEvent _OnMissionSelectionBtn;

    // 'ProgressionSelectionItem' is an Information Holder, setup in the editor for what ever this script is attached to 
    public MouseSelectionInformation_Item _MouseSelectionInfoItem;
    
    [SerializeField] private Transform _ghostTransform;
    [SerializeField] private Transform _finTransform;

    public void Awake()
    {
        // Bind to the 'ProgressionSelectionItem', OnChange Event
        _MouseSelectionInfoItem.OnChange += HandleOnChange;
    }

    // Called Externally 
    public void fn_BindToProgressionSelectionMng()
    {
        MouseSelectionInformation_Mng.Instance.fn_Bind_CurrentlySelected(_MouseSelectionInfoItem);
    }

    private void HandleOnChange()
    {
        // NOTE: Messy Work Aound -  triggers when the '_OnMissionSelectionBtn' when the OnChange Event is called - this is a messy and bad work around & should all be refactored
        _OnMissionSelectionBtn?.Invoke();
    }

}
