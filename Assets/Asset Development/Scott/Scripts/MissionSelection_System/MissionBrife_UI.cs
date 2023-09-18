using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionBrife_UI : MonoBehaviour
{

    [SerializeField] private RectTransform _missionBrife_Root;
    [SerializeField] private TMP_Text _missionNameTMP;
    [SerializeField] private TMP_Text _missionDescriptionTMP;

    private MissionDataHolder _currentBoundMissionDataHolder;


    public void fn_BindMissionDataHolder(MissionDataHolder currentBoundMissionDataHolder)
    {
        if (_currentBoundMissionDataHolder != null) 
        {
            _currentBoundMissionDataHolder = currentBoundMissionDataHolder;
        }
    }



}
