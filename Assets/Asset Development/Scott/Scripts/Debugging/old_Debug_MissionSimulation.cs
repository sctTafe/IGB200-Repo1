using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class old_Debug_MissionSimulation : MonoBehaviour
{
    private TeamMember_SelectionGroup_Data _currentMissionTeam;
    // Start is called before the first frame update
    //void Start()
    //{   
    //}

    //// Update is called once per frame
    //void Update()
    //{       
    //}

    [ContextMenu("Simulate Battle Outcome")]
    public void fn_SimulateChangesToTeamMemberValues()
    {
        UpdateTeamMembers();
    }


    private void UpdateTeamMembers()
    {
        retriveCurrentMissionTeam();

        // Update Each Mission Team Team Members Values
        foreach (var teamMember in _currentMissionTeam._teamMembersGroup)
        {
            // Reduce Each Team Members Moral by 10 - 30% of max value
            teamMember.Value._currentMorale = teamMember.Value._currentMorale - (teamMember.Value._maxMorale / 10) * Random.Range(1.0f, 3.0f);
        }
    }

    private void retriveCurrentMissionTeam()
    {
        _currentMissionTeam = TeanMember_SelectionGroupHolder_Mng.Instance._selectedMissionTeam;
    }
}
