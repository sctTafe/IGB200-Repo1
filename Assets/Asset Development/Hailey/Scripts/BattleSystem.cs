using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    //public GameObject playerOne = StaticData.playerOne;
    public GameObject enemyPrefab;

    public Transform[] playerBattleStations;
    public Transform enemyBattleStation;

    public Unit playerUnit;
    public Unit enemyUnit;

    public GameObject enemyGO;

    public TMP_Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public bool hasActed = false;
    public bool isHealing = false;

    public CharacterSelection players;

    public string scene;
    
    //battle animations
    private Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        players.characters = GameManager.instance.battleTeam; //uncomment when testing info transfer

        foreach (GameObject character in players.characters)
        {
            Debug.Log("character is: " + character.GetComponent<Unit>().type);
        }
        
        foreach (GameObject character in GameManager.instance.battleTeam)
        {
            Debug.Log("battle character is: " + character.GetComponent<Unit>().type);
        }
        StartCoroutine(SetUpBattle());
    }

    void Update()
    {

    }

    //player and enemy spawn into battle
    IEnumerator SetUpBattle() 
    {       
        GameObject playerGO = playerPrefab;
        //GameObject playerGo = playerPrefab;
        playerUnit = playerGO.GetComponent<Unit>();

        for(int i=0; i < players.characters.Count; i++)
        {
            Debug.Log("instantiate team member" + players.characters[i].GetComponent<Unit>().type);
            Instantiate(players.characters[i], playerBattleStations[i]);
        }

        enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        enemyAnimator = enemyGO.GetComponent<Animator>();

        playerHUD.characters = players.characters;
        enemyHUD.characters = new List<GameObject> {enemyPrefab};

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches.";

        playerHUD.SetHUD();
        enemyHUD.SetHUD();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    //handles player's attack
    IEnumerator PlayerAttack(bool isSpecial)
    {
        //determine amount of damage
        //var rnd = new System.Random();
        //int rndDamage = rnd.Next(-10, 10);

        //Debug.Log(rndDamage);
        
        int damage;
        if (isSpecial)
        {
            if (playerUnit.type == enemyUnit.type)
            {
                damage = playerUnit.specialDamage;
                Debug.Log("special attack! very effective");
                dialogueText.text = "The attack is super successful!";
            }
            else
            {
                damage = 1;
                Debug.Log("not very effective");
                dialogueText.text = "The attack is not very effective";
            }
        }
        else
        {
            damage = playerUnit.damage;
            Debug.Log("normal");
            dialogueText.text = "The attack is successful";
        }

        //Damage enemy
        bool isDead = enemyUnit.TakeDamage(damage);
        enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.unitLevel);
        

        yield return new WaitForSeconds(2f);

        //Check if the enemy is dead
        //Change state based on what happened
        if(isDead)
        {
            //End the battle
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            //enemy turn            
            state = BattleState.ENEMYTURN;
            enemyAnimator.SetBool("IsAttacking", true);
            StartCoroutine(EnemyTurn());
        }     

    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";
        
        Debug.Log("enemy's turn and it has attacked");

        yield return new WaitForSeconds(1f);
        
        AttackAPlayer();

        bool allDead = players.AllDead();

        yield return new WaitForSeconds(1f);
        
        enemyAnimator.SetBool("IsAttacking", false);

        if(allDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;            
            PlayerTurn();
        }
    }

    void AttackAPlayer()
    {
        var rnd = new System.Random();
        int extraDamage = 0;
        int i = rnd.Next(0, players.characters.Count - 1);
        Unit tempPlayer = players.characters[i].GetComponent<Unit>();
        if(tempPlayer.weakness == enemyUnit.type) 
        {
            Debug.Log("weakened");
            extraDamage = 5;
        }
        tempPlayer.TakeDamage(enemyUnit.damage + extraDamage);
        playerHUD.SetHP(tempPlayer.currentHP, tempPlayer.unitLevel);
    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            StaticData.isBattleWon = true;
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated. ";
            StaticData.isBattleWon = false;
        }

        //UpdateTeamInformation();
        StaticData.team.Clear(); //reset the team list
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }

    void UpdateTeamInformation()
    {
        bool condition = StaticData.team.Count == players.characters.Count;
        if (condition)
        {
            for (int i = 0; i < players.characters.Count; i++)
            {
                StaticData.team[i]._currentEnergy = players.characters[i].GetComponent<Unit>().currentHP;
                StaticData.team[i]._numDeaths = players.characters[i].GetComponent<Unit>().numDeaths;
            }
        }
    }

    void PlayerTurn()
    {
        hasActed = false;
        dialogueText.text = "Choose an action for " + playerUnit.unitName;
    }

    public IEnumerator PlayerHeal()
    {
        //playerUnit.Heal(5);

        //playerHUD.SetHP(playerUnit.currentHP, playerUnit.unitLevel);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    //execute player attack
    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN || hasActed)
            return;
   
        //if player dead, choose another player
        if(playerUnit.currentHP == 0)
        {
            dialogueText.text = "Team member is exhausted. Choose another";
            return;
        }
        //Change button colour 
        hasActed = !hasActed;   
        StartCoroutine(PlayerAttack(false));
    }

    //execute healing action
    public void OnHealButton()
    {
        if(state != BattleState.PLAYERTURN || hasActed)
            return;

        //if player is dead choose another player
        if(playerUnit.currentHP == 0)
        {
            dialogueText.text = "Team member is exhausted. Choose another";
            return;
        }
        //change button colour 
        hasActed = true;  
        isHealing = true;
        dialogueText.text = "Choose a team member to heal";       
        //StartCoroutine(PlayerHeal());
    }

    //execute player special attack
    public void OnSpecialAttackButton()
    {
        if(state != BattleState.PLAYERTURN || hasActed)
            return;
   
        //if player dead, choose another player
        if(playerUnit.currentHP == 0)
        {
            dialogueText.text = "Team member is exhausted. Choose another";
            return;
        }
        //Change button colour 
        hasActed = !hasActed;   
        StartCoroutine(PlayerAttack(true));
    }
}
