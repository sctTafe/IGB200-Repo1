using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///
/// Sets up all the TeamMember Groups & holds their references for others to call
/// 
/// </summary>
public class TeanMember_SelectionGroupHolder_Mng : MonoBehaviour
{
    public TeamMember_SelectionGroup_Data _avalibleTeamMemberPool;
    public TeamMember_SelectionGroup_Data _selectedMissionTeam;
    public TeamMember_SelectionGroup_Data _newTeamMemberPool;

    private int currentUID = 0;

    void Start()
    {
        Setup_CreateGroups();

        Debuging_CreateTestPool(_avalibleTeamMemberPool,3);
        Debuging_CreateTestPool(_selectedMissionTeam,2);
        //Debugging_ListLengths();
    }
    void Update()
    {
        
    }



    private void Setup_CreateGroups() {
        _avalibleTeamMemberPool = new TeamMember_SelectionGroup_Data();
        _selectedMissionTeam = new TeamMember_SelectionGroup_Data();
        _newTeamMemberPool = new TeamMember_SelectionGroup_Data();
    }


    private void Debuging_CreateTestPool(TeamMember_SelectionGroup_Data tGD, int numberToCreate = 1) {
        
        for (int i = 0; i < numberToCreate; i++) {
            //create new selectable
            TeamMember_Data tm = new TeamMember_Data();
            tm._name = Debugging_RandomString(5);
            tm._uID = GetNewUID();
            tGD._teamMembersGroup.TryAdd(tm._uID, tm);
        }
    }

    private void Debugging_ListLengths()
    {
        int lenght = _avalibleTeamMemberPool._teamMembersGroup.Count;
        Debug.Log("_avalibleTeamMemberPool List lenght = " + lenght);
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
}
