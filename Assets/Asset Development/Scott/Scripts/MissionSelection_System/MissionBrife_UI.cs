using TMPro;
using UnityEngine;

/// <summary>
/// DOSE: Populates Mission Brief UI Window with 'bound' mission info
/// </summary>
public class MissionBrife_UI : MonoBehaviour
{
    // - UI Panel Root -
    [SerializeField] private RectTransform _missionBrife_Root;
    // - Mission Info -
    [SerializeField] private TMP_Text _missionNameTMP;
    [SerializeField] private TMP_Text _missionDescriptionTMP;
    // - Mission Stats -
    [SerializeField] private TMP_Text _missionDifficulty;
    [SerializeField] private TMP_Text _completionBonusPoints;
    [SerializeField] private TMP_Text _DaysToComplete;

    private MissionDataHolder _currentBoundMissionDataHolder;

    public void fn_BindMissionDataHolder(MissionDataHolder newBoundMissionDataHolder)
    {
        if (newBoundMissionDataHolder != null) 
        {
            _currentBoundMissionDataHolder = newBoundMissionDataHolder;

            // - Mission Info - 
            _missionNameTMP.SetText(_currentBoundMissionDataHolder._MissionSO._missionName.ToString());
            _missionDescriptionTMP.SetText(_currentBoundMissionDataHolder._MissionSO._missionDescription.ToString());

            // - Mission Stats - 
            _missionDifficulty.SetText(_currentBoundMissionDataHolder._MissionSO._missionDifficultyRating.ToString());
            _completionBonusPoints.SetText(_currentBoundMissionDataHolder._MissionSO._completionBonusPoints.ToString());
            _DaysToComplete.SetText(_currentBoundMissionDataHolder._MissionSO._daysToComplete.ToString());
        }
    }
    public void fn_EnableMissionBrifeWindow(bool isEnabled = true)
    {
        _missionBrife_Root.gameObject.SetActive(isEnabled);
    }
}
