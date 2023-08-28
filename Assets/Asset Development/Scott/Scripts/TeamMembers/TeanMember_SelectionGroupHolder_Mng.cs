using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public enum SelectionGroupType
{
    error,
    Available,
    Mission,
    Purchase,
    Other
}

/// <summary>
///
/// Sets up all the TeamMember Groups & holds their references for others to call
/// 
/// </summary>
/// 
public class TeanMember_SelectionGroupHolder_Mng : MonoBehaviour
{
    public bool isLoadingTestMembers;

    public Action _OnSetupComplete;

    public TeamMember_SelectionGroup_Data _avalibleTeamMemberPool { get; private set; }
    public TeamMember_SelectionGroup_Data _selectedMissionTeam { get; private set; }
    public TeamMember_SelectionGroup_Data _newTeamMemberPool { get; private set; }

    private int currentUID = 0;

    void Start()
    {
        Setup_CreateGroups();
        if (isLoadingTestMembers)
        {
            Debuging_CreateTestPool(_avalibleTeamMemberPool, 3);
            Debuging_CreateTestPool(_selectedMissionTeam, 2);
            Debugging_ListLengths(_avalibleTeamMemberPool);
            Debugging_ListLengths(_selectedMissionTeam);
        }

        _OnSetupComplete?.Invoke();
    }
    void Update()
    {      
    }

    private void Setup_CreateGroups() {
        _avalibleTeamMemberPool = new TeamMember_SelectionGroup_Data(SelectionGroupType.Available);
        _selectedMissionTeam = new TeamMember_SelectionGroup_Data(SelectionGroupType.Mission);
        _newTeamMemberPool = new TeamMember_SelectionGroup_Data(SelectionGroupType.Purchase);
    }

    public void fn_LoadingIntoAvalibleSelectionGroup(TeamMember_Data teamMembereData)
    {
        teamMembereData._uID = GetNewUID();
        _avalibleTeamMemberPool.fn_TryAddTeamMember(teamMembereData);
    }


    private void Debuging_CreateTestPool(TeamMember_SelectionGroup_Data tGD, int numberToCreate = 1) {
        
        for (int i = 0; i < numberToCreate; i++) {
            //create new selectable
            TeamMember_Data tm = new TeamMember_Data();
            tm._name = Debugging_RandomString(5);
            tm._uID = GetNewUID();
            tGD.fn_TryAddTeamMember(tm);
        }
    }

    private void Debugging_ListLengths(TeamMember_SelectionGroup_Data tMSGD)
    {
        int length = tMSGD._teamMembersGroup.Count;
        Debug.Log("TeamMember_SelectionGroup_Data: List length = " + length);
    }

    private string Debugging_RandomString(int length)
    {
        System.Random random = new System.Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Use Linq to generate a random string of the specified length
        string randomString = new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return randomString;
    }

    private int GetNewUID()
    {
        currentUID++;
        return currentUID;
    }

    #region Debugging
    [ContextMenu("Testing - Get Current Dictionary Lengths")]
    private void Testing_CurrentDictionaryLengths() {
        Debugging_ListLengths(_avalibleTeamMemberPool);
        Debugging_ListLengths(_selectedMissionTeam);
    }
    #endregion

}
