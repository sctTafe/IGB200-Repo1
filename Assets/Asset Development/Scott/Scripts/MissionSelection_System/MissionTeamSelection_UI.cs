using TMPro;
using UnityEngine;

/// <summary>
/// DOSE: Populates Mission Team Selection Window with 'bound' mission info
/// </summary>
public class MissionTeamSelection_UI : MonoBehaviour
{
    // - UI Panel Root -
    [SerializeField] private RectTransform _missionTeamSelection_Root;
    // - Mission Info -
    [SerializeField] private TMP_Text _missionNameTMP;

    private MissionDataHolder _currentBoundMissionDataHolder;

    public void fn_BindMissionDataHolder(MissionDataHolder newBoundMissionDataHolder)
    {
        if (newBoundMissionDataHolder != null)
        {
            _currentBoundMissionDataHolder = newBoundMissionDataHolder;

            // - Mission Info - 
            _missionNameTMP.SetText(_currentBoundMissionDataHolder._MissionSO._missionName.ToString());
        }
    }
    public void fn_EnableMissionTeamSelectionWindow(bool isEnabled = true)
    {
        _missionTeamSelection_Root.gameObject.SetActive(isEnabled);
    }
}
