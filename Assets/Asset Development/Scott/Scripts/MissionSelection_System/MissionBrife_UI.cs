
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DOSE: Populates Mission Brief UI Window with 'bound' mission info
/// </summary>
public class MissionBrife_UI : MonoBehaviour
{
    // - UI Panel Root -
    [Header("Panel Root")]
    [SerializeField] private RectTransform _missionBrife_Root;
    // - Mission Info -
    [Header("Mission Info - Output")]
    [SerializeField] private TMP_Text _missionNameTMP;
    [SerializeField] private TMP_Text _missionDescriptionTMP;
    // - Mission Stats -
    [Header("Mission Stats - Output")]
    [SerializeField] private TMP_Text _missionDifficulty;
    [SerializeField] private TMP_Text _completionBonusPoints;
    [SerializeField] private TMP_Text _DaysToComplete;
    [SerializeField] private Image _tradeNeededSymbol_Img;
    // - Support 
    [SerializeField] private TeamMemberAssetsData_SO _AssetDataSO;

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

            if (_tradeNeededSymbol_Img != null)
            {
                EnemyTypes enemyTypes = _currentBoundMissionDataHolder._MissionSO._enemies[0];
                TeamMemberClassType teamMClass = GetTeamMemberClassTypeFromEnemyType(enemyTypes);
                Sprite toolSprite = _AssetDataSO.fn_GetIconSprite(teamMClass);
                _tradeNeededSymbol_Img.sprite = toolSprite;
            }

        }     
    }

    // NOTE: This function should exist somewhere else
    private TeamMemberClassType GetTeamMemberClassTypeFromEnemyType(EnemyTypes enemyType)
    {

        if (enemyType == EnemyTypes.Water)
        {
            return TeamMemberClassType.Water;
        }
        if (enemyType == EnemyTypes.Electric)
        {
            return TeamMemberClassType.Electric;
        }
        if (enemyType == EnemyTypes.Planning)
        {
            return TeamMemberClassType.Planning;
        }
        if (enemyType == EnemyTypes.Practical)
        {
            return TeamMemberClassType.Practical;
        }

        return TeamMemberClassType.error;
    }


    public void fn_EnableMissionBrifeWindow(bool isEnabled = true)
    {
        _missionBrife_Root.gameObject.SetActive(isEnabled);
    }
}
