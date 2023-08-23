using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeamMember_UI_Element : MonoBehaviour
{
    public TMP_Text _nameText;

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
            _currentTeamMemberData.OnChange += HandleChange_UpdateValues;
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
        _nameText?.SetText(_currentTeamMemberData._name);
    }

    private void Unsubscribe() {
        if (_currentTeamMemberData != null) {
            _currentTeamMemberData.OnChange -= HandleChange_UpdateValues;
        }
    }
    private void UnbindValues() {
        _nameText?.SetText("");
    }
    #endregion
}
