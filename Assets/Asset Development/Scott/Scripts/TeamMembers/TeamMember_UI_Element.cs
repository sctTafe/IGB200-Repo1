using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamMember_UI_Element : MonoBehaviour
{
    public TMP_Text _name;
    public TMP_Text _nameAndJobText;
    public TMP_Text _bioText;
    public Image _profileImg;
    public Image _toolImg;

    [Header("Sliders")] 
    public SliderOutputControl_UI _energySiderOut;
    public SliderOutputControl_UI _moraleSilderOut;
    [Header("Icon Gauge")]
    public IconGauge_UI _energyIconGauge;
    public IconGauge_UI _moraleIconGauge;

    // - Private -
    private TeamMember_UI_ElementsMng _currentMng;
    private TeamMember_Data _currentTeamMemberData;

    private void OnDestroy() {
        Unsubscribe();
    }

    public void fn_Bind(TeamMember_Data teamMemberData, TeamMember_UI_ElementsMng mng) {
        // UnBind from Old
        Unsubscribe();
        UnbindValues();

        // Bind New
        _currentTeamMemberData = teamMemberData;
        _currentMng = mng;

        if (_currentTeamMemberData != null) {
            _currentTeamMemberData._OnChange += HandleChange_UpdateValues;
            HandleChange_UpdateValues();
            this.gameObject.SetActive(true);
        }
        else {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// This is a function for handling when the UI Element is a button, and is clicked
    /// </summary>
    public void fn_Handle_OnElementBtn() {
        Debug.Log("OnElementBtn Pressed");
        _currentMng.fn_ElementBtnPressed(_currentTeamMemberData);
    }
    public void fn_Handle_OnPrimaryActionBtn() {
        Debug.Log("OnPrimaryActionBtn Pressed");
        _currentMng.fn_PrimaryActionTriggered(_currentTeamMemberData);
    }





    #region Private Functions
    private void HandleChange_UpdateValues() {

        _name?.SetText(_currentTeamMemberData._name);
        _nameAndJobText?.SetText(_currentTeamMemberData._nameAndJob);
        _bioText?.SetText(_currentTeamMemberData._bio);
        

        // sliders out
        if (_moraleSilderOut != null)
        {
            float moralePct = (_currentTeamMemberData._currentMorale * 1.0f) / (_currentTeamMemberData._maxMorale * 1.0f);
            _moraleSilderOut.fn_SetFillPct_Lerp(moralePct);
        }
        if (_energySiderOut != null)
        {
            float energyPct =_currentTeamMemberData._currentEnergy / _currentTeamMemberData._maxEnergy;
            _energySiderOut?.fn_SetFillPct_Lerp(energyPct);
        }
        // icon gauge out
        if (_moraleIconGauge != null) {
            float moralePct = (_currentTeamMemberData._currentMorale * 1.0f) / (_currentTeamMemberData._maxMorale * 1.0f);
            _moraleIconGauge.fn_SetLevel_Pct(moralePct);
        }
        if (_energyIconGauge != null) {
            float energyPct = _currentTeamMemberData._currentEnergy / _currentTeamMemberData._maxEnergy;
            _energyIconGauge?.fn_SetLevel_Pct(energyPct);
        }
        // pics
        if (_profileImg != null) 
            _profileImg.sprite = _currentTeamMemberData._profileSprite;
        if (_toolImg != null)
            _toolImg.sprite = _currentTeamMemberData._toolSprite;

    }

    private void Unsubscribe() {
        if (_currentTeamMemberData != null) {
            _currentTeamMemberData._OnChange -= HandleChange_UpdateValues;
        }
    }
    private void UnbindValues() {
        _nameAndJobText?.SetText("");
    }
    #endregion
}
