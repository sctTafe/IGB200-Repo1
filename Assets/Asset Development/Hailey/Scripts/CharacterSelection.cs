using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public List<GameObject> characters;
    public int selectedCharacter = 0;
    public GameObject[] selected;

    public BattleSystem battleSystem;
    public BattleHUD playerHUD;

    public GameObject[] sleepy;

    public TMP_Text selectionText;

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
}
