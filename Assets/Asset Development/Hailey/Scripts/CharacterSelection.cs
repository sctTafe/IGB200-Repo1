using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;

    public BattleSystem battleSystem;
    public BattleHUD playerHUD;

    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);

        //Update Battle System and HUD
        battleSystem.playerUnit = characters[selectedCharacter].GetComponent<Unit>();
        playerHUD.SetButtons(characters[selectedCharacter].GetComponent<Unit>());
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);

        //Update Battle System and HUD
        battleSystem.playerUnit = characters[selectedCharacter].GetComponent<Unit>();
        playerHUD.SetButtons(characters[selectedCharacter].GetComponent<Unit>());
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
