using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    #region Singelton Setup
    public static CharacterSelection Instance = null;
   
    private void SingeltonSetup() {
        //Check if Instance already exists
        if (Instance == null)

            //if not, set Instance to this
            Instance = this;

        //If Instance already exists and it's not this:
        else if (Instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one Instance of a GameManager.
            Destroy(gameObject);
    }
    #endregion

    public List<GameObject> characters;   // IMPORTANT NOTE: THIS IS THE ACTIVE DATA FOR THE IN-PLAY TEAM MEMBERS!!!
    public int selectedCharacter = 0;
    public GameObject[] selected;

    public BattleSystem battleSystem;
    public BattleHUD playerHUD;

    public GameObject[] sleepy;

    public TMP_Text selectionText;

    void Awake()
    {
        SingeltonSetup();
    }

    void Start()
    {
        //selectionText.text = characters[selectedCharacter].GetComponent<Unit>().unitName + " is in play";
        selectionText.text = characters[selectedCharacter].GetComponent<Unit>()._teamMemberName + " is in play";        //SCOT EDIT
    }  

    void Update()
    {
        for(int i = 0; i < characters.Count; i++)
        {
            if(characters[i].GetComponent<Unit>().currentHP <= 0 && !characters[i].GetComponent<Unit>().isSleeping)
            {
                Debug.Log("sleepy");
                sleepy[i].SetActive(true);
                characters[i].GetComponent<Unit>().isSleeping = true;
            }
            else if(characters[i].GetComponent<Unit>().currentHP > 0 && characters[i].GetComponent<Unit>().isSleeping)
            {
                Debug.Log("not sleepy");
                sleepy[i].SetActive(false);
                characters[i].GetComponent<Unit>().isSleeping = false;
            }
        }
    }

    public void fn_Initialize()
    {
        selected[0].SetActive(true);
        //Update Battle System and HUD
        battleSystem.playerUnit = characters[0].GetComponent<Unit>();
        playerHUD.SetButtons(characters[0].GetComponent<Unit>());
        //Update text
        selectionText.text = characters[0].GetComponent<Unit>()._teamMemberName + " is in play";
    }
    public void NextCharacter()
    {
        if (!battleSystem.isHealing)
        {
            selected[selectedCharacter].SetActive(false);     
            selectedCharacter = (selectedCharacter + 1) % characters.Count;
            selected[selectedCharacter].SetActive(true);
  
            //Update Battle System and HUD
            battleSystem.playerUnit = characters[selectedCharacter].GetComponent<Unit>();
            playerHUD.SetButtons(characters[selectedCharacter].GetComponent<Unit>());
        
            //Update text
            selectionText.text = characters[selectedCharacter].GetComponent<Unit>()._teamMemberName + " is in play";
        }
    }

    public void PreviousCharacter()
    {
        if (!battleSystem.isHealing)
        {
            selected[selectedCharacter].SetActive(false); 
            selectedCharacter--;
            if(selectedCharacter < 0)
            {
                selectedCharacter += characters.Count;
            }
            selected[selectedCharacter].SetActive(true);        

            //Update Battle System and HUD
            battleSystem.playerUnit = characters[selectedCharacter].GetComponent<Unit>();
            playerHUD.SetButtons(characters[selectedCharacter].GetComponent<Unit>());
        
            //Update text
            selectionText.text = characters[selectedCharacter].GetComponent<Unit>()._teamMemberName + " is in play";
        }
    }

    public bool AllDead()
    {
        foreach(GameObject character in characters)
        {
            int hp = character.GetComponent<Unit>().currentHP;
            if(hp > 0)
                return false;
        }
        return true;
    }

    public void fn_TransferTeamMembersDataBackToStaticData()
    {
        List<TeamMemberTransfer_Data> team = StaticData.team;
        Unit currentUnit_j;

        for (var i = 0; i < team.Count; i++) {
            for (int j = 0; j < characters.Count; j++) {
                currentUnit_j = characters[j].GetComponent<Unit>();
                if (currentUnit_j._teamMemberName == team[i]._teamMemberName) {
                    team[i]._currentEnergy = currentUnit_j.currentHP;
                    team[i]._currentMorale = currentUnit_j.currentMorale;
                }
            }
        }
    }

}
