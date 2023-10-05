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
/// Dose:
///     Sets up all the TeamMember Groups & holds their references for others to call
/// 
/// Notes:
///     Is an Singelton
/// </summary>
/// 
public class TeanMember_SelectionGroupHolder_PersistentSingletonMng : MonoBehaviour
{
    #region Singelton Setup
    private static TeanMember_SelectionGroupHolder_PersistentSingletonMng _instance;
    public static TeanMember_SelectionGroupHolder_PersistentSingletonMng Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("TeanMember_SelectionGroupHolder_Mng instance is not found.");
            }
            return _instance;
        }
    }
    private void SingeltonSetup()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion


    public bool Debugging_LoadingTestTeamMembers;

    public Action _OnSetupComplete;

    public TeamMember_SelectionGroup_Data _avalibleTeamMemberPool { get; private set; }
    public TeamMember_SelectionGroup_Data _selectedMissionTeam { get; private set; }
    public TeamMember_SelectionGroup_Data _newTeamMemberPool { get; private set; }

    private int currentUID = 0;


    private void Awake()
    {
        SingeltonSetup();
    }
    void Start()
    {
        Setup_CreateGroups();
        if (Debugging_LoadingTestTeamMembers)
        {
            Debuging_CreateTestPool(_avalibleTeamMemberPool, 3);
            Debuging_CreateTestPool(_selectedMissionTeam, 2);
            Debugging_ListLengths(_avalibleTeamMemberPool);
            Debugging_ListLengths(_selectedMissionTeam);
        }
        _OnSetupComplete?.Invoke();
    }
    //void Update()
    //{      
    //}

    private void Setup_CreateGroups() {
        // mission team limited to group size of 4
        _selectedMissionTeam = new TeamMember_SelectionGroup_Data(SelectionGroupType.Mission, 4);
        
        // all other groups unlimited group size
        _avalibleTeamMemberPool = new TeamMember_SelectionGroup_Data(SelectionGroupType.Available);
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