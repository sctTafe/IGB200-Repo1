using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton Setup
    public static GameManager instance = null;
    public List<TeamMemberTransfer_Data> team;
    public List<GameObject> battleTeam;
    public List<GameObject> prefab;

    void Awake() 
    {
        #region Singleton Setup
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        #endregion
    }
    void Start()
    {
        //Get Team from the progress scene through the StaticData class
        team = StaticData.team;

        //clear previous battleTeam
        battleTeam.Clear();

        //Based on the team member's class types, construct the battle team
        for (var i = 0; i < team.Count; i++) {
            Debug.Log("type: " + team[i]._classType.ToString());

            // DOSE: Cycles through the classType Prefabs
            for (var j = 0; j < prefab.Count; j++) {
                //DOSE: Match Class type in staticData team, to class type of Prefabs (by string)
                if (team[i]._classType.ToString() == prefab[j].GetComponent<Unit>().type.ToString()) {
                    var teamMember = Instantiate(prefab[j]);             // SCOTT EDIT
                    //battleTeam.Add(prefab[j]);
                    battleTeam.Add(teamMember);
                    var teamMemberData = battleTeam[battleTeam.Count - 1].GetComponent<Unit>();

                    teamMemberData.index = i;
                    teamMemberData._teamMemberName = (string)team[i]._teamMemberName;

                    teamMemberData.maxHP = (int)team[i]._maxEnergy;
                    teamMemberData.currentHP = (int)team[i]._currentEnergy;

                    teamMemberData.maxMorale = (int)team[i]._maxMorale;
                    teamMemberData.currentMorale = (int)team[i]._currentMorale;

                }
            }
        }
    }



}
