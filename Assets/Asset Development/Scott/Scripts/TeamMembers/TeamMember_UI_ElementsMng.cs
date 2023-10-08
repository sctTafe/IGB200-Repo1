using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamMember_UI_ElementsMng : MonoBehaviour
{
    public Action<TeamMember_Data> _OnTeamMemberClicked;
    public Action<TeamMember_Data, TeamMember_SelectionGroup_Data> _OnPrimaryActionBtn;

    [SerializeField] private TeamMember_UI_Element _templateUIElement;
    [SerializeField] private GameObject _panelRoot;
    
    private Transform _panelRootTransform;
    private Dictionary<int, TeamMember_UI_Element> _UIElementsHolder_Dic = new Dictionary<int, TeamMember_UI_Element>();
    private TeamMember_SelectionGroup_Data _boundSelectionGroup;

    private void Awake()
    {
        _panelRootTransform = _templateUIElement.transform.parent; // gets the parent transform of the Template UI Element 
        _templateUIElement.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        UnbindEventSubscription();
    }

    public void fn_Bind(TeamMember_SelectionGroup_Data selectionGroupData)
    {
        // If already bound, unbind from current entity
        UnbindEventSubscription();
        _boundSelectionGroup = selectionGroupData;

        // if new entity is not null, bind to it
        if (_boundSelectionGroup != null)
        {
            _boundSelectionGroup.OnChange += HandleOnSelectionGroupChange;
            RefreshUiElements();
            _panelRoot.gameObject.SetActive(true);
        }
        else
        {
            _panelRoot.gameObject.SetActive(false);
        }
    }

    public void fn_ElementBtnPressed(TeamMember_Data teamMemberData) {
        Debug.Log("fn_ElementBtnPressed Event Invoked, by: " + teamMemberData._name);
        _OnTeamMemberClicked?.Invoke(teamMemberData);
    }

    public void fn_PrimaryActionTriggered(TeamMember_Data teamMemberData)
    {
        Debug.Log("fn_PrimaryActionTriggered Event Invoked, by: " + teamMemberData._name);
        _OnPrimaryActionBtn?.Invoke(teamMemberData, _boundSelectionGroup);
    }


    private void UnbindEventSubscription()
    {
        if (_boundSelectionGroup != null)
            _boundSelectionGroup.OnChange -= HandleOnSelectionGroupChange;
    }

    private void RefreshUiElements()
    {
        ClearUiElements();
        foreach (var teamMemberData in _boundSelectionGroup._teamMembersGroup.Values) {
            UISlot_CreateSelectableUIElement(teamMemberData);
        }
    }
    private void ClearUiElements()
    {
        foreach (var uiElement in _UIElementsHolder_Dic.Values)
        {
            Destroy(uiElement.gameObject);
        }
        _UIElementsHolder_Dic.Clear();
    }

    private void UISlot_CreateSelectableUIElement(TeamMember_Data teamMemberData) {
        TeamMember_UI_Element newTeamMemberUIElement = Instantiate(_templateUIElement, _panelRootTransform);
        newTeamMemberUIElement.fn_Bind(teamMemberData, this);
        newTeamMemberUIElement.gameObject.SetActive(true);
        _UIElementsHolder_Dic.Add(teamMemberData._uID, newTeamMemberUIElement);
    }



    private void HandleOnSelectionGroupChange()
    {
        RefreshUiElements();
    }


    #region Bispoke Solutions
    // - Display Next Team Member in the Big Info Popup - 
    public void fn_DisplayNextTeamMemberInBigInfo(RecruitmentGroup_Mng rgm) {
        // calls the event for binding the Team Member to the Big Info, to display the next, or if the are no more, binds 'null' to it, to clear it
        if (_UIElementsHolder_Dic.Count > 0) {
            _UIElementsHolder_Dic.FirstOrDefault().Value.fn_Handle_OnElementBtn();
        }
        else {
            rgm.fn_ClearBigInfo();
        }
    }
    #endregion



    #region Debugging
    [ContextMenu("Testing - ClearUiElements")]
    private void Testing_ClearUiElements() {
        ClearUiElements();
    }
    #endregion


}
