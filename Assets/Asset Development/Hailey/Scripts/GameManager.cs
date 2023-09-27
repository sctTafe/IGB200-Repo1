using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton Setup
    public static GameManager instance = null;
    public List<TeamMemberTransfer_Data> team;
    public List<GameObject> battleTeam;
    public GameObject player1;
    public List<GameObject> prefab;

    void Awake() 
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        
        //Get Team from the progress scene through the StaticData class
        team = StaticData.team;
        player1 = StaticData.playerOne;
        
        //clear previous battleTeam
        battleTeam.Clear();

        //Debug.Log("type: " + team[0]._classType.ToString());
        //Based on the team member's class types, construct the battle team
        for(int i = 0; i < team.Count; i++)
        {
            Debug.Log("type: " + team[i]._classType.ToString());
            for(int j = 0; j < prefab.Count; j++)
            {
                if(team[i]._classType.ToString() == prefab[j].GetComponent<Unit>().type.ToString())
                {
                    battleTeam.Add(prefab[j]);
                    battleTeam[battleTeam.Count - 1].GetComponent<Unit>().maxHP = (int)team[i]._maxEnergy;
                    battleTeam[battleTeam.Count - 1].GetComponent<Unit>().currentHP = (int)team[i]._currentEnergy;
                    battleTeam[battleTeam.Count - 1].GetComponent<Unit>().unitLevel = i;
                }
            }
        }

        //Stand in code until classType is an error
        /*for (int j = 0; j < prefab.Count; j++)
        {
            battleTeam.Add(prefab[j]);
        }*/
    }
    
    // Start is called before the first frame update
    void Start()
    {
        /*Debug.Log("The team has " + team.Count + " team members");
        foreach(TeamMemberTransfer_Data role in team)
        {            
            Debug.Log(role._classType);
            //teamObjects.Add(role.gameObject)
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
