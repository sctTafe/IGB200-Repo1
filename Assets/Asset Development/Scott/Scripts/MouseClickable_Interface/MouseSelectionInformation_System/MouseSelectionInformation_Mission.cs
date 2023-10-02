using UnityEngine;
using UnityEngine.Events;

public class MouseSelectionInformation_Mission : MonoBehaviour
{
    public UnityEvent _OnMissionSelectionBtn;

    // 'ProgressionSelectionItem' is an Information Holder, setup in the editor for what ever this script is attached to 
    public MouseSelectionInformation_ItemData _MouseSelectionInfoItemData;
    
    [SerializeField] private Transform _ghostTransform;
    [SerializeField] private Transform _finTransform;

    public void Awake()
    {
        // Bind to the 'ProgressionSelectionItem', OnChange Event
        _MouseSelectionInfoItemData.OnChange += HandleOnChange;
    }

    public void Start()
    {
        Setup_PopulateSelectionItemDataFromMissionSO();
    }


    // -- Primary Function --
    // - Externally Called to bind this selected item to the MouseSelectionInformation_Mng -
    public void fn_BindToProgressionSelectionMng()
    {
        MouseSelectionInformation_Mng.Instance.fn_Bind_CurrentlySelected(_MouseSelectionInfoItemData);
    }


    private void Setup_PopulateSelectionItemDataFromMissionSO() {
        _MouseSelectionInfoItemData._isAMission = true;

        if (TryGetComponent<MissionDataHolder>(out MissionDataHolder mDH)) {
            Missions_Basic_SO dataSO = mDH._MissionSO;
            _MouseSelectionInfoItemData._name = dataSO._missionName;
            _MouseSelectionInfoItemData._missionRewardPoints = dataSO._completionBonusPoints;
        }
    }

    // - Event Handling - 
    private void HandleOnChange()
    {
        // NOTE: Messy Work Aound -  triggers when the '_OnMissionSelectionBtn' when the OnChange Event is called - this is a messy and bad work around & should all be refactored
        _OnMissionSelectionBtn?.Invoke();
    }

}
