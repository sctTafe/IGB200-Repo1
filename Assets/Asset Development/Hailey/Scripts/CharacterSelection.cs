using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter = 0;

    public BattleSystem battleSystem;
    public BattleHUD playerHUD;

    public GameObject pointLight;

    public void NextCharacter()
    {        
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        pointLight.transform.position = new Vector3(
            characters[selectedCharacter].transform.position.x, 
            pointLight.transform.position.y, 
            pointLight.transform.position.z);

        //Update Battle System and HUD
        battleSystem.playerUnit = characters[selectedCharacter].GetComponent<Unit>();
        playerHUD.SetButtons(characters[selectedCharacter].GetComponent<Unit>());
    }

    public void PreviousCharacter()
    {
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        pointLight.transform.position = new Vector3(
            characters[selectedCharacter].transform.position.x, 
            pointLight.transform.position.y, 
            pointLight.transform.position.z);

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
