using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public List<GameObject> characters;
    //public List<GameObject> characters = StaticData.team;
    public int selectedCharacter = 0;
    public GameObject[] lightLocations;
    public GameObject[] selected;

    public BattleSystem battleSystem;
    public BattleHUD playerHUD;

    public GameObject pointLight;

    public GameObject[] sleepy;  

    void Start()
    {
        
    }  

    void Update()
    {
        for(int i = 0; i < characters.Count; i++)
        {
            if(characters[i].GetComponent<Unit>().currentHP <= 0 && !characters[i].GetComponent<Unit>().isSleeping)
            {
                sleepy[i].SetActive(true);
                characters[i].GetComponent<Unit>().isSleeping = true;
            }
            else if(characters[i].GetComponent<Unit>().currentHP > 0 && characters[i].GetComponent<Unit>().isSleeping)
            {
                sleepy[i].SetActive(false);
                characters[i].GetComponent<Unit>().isSleeping = false;
            }
        }
    }

    public void NextCharacter()
    {   
        selected[selectedCharacter].SetActive(false);     
        selectedCharacter = (selectedCharacter + 1) % characters.Count;
        pointLight.transform.position = lightLocations[selectedCharacter].transform.position;
        selected[selectedCharacter].SetActive(true);
  
        //Update Battle System and HUD
        battleSystem.playerUnit = characters[selectedCharacter].GetComponent<Unit>();
        playerHUD.SetButtons(characters[selectedCharacter].GetComponent<Unit>());
    }

    public void PreviousCharacter()
    {
        selected[selectedCharacter].SetActive(false); 
        selectedCharacter--;
        if(selectedCharacter < 0)
        {
            selectedCharacter += characters.Count;
        }
        pointLight.transform.position = lightLocations[selectedCharacter].transform.position;
        selected[selectedCharacter].SetActive(true);        

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
